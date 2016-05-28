using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BeautyTeamWeb.Models;
using System.Drawing;
using System.Web.Mvc;
using BeautyTeamWeb.Services;

namespace BeautyTeamWeb.Services
{
    public static class SchoolService
    {
        public static List<School> Schools { get; set; } = new List<School>();
        static SchoolService()
        {
            Schools?.Add(new NEU { SchoolId = 1 });
            Schools?.Add(new LNU { SchoolId = 2 });
        }
        public static School FindSchoolById(int? id)
        {
            if(id==null)
            {
                return null;
            }
            var sc= Schools?.Find(t => t.SchoolId == id);
            if(sc==null)
            {
                throw new Exception();
            }
            return sc;
        }
    }
    public class Schedule
    {
        public Schedule()
        {
            Classes = new Dictionary<DayOfWeek, List<Class>>();
            for (int i = 0; i < 7; i++)
            {
                Classes.Add((DayOfWeek)i, new List<Class>());
            }
        }
        public Dictionary<DayOfWeek, List<Class>> Classes { get; set; }
        public class Class
        {
            public virtual string ClassDes { get; set; }
        }
    }
    public class MarkList
    {
        public string GPA { get; set; }
        public List<Mark> markList { get; set; }
    }
    public class Mark
    {
        public string ExamType { get; set; }
        public string FinalScore { get; set; }
        public string ScoreEnd { get; set; }
        public string ScoreMiddle { get; set; }
        public string ScoreType { get; set; }
        public string ScoreWhenLearning { get; set; }
        public string StudyScore { get; set; }
        public string StudyTime { get; set; }
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectType { get; set; }
    }
    public abstract class School
    {
        public int SchoolId { get; set; }
        protected abstract string ServerAddress { get; set; }
        public abstract string SchoolName { get; }
        public abstract Task<bool> IsCorrectAccount(string Account, string Password, HTTPService HTTP = null);
        public abstract Task<Schedule> GetSchedule(string Account, string Password, int Term);
        public abstract Task<MarkList> GetGrade(string Account, string Password, int Term);
    }
    public class NEU : School
    {
        protected override string ServerAddress { get; set; } = @"http://202.118.31.197/";
        public override string SchoolName => "东北大学";

        public override async Task<bool> IsCorrectAccount(string Account, string Password,  HTTPService HTTP = null)
        {
            HTTP = HTTP ?? new HTTPService();
            string response = string.Empty;
            do
            {
                var codeMap = await HTTP.GetBitMapAsync(ServerAddress + "ACTIONVALIDATERANDOMPICTURE.APPPROCESS");
                var Result = "Result";
                var Target = ServerAddress + "ACTIONLOGON.APPPROCESS?mode=4";
                response = await HTTP.SendDataByPostAsync(Target, $@"WebUserNO={
                HttpUtility.UrlEncode(Account)}&Password={
                HttpUtility.UrlEncode(Password)}&Agnomen={
                HttpUtility.UrlEncode(Result)}&submit.x=20&submit.y=9", "GB2312");
                throw new Exception();
            }
            while (response.Contains("请输入正确的附加码"));
        }

        public override async Task<Schedule> GetSchedule(string Account, string Password, int Term)
        {
            var HTTP = new HTTPService();
            if (await IsCorrectAccount(Account, Password, HTTP))
            {
                var SourceSchedule = await HTTP.SendDataByPostAsync(ServerAddress + "ACTIONQUERYSTUDENTSCHEDULEBYSELF.APPPROCESS", "YearTermNO=" + Term, "GB2312");
                return AnalyseSchedule(SourceSchedule);
            }
            else
            {
                throw new Exception();
            }
        }
        private Schedule AnalyseSchedule(string Source)
        {
            Source = Source.Substring(Source.IndexOf("</colgroup>"));
            string s = Source.SimplifyHTML();
            string[] strArray = HttpUtility.HtmlDecode(s).Replace("<br>", " ").Replace("</t>", " ").Replace("\r\n", " ")
            .Split(new string[] { "<tr>" }, StringSplitOptions.RemoveEmptyEntries);
            var schedule = new Schedule();
            for (int i = 4; i <= 9; i++)
            {
                string[] strArray2 = strArray[i].Split(new string[] { "<td>" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j <= 6; j++)
                {
                    schedule.Classes[(DayOfWeek)(j == 6 ? 0 : j + 1)].Add(new Schedule.Class
                    {
                        ClassDes = strArray2[j + 2].Replace(" ", " ").Replace(" ", "")
                    });
                }
            }
            return schedule;
        }

        public override async Task<MarkList> GetGrade(string Account, string Password, int Term)
        {
            var HTTP = new HTTPService();
            if (await IsCorrectAccount(Account, Password, HTTP))
            {
                var QueryResult = await HTTP.SendDataByPostAsync("http://202.118.31.197/ACTIONQUERYSTUDENTSCORE.APPPROCESS", "YearTermNO=14", "GB2312");
                return AnalyseMarkList(QueryResult);
            }
            else
            {
                throw new Exception();
            }
        }
        private MarkList AnalyseMarkList(string anaSource)
        {
            int num4;
            MarkList result = new MarkList();
            anaSource = anaSource.Substring(anaSource.IndexOf("平均学分绩点："));
            result.GPA = anaSource.Substring(0, 13);
            anaSource = anaSource.Substring(anaSource.IndexOf("总成绩"));
            string[] separator = new string[] { "</tr>" };
            string[] strArray = anaSource.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<Mark>();
            for (int i = 0; i < (strArray.Length - 6); i = num4 + 1)
            {
                if (!strArray[i].Contains("总成绩"))
                {
                    Mark item = new Mark();
                    string[] textArray2 = new string[] { "</td>" };
                    string[] strArray2 = strArray[i].Split(textArray2, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < 11; j = num4 + 1)
                    {
                        string str = strArray2[j].PurifyString();
                        switch (j)
                        {
                            case 0:
                                item.SubjectType = str;
                                break;
                            case 1:
                                item.SubjectID = str;
                                break;
                            case 2:
                                item.SubjectName = str;
                                break;
                            case 3:
                                item.ExamType = str;
                                break;
                            case 4:
                                item.StudyTime = str;
                                break;
                            case 5:
                                item.StudyScore = str;
                                break;
                            case 6:
                                item.ScoreType = str;
                                break;
                            case 7:
                                item.ScoreWhenLearning = str;
                                break;
                            case 8:
                                item.ScoreMiddle = str;
                                break;
                            case 9:
                                item.ScoreEnd = str;
                                break;
                            case 10:
                                item.FinalScore = str;
                                break;
                            default:
                                item = new Mark();
                                break;
                        }
                        num4 = j;
                    }
                    list.Add(item);
                }
                num4 = i;
            }
            result.markList = list;
            return result;
        }
    }

    public class LNU : School
    {
        public override string SchoolName => "辽宁大学";
        protected override string ServerAddress { get; set; } = @"http://jwgl.lnu.edu.cn";

        public async override Task<MarkList> GetGrade(string Account, string Password, int Term)
        {
            var HTTP = new HTTPService();
            if (await IsCorrectAccount(Account, Password,  HTTP))
            {
                var Response = await HTTP.SendDataByGETAsync(ServerAddress + "/pls/wwwbks/bkscjcx.curscopre", "GB2312");

                Response = Response.SimplifyHTML();
                var result = Response.Split(new string[] { @"<TR>" }, StringSplitOptions.RemoveEmptyEntries);
                var finalresult = new MarkList { markList = new List<Mark>() };
                for (int i = 1; i < result.Length; i++)
                {
                    var line = result[i].Split(new string[] { @"</t>" }, StringSplitOptions.RemoveEmptyEntries);
                    finalresult.markList.Add(new Mark()
                    {
                        StudyScore = line[6].RemoveHTML(),
                        SubjectName = line[2].RemoveHTML()
                    });
                }
                return finalresult;
            }
            else
            {
                throw new Exception();
            }
        }

        public async override Task<Schedule> GetSchedule(string Account, string Password, int Term)
        {
            var HTTP = new HTTPService();
            if (await IsCorrectAccount(Account, Password,  HTTP))
            {
                var Response = await HTTP.SendDataByGETAsync(ServerAddress + "/pls/wwwbks/xk.CourseView", "GB2312");
                Response = @"\n<td><p ><st>" + Response.Substring(Response.IndexOf("第一节"));
                Response = Response.Substring(0, Response.IndexOf("<BR>"));
                Response = Response.SimplifyHTML();
                Response = HttpUtility.HtmlDecode(Response);
                var Lines = Response.Split(new string[] { "<TR>" }, StringSplitOptions.RemoveEmptyEntries);
                var Sche = new Schedule();
                for (int i = 0; i < Lines.Length; i++)
                {
                    var Cells = Lines[i].Split(new string[] { "<td>" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j <= 8; j++)
                    {
                        if (j == 0 || j == 1)
                            continue;
                        Sche.Classes[(DayOfWeek)(j == 8 ? 0 : j - 1)].Add(new Schedule.Class
                        {
                            ClassDes = Cells[j].RemoveHTML()
                        });
                    }
                }
                return Sche;
            }
            else
            {
                throw new Exception();
            }
        }

        public override async Task<bool> IsCorrectAccount(string Account, string Password, HTTPService HTTP = null)
        {
            HTTP = HTTP ?? new HTTPService();
            var response = await HTTP.SendDataByPostAsync(ServerAddress + @"/pls/wwwbks/bks_login2.login", $@"stuid={
                HttpUtility.UrlEncode(Account)}&pwd={
                HttpUtility.UrlEncode(Password)}", "GB2312");
            return response.Contains("登录成功!");
        }
    }

    public class SYUCT : School
    {
        public override string SchoolName => "沈阳化工大学";

        protected override string ServerAddress { get; set; } = "";


        public override Task<MarkList> GetGrade(string Account, string Password, int Term)
        {
            throw new NotImplementedException();
        }

        public override Task<Schedule> GetSchedule(string Account, string Password, int Term)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> IsCorrectAccount(string Account, string Password,  HTTPService HTTP = null)
        {
            throw new NotImplementedException();
        }
    }
}