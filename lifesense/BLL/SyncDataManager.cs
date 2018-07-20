using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lifesense.BLL.http;
using lifesense.BLL.http.ResponseParam;
using Maticsoft.Common;
using System.Threading;
using lifesense.BLL;

namespace ConsoleLifesense
{
    public  class SyncDataManager
    {
        public  String token;
        private string mSyncDay;
        //数据同步的时间点，每天中午12点同步数据
        private  int SYC_HOUR = 12;
        private int SYC_SleepTime = 600000;
        private int Exption_SleepTime = 300000;
        public log4net.ILog log = null;
        public SyncDataManager()
        {
           if(log==null)
           {
               log = log4net.LogManager.GetLogger("服务模式");
           }
         }
        public void start()
        {
            getSysConfigue();

            Thread thread = new Thread(syncData);
            thread.Start();

            Thread threadException = new Thread(syncDataFromFailList);
            threadException.Start();
        }
        /// <summary>
        /// 获取配置文件
        /// </summary>
        public void getSysConfigue()
        {
            try
            {
                t_configinfo configbll = new t_configinfo();
                List<lifesense.Model.t_configinfo> listmodel = configbll.GetModelList("");
                if (listmodel.Count > 0)
                {
                    listmodel.ForEach(model =>
                        {
                            if (model.keyId == "synTime")
                            {
                                SYC_HOUR = Convert.ToInt32(model.KeyValue);
                            }
                            else if (model.keyId == "synSleepTime")
                            {
                                SYC_SleepTime = Convert.ToInt32(model.KeyValue);
                            }
                            else  if (model.keyId == "exceptionSynSleepTime")
                            {
                                Exption_SleepTime = Convert.ToInt32(model.KeyValue);
                            }
                        });
                }
            }
            catch(Exception ex)
            {
                log.Info("异常信息:"+ex.Message.ToString());
            }
        }
        public bool IsSyncData = true;
        public bool IsSyncDataFail = true;
        private void syncData()
        {
            int currentHour;
            string lastSyncDay = "";
            while (IsSyncData)
            {
                try
                {
                    currentHour = DateTime.Now.Hour;
                    mSyncDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    if (currentHour == SYC_HOUR && !mSyncDay.Equals(lastSyncDay))
                    {
                        log.Info("开始同步");
                        lastSyncDay = mSyncDay;
                        syncData(mSyncDay);
                        log.Info("结束同步");
                    }

                }
                catch
                {

                }
                finally{
                Thread.Sleep(SYC_SleepTime);
                }
            }
        }

       public List<lifesense.Model.t_failrequestInfo> listFailModel = null;
       public string msg = string.Empty;
        //从失败列表中同步数据
        public void syncDataFromFailList()
        {
            while (IsSyncDataFail)
            {
                try
                {
                    lifesense.BLL.t_failrequestInfo failBll = new lifesense.BLL.t_failrequestInfo();
                    this.listFailModel = failBll.GetModelList("");
                    if (listFailModel == null)
                    {
                        return;
                    }
                    log.Info("失败列表开始同步");
                    syncExceptionData();
                    log.Info("失败列表结束同步,详细信息如下："+msg);
                }
                catch
                {

                }
                finally {
                    Thread.Sleep(Exption_SleepTime);
                }
            }
        }
      private static  object obj = new object();
        public void syncData(lifesense.Model.t_userinfo userModel, string syncDay)
        {
            lock (obj)
            {
                token = new HttpToken(userModel, syncDay).getTempAuthorizeCode();
                if (!string.IsNullOrEmpty(token))
                {
                    string authorizeCode = new HttpCheckUser(syncDay, token, userModel).getTempAuthorizeCode();
                    //Thread.Sleep(1000 * 5);
                    if (!string.IsNullOrEmpty(authorizeCode))
                    {
                        AcessTokenandOpendid model = new UserInfo(authorizeCode, userModel, syncDay).getUserInfo();
                        if (model != null)
                        {
                            SleepData sleepData = new HttpSleepData(model, userModel, syncDay).getSleepData(syncDay);
                            bool saveSleepSuccess = saveSleepData(sleepData, userModel);
                            log.Info("同步日期" + syncDay + "深睡时间:" + sleepData.sleep.depthTime);
                            SportData sportData = new HttpSportData(model, userModel, syncDay).getSportData(syncDay);
                            bool saveSportSuccess = saveSportData(sportData, userModel, syncDay);
                            log.Info("同步日期" + syncDay + "卡里数:" + sportData.sport.calorie);
                            HeartrateData heartrateData = new HttpHeartData(model, userModel, syncDay).getHeartrateData(syncDay);
                            bool saveHeartrateSuccess = saveHeartrateData(heartrateData, userModel, syncDay);
                            log.Info("同步日期" + syncDay + "心率:" + heartrateData.heartrate.heartrate != null ? "" : heartrateData.heartrate.heartrate.ToString());
                            // 数据都同步成功了，再重错误列表中移除掉
                            if (saveSleepSuccess && saveSportSuccess && saveHeartrateSuccess)
                            {
                                FailRequestManager.mInstance.deleteFromFialList(userModel.UserID, Convert.ToDateTime(syncDay));

                            }
                            else
                            {
                                log.Info(string.Format("存在同步失败的:睡眠{0},步行{1},心率{2}", saveSleepSuccess, saveSportSuccess, saveHeartrateSuccess));
                            }
              
                        }
                    }
                }
                //Thread.Sleep(1000*30);
            }
        }

        private void syncData(string syncDay)
        {
            lifesense.BLL.t_userinfo userBll = new lifesense.BLL.t_userinfo();
            List<lifesense.Model.t_userinfo> listUser = userBll.GetModelList("");
            listUser.ForEach(userModel =>
            {
                syncData(userModel, syncDay);
            });
        }


        private bool saveSleepData(SleepData sleepData, lifesense.Model.t_userinfo mUserInfoModel)
        {
            if (sleepData == null || sleepData.sleep == null)
            {
                return false;
            }
            lifesense.Model.t_sleepinfo sleepModel = getSleepInfoModel(sleepData, mUserInfoModel);
            lifesense.BLL.t_sleepinfo sleepBll = new lifesense.BLL.t_sleepinfo();
            string strWhere = " UserId='" + mUserInfoModel.UserID + "' and SleepingTime='" + sleepModel.SleepingTime + "'";
            List<lifesense.Model.t_sleepinfo> sleepInfoList = sleepBll.GetModelList(strWhere);
            if (sleepInfoList != null && sleepInfoList.Count <= 0)
            {
                return sleepBll.Add(sleepModel) > 0 ? true : false;
            }
            return true;
        }

        private lifesense.Model.t_sleepinfo getSleepInfoModel(SleepData sleepData, lifesense.Model.t_userinfo mUserInfoModel)
        {
            lifesense.Model.t_sleepinfo sleepModel = new lifesense.Model.t_sleepinfo();
            sleepModel.UserID = mUserInfoModel.UserID;
            sleepModel.LongSleepNum = sleepData.sleep.depthTime;
            sleepModel.SleepingTime = TimeParser.GetTime(sleepData.sleep.startTime.ToString());
            sleepModel.ShallowSleepNum = sleepData.sleep.shallowTime;
            sleepModel.WakeUpLong = sleepData.sleep.consciousTime;
            sleepModel.WakingTime = Convert.ToDateTime(sleepModel.SleepingTime).AddMinutes(sleepData.sleep.totalTime - sleepData.sleep.consciousTime);
            return sleepModel;
        }


        private bool saveSportData(SportData sportData, lifesense.Model.t_userinfo mUserInfoModel, string day)
        {
            if (sportData == null || sportData.sport == null)
            {
                return false;
            }
            lifesense.Model.t_walkinfo sportModel = getSportModel(sportData, mUserInfoModel,day);
            lifesense.BLL.t_walkinfo walkBll = new lifesense.BLL.t_walkinfo();
            String strWhere = " UserId='" + mUserInfoModel.UserID + "' and MeasureTime='" + sportModel.MeasureTime + "'";
            List<lifesense.Model.t_walkinfo> walkInfoList = walkBll.GetModelList(strWhere);
            if (walkInfoList != null && walkInfoList.Count <= 0)
            {
                return walkBll.Add(sportModel) > 0 ? true : false;
            }
            return true;
        }

        private lifesense.Model.t_walkinfo getSportModel(SportData sportData, lifesense.Model.t_userinfo mUserInfoModel,string day)
        {
            lifesense.Model.t_walkinfo walkModel = new lifesense.Model.t_walkinfo();
            walkModel.UserID = mUserInfoModel.UserID;
            walkModel.MeasureTime = Convert.ToDateTime(day);
            walkModel.StepNum = sportData.sport.stepCount;
            walkModel.Calorie =Convert.ToDecimal (sportData.sport.calorie);
            walkModel.Mileage =Convert.ToDecimal (sportData.sport.distance);
            return walkModel;
        }

        private bool saveHeartrateData(HeartrateData heartrateData, lifesense.Model.t_userinfo mUserInfoModel,string day)
        {
            if (heartrateData == null || heartrateData.heartrate == null)
            {
                return false;
            }
            lifesense.Model.t_heartrateinfo heartrateModel = getHeartrateInfoModel(heartrateData, mUserInfoModel,day);
            lifesense.BLL.t_heartrateinfo heartrateBll = new lifesense.BLL.t_heartrateinfo();
            string strWhere = " UserId='" + mUserInfoModel.UserID + "' and StartTime='" + heartrateModel.StartTime + "'";
            List<lifesense.Model.t_heartrateinfo> heartrateInfoList = heartrateBll.GetModelList(strWhere);
            if (heartrateInfoList != null && heartrateInfoList.Count <= 0)
            {
                return heartrateBll.Add(heartrateModel) > 0 ? true : false;
            }
            return true;
        }

        private lifesense.Model.t_heartrateinfo getHeartrateInfoModel(HeartrateData heartrateData, lifesense.Model.t_userinfo mUserInfoModel, string day)
        {
            lifesense.Model.t_heartrateinfo heartrateModel = new lifesense.Model.t_heartrateinfo();
            heartrateModel.UserID = mUserInfoModel.UserID;
            heartrateModel.StartTime = Convert.ToDateTime(day);
            heartrateModel.HeartRate = heartrateData.heartrate.heartrate;
            return heartrateModel;
        }
        /// <summary>
        /// 手动同步异常的数据
        /// </summary>
        /// <param name="syncDay"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void syncExceptionData()
        {
            List<lifesense.Model.t_failrequestInfo> listModel = this.listFailModel;
            string temp = string.Empty;
            if(listModel.Count>0)
            {
                listModel.ForEach(failModel =>
                {

                    lifesense.Model.t_userinfo userModel = new lifesense.Model.t_userinfo();
                    lifesense.BLL.t_userinfo userinfoBll = new lifesense.BLL.t_userinfo();
                    List<lifesense.Model.t_userinfo> listUser = userinfoBll.GetModelList(string.Format("UserID='{0}'", failModel.UserID));
                    if (listUser != null && listUser.Count == 1)
                    {
                        userModel = listUser[0];
                        syncData(userModel, failModel.WriteTime.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        temp += string.Format("不存在用户:{0}",failModel.UserID);
                    }
                });
                if(!string.IsNullOrEmpty(temp))
                {
                    msg=temp;
                }
            }

        }
       public DateTime beginTime;
       public DateTime endTime;
        /// <summary>
        /// 手动同步日期的数据
        /// </summary>
        /// <param name="syncDay"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void syncDateSegmentData()
        {
            int dayCount = (endTime - beginTime).Days;
            for (int i = 0; i < dayCount+1;i++)
            {
                syncData(beginTime.AddDays(i).ToString("yyyy-MM-dd"));
            }
        }
    }
}
