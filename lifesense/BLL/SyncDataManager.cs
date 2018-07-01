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
        private string mSyncDay = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
        public void start()
        {
            syncData(mSyncDay);
        }

        //从失败列表中同步数据
        public void syncDataFromFailList()
        {
            lifesense.BLL.t_failrequestInfo failBll = new lifesense.BLL.t_failrequestInfo();
            List<lifesense.Model.t_failrequestInfo> listUser = failBll.GetModelList("");
            if (listUser == null)
            {
                return;
            }
            listUser.ForEach(failModel =>
            {
                lifesense.Model.t_userinfo userModel = new lifesense.Model.t_userinfo();
                userModel.UserID = failModel.UserID;
                syncData(userModel, failModel.WriteTime.ToString("yyyy-MM-dd"));
            });
        }
        public void syncData(lifesense.Model.t_userinfo userModel, string syncDay)
        {
            token = new HttpToken(userModel, syncDay).getTempAuthorizeCode();
            if (!string.IsNullOrEmpty(token))
            {
                string authorizeCode = new HttpCheckUser(syncDay, token, userModel).getTempAuthorizeCode();
                if (!string.IsNullOrEmpty(authorizeCode))
                {
                    AcessTokenandOpendid model = new UserInfo(authorizeCode, userModel, syncDay).getUserInfo();
                    if (model != null)
                    {
                        SleepData sleepData = new HttpSleepData(model, userModel, syncDay).getSleepData(syncDay);
                        bool saveSleepSuccess = saveSleepData(sleepData, userModel);

                        SportData sportData = new HttpSportData(model, userModel, syncDay).getSportData(syncDay);
                        bool saveSportSuccess = saveSportData(sportData, userModel, syncDay);

                        HeartrateData heartrateData = new HttpHeartData(model, userModel, syncDay).getHeartrateData(syncDay);
                        bool saveHeartrateSuccess = saveHeartrateData(heartrateData, userModel, syncDay);
                        // 数据都同步成功了，再重错误列表中移除掉
                        if (saveSleepSuccess && saveSportSuccess && saveHeartrateSuccess)
                        {
                            FailRequestManager.mInstance.deleteFromFialList(userModel.UserID, Convert.ToDateTime(syncDay));
                        }
                    }
                }
                //Thread.Sleep(1000*5);
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
        public void syncExceptionData(List<lifesense.Model.t_failrequestInfo> listModel,out string msg)
        {
            msg = string.Empty;
            string temp = string.Empty;
            if(listModel.Count>0)
            {
                listModel.ForEach(failModel =>
                {

                    lifesense.Model.t_userinfo userModel = new lifesense.Model.t_userinfo();
                    lifesense.BLL.t_userinfo userinfoBll = new lifesense.BLL.t_userinfo();
                    List<lifesense.Model.t_userinfo> listUser = userinfoBll.GetModelList(string.Format("UserID={0}", failModel.UserID));
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
        /// <summary>
        /// 手动同步日期的数据
        /// </summary>
        /// <param name="syncDay"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public void syncDateSegmentData(DateTime beginTime, DateTime endtime)
        {
            int dayCount = (endtime - beginTime).Days;
            for (int i = 0; i < dayCount;i++)
            {
                syncData(beginTime.AddDays(i).ToString("yyyy-MM-dd"));
            }
        }
    }
}
