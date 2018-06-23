using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lifesense.BLL.http.config
{
 public   class SHAUtils
    {

     public static String getSHACode(System.Collections.ArrayList array)
     {
         if (array == null)
         {
             return "";
         }
         else
         {
             array.Sort();
         }
         String content = array.ToString();
         return HashCode(content);
         
     }

     public static String HashCode(string str)
     {
         string rethash = "";
         try
         {

             System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
             System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
             byte[] combined = encoder.GetBytes(str);
             hash.ComputeHash(combined);
             rethash = Convert.ToBase64String(hash.Hash);
         }
         catch (Exception ex)
         {
             string strerr = "Error in HashCode : " + ex.Message;
         }
         return rethash;
     }


    }


}
