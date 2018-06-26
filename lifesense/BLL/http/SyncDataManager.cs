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
           token = new Token().getTempAuthorizeCode();
           String authorizeCode = new CheckUser(token).getTempAuthorizeCode();
           acessTokenandOpendid model = new UserInfo(authorizeCode).getUserInfo();
           if (model != null)
           {
               bool bol = new UserData(model).GetUserData();
           }
        }
    }
}
