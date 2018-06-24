using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace lifesense.BLL.http.config
{
 public   class SHAUtils
    {

     public static String getSHACode(String[] list)
     {
         if (list != null && list.Length > 0)
         {
             Array.Sort(list, (a, b) => string.CompareOrdinal(a, b));
             StringBuilder sb = new StringBuilder();
             for (int i = 0; i < list.Length;i++ )
             {
                 sb.Append(list[i]);
             }
             return SHA1(sb.ToString()).ToLower();
         }
         else
         {
             return "";
         }

         
     }

     /// <summary>  
     /// SHA1 加密，返回大写字符串  
     /// </summary>  
     /// <param name="content">需要加密字符串</param>  
     /// <returns>返回40位UTF8 大写</returns>  
     public static string SHA1(string content)
     {
         return SHA1(content, Encoding.UTF8);
     }
     /// <summary>  
     /// SHA1 加密，返回大写字符串  
     /// </summary>  
     /// <param name="content">需要加密字符串</param>  
     /// <param name="encode">指定加密编码</param>  
     /// <returns>返回40位大写字符串</returns>  
     public static string SHA1(string content, Encoding encode)
     {
         try
         {
             SHA1 sha1 = new SHA1CryptoServiceProvider();
             byte[] bytes_in = encode.GetBytes(content);
             byte[] bytes_out = sha1.ComputeHash(bytes_in);
             //sha1.Dispose();
             string result = BitConverter.ToString(bytes_out);
             result = result.Replace("-", "");
             return result;
         }
         catch (Exception ex)
         {
             throw new Exception("SHA1加密出错：" + ex.Message);
         }
     }  


    }


}
