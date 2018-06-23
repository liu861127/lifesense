using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lifesense.BLL.http;

namespace ConsoleLifesense
{
    public  class SyncDataManager
    {

        public void start()
        {
            getToken();
        }

        private void getToken()
        {
           String token = new Token().getTempAuthorizeCode();
           token = "";
        }
    }
}
