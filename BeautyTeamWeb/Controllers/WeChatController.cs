using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyTeamWeb.Services;
using static BeautyTeamWeb.Services.WeChatService;
using System.Threading.Tasks;
using BeautyTeamWeb.ViewModels;

namespace BeautyTeamWeb.Controllers
{
    public class WeChatController : ControllerWithAuthorize
    {
        public async Task<ActionResult> Bind(string openid)
        {
            ViewData["Title"] = "绑定账号 - Obisoft";
            ViewData["Schools"] = SchoolService.Schools;
            var WCUser = await UserInfomationAsync(openid, await AccessTokenAsync());
            return View(WCUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Bind(Bind model)
        {
            var WCUser = await UserInfomationAsync(model.openid, await AccessTokenAsync());
            var School = SchoolService.FindSchoolById(model.school);
            ViewBag.Schools = SchoolService.Schools;
            if (ModelState.IsValid && School != null)
            {
                var FUser = await UserManager.FindByEmailAsync(model.email);
                //Wrong Password
                if (!await School.IsCorrectAccount(model.aaoaccount, model.aaopass))
                {
                    ViewBag.ErrorMessage = "教务处密码错误!";
                    return View(WCUser);
                }
                else
                {
                    foreach(var user in UserManager.Users.ToList().Where(t=>t.openid== WCUser.openid))
                    {
                        user.openid = null;
                        await UserManager.UpdateAsync(user);
                    }
                    //Already an Obisoft User, Update him.
                    if (FUser != null)
                    {
                        FUser.SchoolId = model.school;
                        FUser.SchoolAccount = model.aaoaccount;
                        FUser.SchoolPassAes = model.aaopass.AESEncrypt();
                        FUser.openid = WCUser.openid;
                        FUser.NickName = WCUser.nickname;
                        await UserManager.UpdateAsync(FUser);
                        return RedirectToAction("Schedule", new { openid = model.openid });
                    }
                    //Not an Obisoft User, Register him
                    else
                    {
                        await UserManager.CreateAsync(new Models.ObisoftUser
                        {
                            UserName = model.email,
                            Email = model.email,
                            Description = "From WeChat",
                            openid = WCUser.openid,
                            SchoolId = model.school,
                            SchoolAccount = model.aaoaccount,
                            SchoolPassAes = model.aaopass.AESEncrypt(),
                            IconImage = WCUser.headimgurl,
                            NickName = WCUser.nickname
                        }, model.aaopass);
                        return RedirectToAction("Schedule", new { openid = model.openid });
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "输入的信息不合法!";
                return View(WCUser);
            }
        }
    }
}