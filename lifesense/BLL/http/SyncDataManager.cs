using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lifesense.BLL.http;
using lifesense.BLL.http.ResponseParam;

namespace ConsoleLifesense
{
    public  class SyncDataManager
    {
        public  String token;
        public void start()
        {
            getToken();
        }

        private void getToken()
        {
          
           lifesense.BLL.t_userinfo userBll = new lifesense.BLL.t_userinfo();
           List<lifesense.Model.t_userinfo> listUser = userBll.GetModelList("");
           //listUser.ForEach(delegate(lifesense.Model.t_userinfo userModel)
           //{
           //    String authorizeCode = new CheckUser(token, userModel).getTempAuthorizeCode();
           //    AcessTokenandOpendid model = new UserInfo(authorizeCode).getUserInfo();
           //    if (model != null)
           //    {
           //        bool bol = new UserData(model).GetUserData();
           //    }
           //});
           listUser.ForEach(userModel => {
               token = new Token().getTempAuthorizeCode();
               if (!string.IsNullOrEmpty(token))
               {
                   String authorizeCode = new CheckUser(token, userModel).getTempAuthorizeCode();
                   if (!string.IsNullOrEmpty(authorizeCode))
                   {
                       AcessTokenandOpendid model = new UserInfo(authorizeCode).getUserInfo();
                       if (model != null)
                       {
                           bool bol = new UserData(model).GetUserData();
                       }
                   }
               }
           });
        }
    }
}
