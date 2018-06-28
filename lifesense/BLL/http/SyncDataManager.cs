using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lifesense.BLL.http;
using lifesense.BLL.http.ResponseParam;
using Maticsoft.Common;

namespace ConsoleLifesense
{
    public  class SyncDataManager
    {
        public  String token;
        private string syncDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        public void start()
        {
            getToken();
        }

        private void getToken()
        {
          
           lifesense.BLL.t_userinfo userBll = new lifesense.BLL.t_userinfo();
           List<lifesense.Model.t_userinfo> listUser = userBll.GetModelList("");
           listUser.ForEach(userModel => {
               token = new HttpToken().getTempAuthorizeCode();
               if (!string.IsNullOrEmpty(token))
               {
                   String authorizeCode = new HttpCheckUser(token, userModel).getTempAuthorizeCode();
                   if (!string.IsNullOrEmpty(authorizeCode))
                   {
                       AcessTokenandOpendid model = new UserInfo(authorizeCode).getUserInfo();
                       if (model != null)
                       {
                           SleepData sleepData = new HttpSleepData(model).getSleepData(syncDay);
                           bool saveSleepSuccess = saveSleepData(sleepData, userModel);

                           SportData sportData = new HttpSportData(model).getSportData(syncDay);
                           //bool saveSportSuccess = saveSportData(sportData, userModel);

                           HeartrateData heartrateData = new HttpHeartData(model).getHeartrateData();
                       }
                   }
               }
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
            String strWhere = " UserId='" + mUserInfoModel.UserID + "' and SleepingTime='" + sleepModel.SleepingTime + "'";
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


        //private bool saveSportData(SportData sportData, lifesense.Model.t_userinfo mUserInfoModel,string day)
        //{
        //    if (sportData == null || sportData.sport == null)
        //    {
        //        return false;
        //    }
        //    lifesense.Model.t_walkinfo sportModel = getSportModel(sportData, mUserInfoModel);
        //    lifesense.BLL.t_sleepinfo sleepBll = new lifesense.BLL.t_sleepinfo();
        //    String strWhere = " UserId='" + mUserInfoModel.UserID + "' and SleepingTime='" + sleepModel.SleepingTime + "'";
        //    List<lifesense.Model.t_sleepinfo> sleepInfoList = sleepBll.GetModelList(strWhere);
        //    if (sleepInfoList != null && sleepInfoList.Count <= 0)
        //    {
        //        return sleepBll.Add(sleepModel) > 0 ? true : false;
        //    }
        //    return true;
        //}

        //private lifesense.Model.t_walkinfo getSportModel(SportData sportData, lifesense.Model.t_userinfo mUserInfoModel)
        //{
        //    lifesense.Model.t_walkinfo walkModel = new lifesense.Model.t_walkinfo();
        //    walkModel.UserID = mUserInfoModel.UserID;
        //    walkModel.MeasureTime=sportData.sport.
        //    return walkModel;
        //}

        //private bool saveSleepData(SleepData sleepData, lifesense.Model.t_userinfo mUserInfoModel)
        //{
        //    if (sleepData == null || sleepData.sleep == null)
        //    {
        //        return false;
        //    }
        //    lifesense.Model.t_sleepinfo sleepModel = getSleepInfoModel(sleepData, mUserInfoModel);
        //    lifesense.BLL.t_sleepinfo sleepBll = new lifesense.BLL.t_sleepinfo();
        //    String strWhere = " UserId='" + mUserInfoModel.UserID + "' and SleepingTime='" + sleepModel.SleepingTime + "'";
        //    List<lifesense.Model.t_sleepinfo> sleepInfoList = sleepBll.GetModelList(strWhere);
        //    if (sleepInfoList != null && sleepInfoList.Count <= 0)
        //    {
        //        return sleepBll.Add(sleepModel) > 0 ? true : false;
        //    }
        //    return true;
        //}

        //private lifesense.Model.t_sleepinfo getSleepInfoModel(SleepData sleepData, lifesense.Model.t_userinfo mUserInfoModel)
        //{
        //    lifesense.Model.t_sleepinfo sleepModel = new lifesense.Model.t_sleepinfo();
        //    sleepModel.UserID = mUserInfoModel.UserID;
        //    sleepModel.LongSleepNum = sleepData.sleep.depthTime;
        //    sleepModel.SleepingTime = TimeParser.GetTime(sleepData.sleep.startTime.ToString());
        //    sleepModel.ShallowSleepNum = sleepData.sleep.shallowTime;
        //    sleepModel.WakeUpLong = sleepData.sleep.consciousTime;
        //    sleepModel.WakingTime = Convert.ToDateTime(sleepModel.SleepingTime).AddMinutes(sleepData.sleep.totalTime - sleepData.sleep.consciousTime);
        //    return sleepModel;
        //}

    }
}
