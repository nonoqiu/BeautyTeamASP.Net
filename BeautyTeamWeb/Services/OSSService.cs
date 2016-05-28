using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace BeautyTeamWeb.Services
{
    public static class OSSService
    {
        public static async Task<string> Upload(string fileName, string LocalPath, bool UnCompressed, bool HTTPS)
        {
            string Target = HTTPS ? "https" : "http";
            OssClient client = new OssClient(
            Secrets.endpoint,
            Secrets.accessKeyId,
            Secrets.accessKeySecret);
            await Task.Run(() =>
            {
                client.PutObject("obisoft", fileName, LocalPath);
            });
            Target += @"://obisoft.img-cn-beijing.aliyuncs.com/" + fileName + (UnCompressed ? string.Empty : @"@!ok");
            return Target;
        }
        public static string CreatePasswordHash(this string pwd, int saltLenght)
        {
            string strSalt = saltLenght.CreateSalt();
            string saltAndPwd = string.Concat(pwd, strSalt);
#pragma warning disable CS0618
            string hashenPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
#pragma warning restore CS0618
            return hashenPwd.ToLower().Substring(0, 16);
        }
        private static string CreateSalt(this int saltLenght)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltLenght];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}