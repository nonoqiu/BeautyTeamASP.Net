using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace BeautyTeamWeb.Services
{
    public class OtherService
    {
        public static async Task<Peccancy> QueryPeccancy(string City, string CarNum, string EngineNum, string VIN)
        {
            var HTTP = new HTTPService();
            var Result = await HTTP.SendDataByGETAsync($@"https://sp0.baidu.com/" +
            $@"5LMDcjW6BwF3otqbppnN2DJv/traffic.pae.baidu.com/data/query?city={
            HttpUtility.UrlEncode(City)}&hphm={
            HttpUtility.UrlEncode(CarNum)}&engine={
            HttpUtility.UrlEncode(EngineNum)}&body={
            HttpUtility.UrlEncode(VIN)}");
            var JResult= JsonConvert.DeserializeObject<Peccancy>(Result); 
            foreach(var T in JResult.data.lists)
            {
                T.PointCost = T.point;
            }
            return JResult;
        }
        public class Peccancy
        {
            public virtual string msg { get; set; }
            public virtual int status { get; set; }
            public virtual PeccancySon data { get; set; }
        }
        public class PeccancySon
        {
            public virtual string hphm { get; set; }
            public virtual string city { get; set; }
            public virtual string city_name { get; set; }
            public virtual List<OnePeccancy> lists { get; set; }
        }
        public class OnePeccancy
        {
            [JsonProperty("Address")]
            public virtual string address { get; set; }
            [JsonIgnore]
            public virtual string agency { get; set; }
            [JsonProperty("Fine")]
            public virtual int fine { get; set; }
            [JsonIgnore]
            public virtual int handled { get; set; }
            [JsonIgnore]
            public virtual double latitude { get; set; }
            [JsonIgnore]
            public virtual double longitude { get; set; }
            public virtual int point { get; set; }
            public virtual int PointCost { get; set; }
            public virtual DateTime time { get; set; }
            public virtual string violation_type { get; set; }
            [JsonIgnore]
            public virtual DateTime date { get; set; }
        }
        public static async Task<Insurance> QueryInsurance(int CarPrice, int CarYears = 2016)
        {
            var HTTP = new HTTPService();
            var Result = await HTTP.SendDataByPostAsync("http://www.epicc.com.cn/ecar/caculate/quotedPrice",
                $"areaCode=21000000&CarPrice={CarPrice.ToString()}&CarYears={CarYears.ToString()}&citycode=21000000");
            return JsonConvert.DeserializeObject<Insurance>(Result);
        }
        public class Insurance
        {
            public virtual string eco { get; set; }
            public virtual string com { get; set; }
            //[JsonIgnore]
            public virtual string ecomaket { get; set; }
            //[JsonIgnore]
            public virtual string commaket { get; set; }
        }
    }
}