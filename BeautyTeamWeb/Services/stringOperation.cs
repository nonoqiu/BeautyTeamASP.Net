using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BeautyTeamWeb.Services
{
    public static class stringOperation
    {
        public static string SimplifyHTML(this string WaitingtoAnalyse)
        {
            string s = string.Empty;
            while (WaitingtoAnalyse.Length > 5)
            {
                s = s + WaitingtoAnalyse.Substring(0, WaitingtoAnalyse.IndexOf("<"));
                WaitingtoAnalyse = WaitingtoAnalyse.Substring(WaitingtoAnalyse.IndexOf("<"));
                s = s + WaitingtoAnalyse.Substring(0, 3);
                WaitingtoAnalyse = WaitingtoAnalyse.Substring(WaitingtoAnalyse.IndexOf(">"));
            }
            return s;
        }

        public static string Take(this string source, int Count)
        {
            if (source.Length <= Count)
            {
                return source;
            }
            else
            {
                return source.Substring(0, Count - 3) + "...";
            }
        }
        public static string PurifyString(this string souce)
        {
            souce = souce.Replace("td", "");
            souce = souce.Replace("tr", "");
            souce = souce.Replace("/", "");
            souce = souce.Replace("\"", "");
            souce = souce.Replace(">", "");
            souce = souce.Replace("<", "");
            souce = souce.Replace("=", "");
            souce = souce.Replace("-", "");
            souce = souce.Replace("height28", "");
            souce = souce.Replace("aligncenter", "");
            souce = souce.Replace("colorrow", "");
            souce = souce.Replace("class", "");
            souce = souce.Replace("Next", "");
            souce = souce.Replace("nowrap", "");
            souce = souce.Replace("&nbsp;", "");
            souce = souce.Trim();
            return souce;
        }
        public static string RemoveHTML(this string Content)
        {
            string s = string.Empty;
            Content = HttpUtility.HtmlDecode(Content?.ToString() ?? string.Empty);
            if((!Content.Contains("<"))&&(!Content.Contains(">")))
            {
                return Content;
            }
            while (Content.Length > 5)
            {
                s = s + Content.Substring(0, Content.IndexOf("<"));
                Content = Content.Substring(Content.IndexOf(">")).Substring(1);
            }
            return HttpUtility.HtmlDecode(s);
        }
        public static string AESEncrypt(this string toEncrypt)
        {
            if (string.IsNullOrEmpty(toEncrypt))
                return string.Empty;
            byte[] keyArray = Encoding.UTF8.GetBytes(Secrets.AESKey);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }
        public static string AESDecrypt(this string toDecrypt)
        {
            if (string.IsNullOrEmpty(toDecrypt))
                return string.Empty;
            byte[] keyArray = Encoding.UTF8.GetBytes(Secrets.AESKey);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        public static string MD5Encrypt(this string toEncrypt)
        {
            byte[] result = Encoding.Default.GetBytes(toEncrypt.Trim());    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }

}