using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lifesense.BLL.http;

namespace ConsoleLifesense
{
    public  class SyncDataManager
    {
        public static String token;
        public void start()
        {
            getToken();
        }

        private void getToken()
        {
           token = new Token().getTempAuthorizeCode();
           String userInfo = new CheckUser().getTempAuthorizeCode();
        }
    }
}
