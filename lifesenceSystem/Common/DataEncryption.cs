using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataEncryption
    {
        private static string encryptkey = "OyOk";
        #region   对数据进行加密
        /// <summary>
        /// 对数据进行加密
        /// </summary>
        /// <param name="encryptstring">需要加密的数据</param>
        /// <returns></returns>
        public static string DESEncrypt(string encryptstring)
        {
            string strRtn;
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();//des进行加密
                byte[] key = System.Text.Encoding.Unicode.GetBytes(encryptkey);
                byte[] data = System.Text.Encoding.Unicode.GetBytes(encryptstring);
                MemoryStream ms = new MemoryStream();//存储加密后的数据
                CryptoStream cs = new CryptoStream(ms, desc.CreateEncryptor(key, key), CryptoStreamMode.Write);
                cs.Write(data, 0, data.Length);//进行加密
                cs.FlushFinalBlock();
                strRtn = Convert.ToBase64String(ms.ToArray());
                return strRtn;
            }

            catch (Exception ex)
            {
                //MessageBox.Show("错误：" + ex.Message, "错误消息提示框", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion

        #region   对数据进行解密
        /// <summary>
        /// 对数据进行解密
        /// </summary>
        /// <param name="decryptstring">需要解密的数据</param>
        /// <returns></returns>
        public static string DESDecrypt(string decryptstring)
        {
            string strRtn;
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                byte[] key = System.Text.Encoding.Unicode.GetBytes(encryptkey);
                byte[] data = Convert.FromBase64String(decryptstring);
                MemoryStream ms = new MemoryStream();//存储解密后的数据
                CryptoStream cs = new CryptoStream(ms, desc.CreateDecryptor(key, key), CryptoStreamMode.Write);
                cs.Write(data, 0, data.Length);//解密数据
                cs.FlushFinalBlock();
                strRtn = System.Text.Encoding.Unicode.GetString(ms.ToArray());
                return strRtn;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("错误：" + ex.Message, "错误消息提示框", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion
    }
}
