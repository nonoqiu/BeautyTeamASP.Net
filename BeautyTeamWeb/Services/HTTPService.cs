using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BeautyTeamWeb.Services
{
    public class HTTPService : object
    {
        public CookieContainer cc = new CookieContainer();
        public async Task<string> SendDataByPostAsync(string Url, string postDataStr, string Decode = "utf-8")
        {
            var request = WebRequest.CreateHttp(Url);
            if (cc.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cc = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cc;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            var myRequestStream = await request.GetRequestStreamAsync();
            var myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("GB2312"));
            await myStreamWriter.WriteAsync(postDataStr);
            myStreamWriter.Close();
            var response = await request.GetResponseAsync();
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(Decode));
            string retString = await myStreamReader.ReadToEndAsync();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        public async Task<string> SendDataByGETAsync(string Url, string Coding = "utf-8")
        {
            var request = WebRequest.CreateHttp(Url);
            if (cc.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cc = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cc;
            }
            request.Method = "GET";
            request.ContentType = "text/html;charset=" + Coding;
            var response = await request.GetResponseAsync();
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(Coding));
            string retString = await myStreamReader.ReadToEndAsync();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        public async Task<Bitmap> GetBitMapAsync(string Url)
        {
            var request = WebRequest.CreateHttp(Url);
            if (cc.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cc = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cc;
            }
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            var response = await request.GetResponseAsync();
            return new Bitmap(Image.FromStream(response.GetResponseStream()));
        }
    }
}