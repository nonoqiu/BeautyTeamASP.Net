using System.Web;
using System.Web.Mvc;
using BeautyTeamWeb.Models;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using BeautyTeamWeb.ViewModels;
using System.IO;
using System;
using BeautyTeamWeb.Services;
using System.Collections.Generic;
using System.Linq;
using static BeautyTeamWeb.Services.WeChatService;

namespace BeautyTeamWeb.Controllers
{
    [RequireHttps]
    public class ApiController : ControllerWithAuthorize
    {
        #region Account Management API
        // GET: /api
        public async Task<string> Index()
        {
            return await this._apiReplyTool(async () =>
            {
                return await MessageAsync("Welcome to BeautyTeam API!", HttpStatusCode.OK);
            });
        }

        [HttpPost]
        // POST: /api/Log
        public async Task<string> Log(EventLog eventLog)
        {
            return await this._apiReplyTool(async () =>
            {
                //Create Database Model
                var log = new EventLogDb(eventLog);
                DbContext.EventLogDbs.Add(log);
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        [HttpPost]
        // POST: /api/Login
        public async Task<string> Login(LoginViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe ?? false, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return await MessageAsync("Success", HttpStatusCode.OK);
                    case SignInStatus.LockedOut:
                        return await MessageAsync("LockedOut", HttpStatusCode.Forbidden);
                    case SignInStatus.RequiresVerification:
                        return await MessageAsync("RequiresVerification", HttpStatusCode.Redirect);
                    case SignInStatus.Failure:
                    default:
                        return await MessageAsync("Failure", HttpStatusCode.Forbidden);
                }
            });
        }

        [HttpPost]
        // POST: /api/ForgotPassword
        public async Task<string> ForgotPassword(ForgotPasswordViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                //Not Found
                if (user == null)
                {
                    return NotFoundResult;
                }
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", $"Please reset your password by clicking <a href=\"{callbackUrl}\">here</a>");
                return OkResult;
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/VerifyEmail
        public async Task<string> VerifyEmail()
        {
            return await this._apiReplyTool(async () =>
            {
                var user = await userAsync();
                //Already Confirmed
                if (user.EmailConfirmed)
                {
                    return ForbiddenResult;
                }
                //Send Email
                else
                {
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                }
                return OkResult;
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/ChangePassword
        public async Task<string> ChangePassword(ChangePasswordViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return OkResult;
                }
                //Wrong password
                return ForbiddenResult;
            });
        }

        [ApiAuthorize]
        // GET: /api/CurrentUser
        public async Task<string> CurrentUser()
        {
            return await this._apiReplyTool(async () =>
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                return await _JsongAsync(new ObiObject<ObisoftUser> { Object = user, StatusCode = HttpStatusCode.OK });
            });
        }

        // GET: /api/AnotherUser
        public async Task<string> AnotherUser(string id/*User Id*/)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await UserManager.FindByIdAsync(id);
                //Not Found
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //Return
                var Result = new AnotherUser
                {
                    Id = Target.Id,
                    Description = Target.Description,
                    Email = Target.Email,
                    RealName = Target.RealName,
                    IconImage = Target.IconImage,
                    NickName = Target.NickName,
                    UserName = Target.UserName
                };
                return await _JsongAsync(new ObiObject<AnotherUser> { Object = Result, StatusCode = HttpStatusCode.OK });
            });
        }

        [HttpPost]
        // POST: /api/Register
        public async Task<string> Register(RegisterViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var user = new ObisoftUser { UserName = model.Email, Email = model.Email, NickName = model.Email.Split('@')[0] };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return OkResult;
                }
                return ConflictSetResult;
            });
        }

        // GET: /api/loginstatus
        public async Task<string> loginstatus()
        {
            return await this._apiReplyTool(async () =>
            {
                bool status = User.Identity.IsAuthenticated;
                return await _JsongAsync(new ObiValue<bool> { StatusCode = HttpStatusCode.OK, Value = status });
            });
        }

        [HttpPost]
        // POST: /api/logoff
        public async Task<string> logoff()
        {
            return await this._apiReplyTool(async () =>
            {
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                await System.Threading.Tasks.Task.Delay(0);
                return OkResult;
            });
        }

        // GET: /api/latestversion?Platform=android&CurrentVersion=0.0.1
        public async Task<string> latestversion(VersionCheckModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                string LatestVersion = string.Empty;
                string LatestVersionDescription = string.Empty;
                switch (model.Platform.Trim().ToLower())
                {
                    case "android":
                        LatestVersion = ConfigurationManager.AppSettings["AndroidVersion"];
                        LatestVersionDescription = ConfigurationManager.AppSettings["AndroidVersionDes"];
                        break;
                    case "ios":
                        LatestVersion = ConfigurationManager.AppSettings["iOSVersion"];
                        LatestVersionDescription = ConfigurationManager.AppSettings["iOSVersionDes"];
                        break;
                    case "uwp":
                        LatestVersion = ConfigurationManager.AppSettings["UWPVersion"];
                        LatestVersionDescription = ConfigurationManager.AppSettings["UWPVersionDes"];
                        break;
                    case "wpf":
                        LatestVersion = ConfigurationManager.AppSettings["WPFVersion"];
                        LatestVersionDescription = ConfigurationManager.AppSettings["WPFVersionDes"];
                        break;
                    default:
                        return NotFoundResult;
                }
                var result = new VersionCheckResult
                {
                    StatusCode = HttpStatusCode.OK,
                    IsLatest = LatestVersion.Trim().ToLower() == (model?.CurrentVersion?.Trim()?.ToLower() ?? string.Empty),
                    LatestVersion = LatestVersion,
                    LatestVersionDescription = LatestVersionDescription
                };
                return await _JsongAsync(result);
            });
        }

        // GET: /api/FindUserByEmail?Email=mma%40obisoft.com.cn
        public async Task<string> FindUserByEmail([DataType(DataType.EmailAddress)]string Email)
        {
            return await this._apiReplyTool(async () =>
            {
                var user = await UserManager.FindByEmailAsync(Email);
                if (user == null)
                {
                    return NotFoundResult;
                }
                return await _JsongAsync(new ObiObject<string>
                {
                    Object = user.Id,
                    StatusCode = HttpStatusCode.OK
                });
            });
        }

        [ApiAuthorize]
        // POST: /api/SetBasicInfo
        public async Task<string> SetBasicInfo(BasicInfoViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                cuser.PhoneNumber = model.PhoneNumber ?? cuser.PhoneNumber;
                cuser.NickName = model.NickName ?? cuser.NickName;
                cuser.RealName = model.RealName ?? cuser.RealName;
                cuser.IconImage = model.IconImage ?? cuser.IconImage;
                cuser.Description = model.Description ?? cuser.Description;
                await UserManager.UpdateAsync(cuser);
                return OkResult;
            });
        }

        // GET: /api/_Unauthorized
        public async Task<string> _Unauthorized()
        {
            return await _JsongAsync(new ServerReply { StatusCode = HttpStatusCode.Unauthorized });
        }

        [HttpPost]
        // POST: /api/UploadImage
        public async Task<string> UploadImage(UploadImageViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var file = Request.Files["file"];
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                var result = string.Empty;
                if (file != null)
                {
                    if (Directory.Exists(Server.MapPath("~/TempUpload")) == false)
                        Directory.CreateDirectory(Server.MapPath("~/TempUpload"));
                    var fileName = file.FileName.CreatePasswordHash(4) + fileExtension;
                    var virpath = HttpContext.Server.MapPath("~/TempUpload") + @"\" + fileName;
                    file.SaveAs(virpath);
                    result = await OSSService.Upload(fileName, virpath, model.UnCompressed, model.HTTPS);
                }
                return await MessageAsync(result, HttpStatusCode.OK); ;
            });
        }
        #endregion

        #region School Management API
        // GET: /api/SchoolList
        public async Task<string> SchoolList()
        {
            return await this._apiReplyTool(async () =>
            {
                var List = SchoolService.Schools;
                return await _JsongAsync(new ObiList<School>
                {
                    List = List,
                    StatusCode = HttpStatusCode.OK
                });
            });
        }

        // GET: /api/SchoolDetail/1
        public async Task<string> SchoolDetail(int? id)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = SchoolService.FindSchoolById(id);
                if (Target == null)
                {
                    return NotFoundResult;
                }
                return await _JsongAsync(new ObiObject<School>
                {
                    Object = Target,
                    StatusCode = HttpStatusCode.OK
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/BindSchool/1
        public async Task<string> BindSchool(int? id)
        {
            return await this._apiReplyTool(async () =>
            {
                if (SchoolService.FindSchoolById(id) == null)
                {
                    return NotFoundResult;
                }
            (await userAsync()).SchoolId = id;
                (await userAsync()).SchoolAccount = string.Empty;
                (await userAsync()).SchoolPassAes = string.Empty;
                await UserManager.UpdateAsync(await userAsync());
                return OkResult;
            });
        }

        // POST: /api/SetSchoolAccount
        [HttpPost]
        [ApiAuthorize]
        public async Task<string> SetSchoolAccount(SetSchoolAccount model)
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                if (cuser.SchoolBinded)
                {
                    if (await cuser.UserSchool().IsCorrectAccount(model.Account, model.Password))
                    {
                        cuser.SchoolAccount = model.Account;
                        cuser.SchoolPassAes = model.Password.AESEncrypt();
                        await UserManager.UpdateAsync(cuser);
                        return OkResult;
                    }
                    else
                    {
                        //Wrong Password
                        return await _JsongAsync(new ServerReply { StatusCode = HttpStatusCode.BadRequest });
                    }
                }
                else
                {
                    //Not Binded
                    return await _JsongAsync(new ServerReply { StatusCode = HttpStatusCode.Forbidden });
                }
            });
        }
        #endregion

        #region Team Management API
        [ApiAuthorize]
        // GET: /api/StatisticInfo
        public async Task<string> StatisticInfo()
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                int PeopleYouLeading = 0;
                cuser.GU_Relations.Where(t => (short)t.RelationType <= 3).ToList().ForEach((GU_Relation t) => PeopleYouLeading += t.Group.GU_Relations.Count - 1);
                int TaskFinished = cuser.TU_Relations.Where(t => t.GroupTask.DeadLine < DateTime.Now).Count();
                var Model = new TeamStatisticInfo
                {
                    Joined = cuser.GU_Relations.Count(),
                    Owned = cuser.GU_Relations.Where(t => t.RelationType == GU_RelationType.Owner).Count(),
                    Admined = cuser.GU_Relations.Where(t => (short)t.RelationType <= 3).Count(),
                    Teams = cuser.GU_Relations.Where(t => t.Group is Team).Count(),
                    InfoStations = cuser.GU_Relations.Where(t => t.Group is RadioStation).Count(),
                    PeopleYouLeading = PeopleYouLeading,
                    TaskFinished = TaskFinished
                };
                return await _JsongAsync(new ObiObject<TeamStatisticInfo>
                {
                    Object = Model,
                    StatusCode = HttpStatusCode.OK
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/CreateTeam
        public async Task<string> CreateTeam(CreateTeamModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var newTeam = new Team
                {
                    GroupName = model.TeamName,
                    GroupDescription = model.TeamDescription,
                    CreateTime = DateTime.Now,
                    GroupType = model.GroupType
                };
                DbContext.Groups.Add(newTeam);
                await DbContext.SaveChangesAsync();
                //Set him as the owner
                DbContext.GU_Relation.Add(new GU_Relation
                {
                    ObisoftUserId = User.Identity.GetUserId(),
                    GroupId = newTeam.GroupId,
                    RelationType = GU_RelationType.Owner
                });
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = newTeam.GroupId
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/CreateRadioStation
        public async Task<string> CreateRadioStation(CreateRadioStationModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var newRadioStation = new RadioStation
                {
                    GroupName = model.RadioStationName,
                    GroupDescription = model.RadioStationDescription,
                    CreateTime = DateTime.Now,
                    GroupType = model.GroupType
                };
                DbContext.Groups.Add(newRadioStation);
                await DbContext.SaveChangesAsync();
                //Set him as the owner
                DbContext.GU_Relation.Add(new GU_Relation
                {
                    ObisoftUserId = User.Identity.GetUserId(),
                    GroupId = newRadioStation.GroupId,
                    RelationType = GU_RelationType.Owner
                });
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = newRadioStation.GroupId
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/DeleteGroup
        public async Task<string> DeleteGroup(int? id)//Group Id
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Groups.FindAsync(id);
                //Not Found the group
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //User is not the owner.
                var MyRelation = Target.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                if (MyRelation == null || MyRelation.RelationType != GU_RelationType.Owner)
                {
                    return ForbiddenResult;
                }
                DbContext.Groups.Remove(Target);
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/JoinGroup/5
        public async Task<string> JoinGroup(int? id)//Group Id
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Groups.FindAsync(id);
                //Not found the team.
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //Already have some relation
                else if (Target.GU_Relations.Exists(t => t.ObisoftUserId == User.Identity.GetUserId()))
                {
                    return ConflictSetResult;
                }
                else
                {
                    //Join the Group
                    DbContext.GU_Relation.Add(new GU_Relation
                    {
                        ObisoftUserId = User.Identity.GetUserId(),
                        GroupId = Target.GroupId,
                        RelationType = GU_RelationType.Member
                    });
                    await DbContext.SaveChangesAsync();
                    return OkResult;
                }
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/LeaveGroup
        public async Task<string> LeaveGroup(int? id)//Gourp Id
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Groups.FindAsync(id);
                //Not Found the team.
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //Do Not have some relation
                var MyRelation = Target.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                if (MyRelation == null)
                {
                    return ConflictSetResult;
                }
                //If he is the owner, destroy the group.
                else if (MyRelation.RelationType == GU_RelationType.Owner)
                {
                    DbContext.Groups.Remove(Target);
                }
                //Found relation and going to delete.
                DbContext.GU_Relation.Remove(MyRelation);
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        [ApiAuthorize]
        // GET: /api/Groupsijoined
        public async Task<string> Groupsijoined()
        {
            return await this._apiReplyTool(async () =>
            {
                var Result = new List<GU_RelationR>();
                (await userAsync()).GU_Relations.ForEach((GU_Relation t) =>Result.Add(new GU_RelationR(t)));
                return await _JsongAsync(new ObiList<GU_RelationR>
                {
                    List = Result,
                    StatusCode = HttpStatusCode.OK
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/SetAdmin
        public async Task<string> SetAdmin(int? id/*Group Id*/, SetGroupAdminModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Groups.FindAsync(id);
                //Group Not Found or User not found
                if (Target == null || UserManager.FindById(model.TargetUserId) == null)
                {
                    return NotFoundResult;
                }

                //Not Authorized, I am not the owner.
                var MyRelation = Target.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                if (MyRelation == null || MyRelation.RelationType != GU_RelationType.Owner)
                {
                    return ForbiddenResult;
                }

                //He is already an Owner or He is already an admin
                var TRelation = Target.GU_Relations.Find(t => t.ObisoftUserId == model.TargetUserId);
                if (TRelation == null || (TRelation.RelationType == GU_RelationType.Owner || TRelation.RelationType == GU_RelationType.Admin))
                {
                    return ConflictSetResult;
                }

                //Change the relation
                TRelation.RelationType = GU_RelationType.Admin;
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        // GET: /api/GroupDetails
        public async Task<string> GroupDetails(int? id)//Group Id
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Groups.FindAsync(id);
                //Group Not Found
                if (Target == null)
                {
                    return NotFoundResult;
                }
                return await _JsongAsync(new ObiObject<Group>
                {
                    Object = Target,
                    StatusCode = HttpStatusCode.OK
                });
            });
        }

        [ApiAuthorize]
        // GET: api/TeamProjects
        public async Task<string> TeamProjects(int? id)//Team Id
        {
            return await this._apiReplyTool(async () =>
            {
                var TargetTeam = await DbContext.Groups.FindAsync(id) as Team;
                if (TargetTeam == null)
                {
                    return NotFoundResult;
                }
                //Not Authorized
                var MyRelation = TargetTeam.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                if (MyRelation == null)
                {
                    return ForbiddenResult;
                }
                return await _JsongAsync(new ObiList<Project>
                {
                    StatusCode = HttpStatusCode.OK,
                    List = TargetTeam.GroupProjects
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/BootUser
        public async Task<string> BootUser(int? id/*GroupId*/, BootUserModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Groups.FindAsync(id);

                //Group Not Found or User not found or I just did not join the group or he did not join the group.
                if (Target == null || UserManager.FindById(model.TargetUserId) == null)
                {
                    return NotFoundResult;
                }

                //Not Authorized, I am not the owner and I am not the admin
                var MyRelation = Target.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                if (MyRelation == null || (MyRelation.RelationType != GU_RelationType.Owner && MyRelation.RelationType != GU_RelationType.Admin))
                {
                    return ForbiddenResult;
                }

                //I am not the owner and I want to kick an admin
                var TRelation = Target.GU_Relations.Find(t => t.ObisoftUserId == model.TargetUserId);
                if (TRelation == null || (MyRelation.RelationType != GU_RelationType.Owner && TRelation.RelationType == GU_RelationType.Admin))
                {
                    return ConflictSetResult;
                }

                //I want to boot myself
                if (TRelation.GU_RelationId == MyRelation.GU_RelationId)
                {
                    return ConflictSetResult;
                }

                //Change the relation
                DbContext.GU_Relation.Remove(TRelation);
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        [ApiAuthorize]
        // GET: /api/SearchGroup
        public async Task<string> SearchGroup([Required]string keyword, bool? isTeamNotRadioStation)
        {
            return await this._apiReplyTool(async () =>
            {
                List<Group> result = null;
                if (isTeamNotRadioStation == true)
                {
                    result = DbContext.Groups.Where(t => t.GroupName.Contains(keyword) && t is Team).ToList();
                }
                else
                {
                    result = DbContext.Groups.Where(t => t.GroupName.Contains(keyword) && t is RadioStation).ToList();
                }
                return await _JsongAsync(new ObiList<Group>
                {
                    StatusCode = HttpStatusCode.OK,
                    List = result
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/CreateProject
        public async Task<string> CreateProject(int? id/*Team (Group) Id*/, CreateProjectModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Groups.FindAsync(id) as Team;
                //Can not find target team.
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //I am not an admin and I am not an owner.
                var MyRelation = Target.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                if (MyRelation == null || (MyRelation.RelationType != GU_RelationType.Owner && MyRelation.RelationType != GU_RelationType.Admin))
                {
                    return ForbiddenResult;
                }
                //Create new Project
                var NewProject = new Project
                {
                    GroupId = Target.GroupId,
                    ProjectDescription = model.ProjectDescription,
                    ProjectName = model.ProjectName,
                };
                Target.GroupProjects.Add(NewProject);
                await DbContext.SaveChangesAsync();
                //Return Project Id
                return await _JsongAsync(new ObiValue<int>
                {
                    Value = NewProject.ProjectId,
                    StatusCode = HttpStatusCode.OK
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/DeleteProject
        public async Task<string> DeleteProject(int? id/*Project Id*/)
        {
            return await this._apiReplyTool(async () =>
            {
                var TargetProject = await DbContext.Projects.FindAsync(id);
                //Not found this project
                if (TargetProject == null)
                {
                    return NotFoundResult;
                }
                //Unauthorized
                var TargetGroup = TargetProject.Group;
                var MyRelation = TargetGroup.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                //I am not the owner and I am not the admin
                if (MyRelation == null || (MyRelation.RelationType != GU_RelationType.Admin && MyRelation.RelationType != GU_RelationType.Owner))
                {
                    return ForbiddenResult;
                }
                DbContext.Projects.Remove(TargetProject);
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        [HttpPost]
        [ApiAuthorize]
#warning Not Implemented!!!!
        // POST: /api/CreateTeamTask
        public async Task<string> CreateTeamTask(int? id /*Project Id*/, CreatePersonalTaskModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var TargetProject = await DbContext.Projects.FindAsync(id);
                //Not found this project
                if (TargetProject == null)
                {
                    return NotFoundResult;
                }
                //Unauthorized
                var TargetGroup = TargetProject.Group;
                var MyRelation = TargetGroup.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                if (MyRelation == null)
                {
                    return ForbiddenResult;
                }
                return "";

            });
        }
        #endregion

        #region Personal Events And Tasks API
        [HttpPost]
        [ApiAuthorize]
        // POST: /api/CreatePersonalTask
        public async Task<string> CreatePersonalTask(CreatePersonalTaskModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                if (model.NoticeBefore.OutOfRange())
                {
                    return NotAcceptableResult;
                }
                var newPersonalTask = new PersonalTask
                {
                    DeadLine = model.DeadLine ?? DateTime.Now + new TimeSpan(days: 5, hours: 0, minutes: 0, seconds: 0),
                    Before = model.NoticeBefore ?? new TimeSpan(hours: 23, minutes: 59, seconds: 59),
                    ObisoftUserId = User.Identity.GetUserId(),
                    TaskName = model.TaskName,
                    TaskContent = model.TaskContent
                };
                DbContext.PersonalTasks.Add(newPersonalTask);
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = newPersonalTask.TaskId
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/CreatePersonalEvent
        public async Task<string> CreatePersonalEvent(CreatePersonalEventModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                if (model.NoticeBefore.OutOfRange())
                {
                    return NotAcceptableResult;
                }
                var newPersonalEvent = new PersonalEvent
                {
                    HappenTime = model.HappenTime ?? DateTime.Now + new TimeSpan(days: 5, hours: 0, minutes: 0, seconds: 0),
                    EndTime = model.EndTime ?? DateTime.Now + new TimeSpan(days: 6, hours: 0, minutes: 0, seconds: 0),
                    Before = model.NoticeBefore ?? new TimeSpan(hours: 0, minutes: 25, seconds: 0),
                    ObisoftUserId = User.Identity.GetUserId(),
                    EventContent = model.EventContent,
                    EventName = model.EventName
                };
                DbContext.PersonalEvents.Add(newPersonalEvent);
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = newPersonalEvent.EventId
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/DeletePersonalTask
        public async Task<string> DeletePersonalTask(int? id)//TaskId
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.PersonalTasks.FindAsync(id);
                if (Target == null)
                {
                    return NotFoundResult;
                }
                else if (Target.ObisoftUserId != User.Identity.GetUserId())
                {
                    return ForbiddenResult;
                }
                else
                {
                    DbContext.PersonalTasks.Remove(Target);
                    await DbContext.SaveChangesAsync();
                    return OkResult;
                }
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/DeletePersonalEvent
        public async Task<string> DeletePersonalEvent(int? id)//EventId
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.PersonalEvents.FindAsync(id);
                if (Target == null)
                {
                    return NotFoundResult;
                }
                else if (Target.ObisoftUserId != User.Identity.GetUserId())
                {
                    return ForbiddenResult;
                }
                else
                {
                    DbContext.PersonalEvents.Remove(Target);
                    await DbContext.SaveChangesAsync();
                    return OkResult;
                }
            });
        }

        [ApiAuthorize]
        // GET: /api/PersonalTaskDetails
        public async Task<string> PersonalTaskDetails(int? id)//Task Id
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.PersonalTasks.FindAsync(id);
                if (Target == null)
                {
                    return NotFoundResult;
                }
                else if (Target.ObisoftUserId != User.Identity.GetUserId())
                {
                    return ForbiddenResult;
                }
                else
                {
                    return await _JsongAsync(new ObiObject<PersonalTask>
                    {
                        Object = Target,
                        StatusCode = HttpStatusCode.OK
                    });
                }
            });
        }

        [ApiAuthorize]
        // GET: /api/PersonalEventDetails
        public async Task<string> PersonalEventDetails(int? id)//Event Id
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.PersonalEvents.FindAsync(id);
                if (Target == null)
                {
                    return NotFoundResult;
                }
                else if (Target.ObisoftUserId != User.Identity.GetUserId())
                {
                    return ForbiddenResult;
                }
                else
                {
                    return await _JsongAsync(new ObiObject<PersonalEvent>
                    {
                        Object = Target,
                        StatusCode = HttpStatusCode.OK
                    });
                }
            });
        }

        [ApiAuthorize]
        // GET: /api/AllNoticeForMe
        public async Task<string> AllNoticeForMe(int? Amount)
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                List<INoticeable> Notice = new List<INoticeable>();
                //Add all his PersonalTasks which deads later
                foreach (INoticeable t in cuser.PersonalTasks.Where(t => t.DeadLine > DateTime.Now))
                {
                    Notice.Add(t);
                }
                //Add all his PersonalEvents which happens later
                foreach (INoticeable e in cuser.PersonalEvents.Where(t => t.EndTime > DateTime.Now))
                {
                    Notice.Add(e);
                }
                //Add all his GroupTasks which deads later
                foreach (var TURelation in cuser.TU_Relations.Where(t => t.GroupTask.DeadLine > DateTime.Now))
                {
                    Notice.Add(TURelation.GroupTask);
                }
                //Add all his GroupEvents which happens later
                foreach (var EURelation in cuser.EU_Relations.Where(t => t.GroupEvent.EndTime > DateTime.Now))
                {
                    Notice.Add(EURelation.GroupEvent);
                }
                //For all his radiostations he joined
                foreach (var Group in cuser.GU_Relations.Where(t => t.Group is RadioStation))
                {
                    //Add RadioTasks which deads later
                    foreach (var t in (Group.Group as RadioStation).RadioTasks.Where(t => t.DeadLine > DateTime.Now))
                    {
                        Notice.Add(t);
                    }
                    //Add RadioEvent which happens later
                    foreach (var e in (Group.Group as RadioStation).RadioEvents.Where(t => t.EndTime > DateTime.Now))
                    {
                        Notice.Add(e);
                    }
                }
                return await _JsongAsync(new ObiList<INoticeable>
                {
                    List = Notice.OrderByDescending(t => t.NoticeDate).Take(Amount ?? 10),
                    StatusCode = HttpStatusCode.OK
                });
            });
        }
        #endregion

        #region Invitations Management
        [ApiAuthorize]
        // GET: /api/MyInvitations
        public async Task<string> MyInvitations()
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                return await _JsongAsync(new ObiList<Invitation>
                {
                    StatusCode = HttpStatusCode.OK,
                    List = cuser.Invitations
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/InviteAUser/5?UserId=ASDSADA
        public async Task<string> InviteAUser(int id/*Group Id*/, string UserId)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Groups.FindAsync(id) as Team;
                //Can not find target team.
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //I am not an admin and I am not an owner.
                var MyRelation = Target.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                if (MyRelation == null || (MyRelation.RelationType != GU_RelationType.Owner && MyRelation.RelationType != GU_RelationType.Admin))
                {
                    return ForbiddenResult;
                }
                //Can not fine the User
                if (await UserManager.FindByIdAsync(UserId) == null)
                {
                    return await _JsongAsync(new ServerReply
                    {
                        StatusCode = HttpStatusCode.NotModified
                    });
                }
                //User Already Joined.
                if (Target.GU_Relations.Find(t => t.ObisoftUserId == UserId) != null)
                {
                    return NotAcceptableResult;
                }
                //Create Invitaion
                DbContext.Invitations.Add(new Invitation
                {
                    GroupId = id,
                    ObisoftUserId = UserId
                });
                return OkResult;
            });
        }
        #endregion

        #region Privacy Management
        [ApiAuthorize]
        // GET: /api/PrivacyState
        public async Task<string> PrivacyState()
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                var model = new PrivacyStateViewModel(cuser);
                return await _JsongAsync(new ObiObject<PrivacyStateViewModel>
                {
                    StatusCode = HttpStatusCode.OK,
                    Object = model
                });
            });
        }

        [ApiAuthorize]
        // POST: /api/SetPrivacyState
        public async Task<string> SetPrivacyState(PrivacyStateViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var cuser = await userAsync();
                cuser.AllowAddtoMyCalendar = model.AllowAddtoMyCalendar;
                cuser.AllowSeeIfImFree = model.AllowSeeIfImFree;
                cuser.AllowSeeMyCalendar = model.AllowSeeMyCalendar;
                cuser.AllowSeeMySchoolAndAccount = model.AllowSeeMySchoolAndAccount;
                cuser.AllowSeeWhatImDoing = model.AllowSeeWhatImDoing;
                await UserManager.UpdateAsync(cuser);
                return OkResult;
            });
        }
        #endregion

        #region Posts Management
        [ApiAuthorize]
        // GET: /api/PostsCenter?Take=5
        public async Task<string> PostsCenter(int Take)
        {
            return await this._apiReplyTool(async () =>
            {
                var Return = new List<Posts>();
                //Union
                foreach (var Relation in (await userAsync()).GU_Relations)
                {
                    foreach (var Posts in Relation.Group.GroupPostss)
                    {
                        Return.Add(Posts);
                    }
                }
                return await _JsongAsync(new ObiList<Posts>
                {
                    StatusCode = HttpStatusCode.OK,
                    List = Return.OrderByDescending(t => t.PublishDate).Take(Take)
                });
            });
        }

        [ApiAuthorize]
        // GET: /api/AllPostsFromGroup/5?Take=5
        public async Task<string> AllPostsFromGroup(int? id/*Group Id*/, int Take)
        {
            return await this._apiReplyTool(async () =>
            {
                var Return = new List<Posts>();
                var Group = await DbContext.Groups.FindAsync(id);
                //Not Found The Group
                if (Group == null)
                {
                    return NotFoundResult;
                }
                var MyRelation = Group.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                //Not Authorized
                if (MyRelation == null)
                {
                    return ForbiddenResult;
                }
                //Return
                return await _JsongAsync(new ObiList<Posts>
                {
                    StatusCode = HttpStatusCode.OK,
                    List = Return.OrderByDescending(t => t.PublishDate).Take(Take)
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/PostAsAGroup/5
        public async Task<string> PostAsAGroup(int? id/*Group Id*/, PostsViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var Group = await DbContext.Groups.FindAsync(id);
                //Not Found The Group
                if (Group == null)
                {
                    return NotFoundResult;
                }
                var MyRelation = Group.GU_Relations.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                //Not Authorized
                if (MyRelation == null || MyRelation.RelationType != GU_RelationType.Owner || MyRelation.RelationType != GU_RelationType.Admin)
                {
                    return ForbiddenResult;
                }
                //Add
                var NewPost = new GroupPosts
                {
                    Content = model.Content,
                    Title = model.Title,
                    GroupId = id ?? 0,
                    PublishDate = DateTime.Now,
                    PublisherFormGroupId = User.Identity.GetUserId()
                };
                DbContext.Posts.Add(NewPost);
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = NewPost.PostsId
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/PostAsAMe
        public async Task<string> PostAsAMe(PostsViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                //Create
                var NewPost = new PersonalPosts
                {
                    Content = model.Content,
                    Title = model.Title,
                    PublishDate = DateTime.Now,
                    ObisoftUserId = User.Identity.GetUserId()
                };
                //Publish
                DbContext.Posts.Add(NewPost);
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = NewPost.PostsId
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/EditAGroupPost/5
        public async Task<string> EditAGroupPost(int? id/*PostsId*/, PostsViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var OldPost = DbContext.Posts.Find(id) as GroupPosts;
                //Post Not Found
                if (OldPost == null)
                {
                    return NotFoundResult;
                }
                //Unauthorize
                if (OldPost.PublisherFormGroupId != User.Identity.GetUserId())
                {
                    return ForbiddenResult;
                }
                //Update
                OldPost.Title = model.Title;
                OldPost.Content = model.Content;
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/EditAPersonalPost/5
        public async Task<string> EditAPersonalPost(int? id/*PostsId*/, [System.Web.Http.FromBody]PostsViewModel model)
        {
            return await this._apiReplyTool(async () =>
            {
                var OldPost = DbContext.Posts.Find(id) as PersonalPosts;
                //Post Not Found
                if (OldPost == null)
                {
                    return NotFoundResult;
                }
                //Unauthorize
                if (OldPost.ObisoftUserId != User.Identity.GetUserId())
                {
                    return ForbiddenResult;
                }
                //Update
                OldPost.Title = model.Title;
                OldPost.Content = model.Content;
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        [HttpPost]
        [ApiAuthorize]
        // POST: /api/ResponseToPost/5
        public async Task<string> ResponseToPost(int? id/*PostsId*/, PostResposneType Type)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Posts.FindAsync(id);
                //Not Found
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //Already Responsed!
                if (Target.PostResponses.Exists(t => t.ObisoftUserId == User.Identity.GetUserId()))
                {
                    return ConflictSetResult;
                }
                //Response
                var NewResponse = new PostResponse
                {
                    ObisoftUserId = User.Identity.GetUserId(),
                    PostsId = id ?? 0,
                    PostResposneType = Type
                };
                Target.PostResponses.Add(NewResponse);
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = NewResponse.PostResponseId
                }); ;
            });
        }

        [HttpPost]
        [ApiAuthorize]
        public async Task<string> UnResponseToPost(int? id/*PostsId*/)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Posts.FindAsync(id);
                //Not Found
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //Find your Response
                var Response = Target.PostResponses.Find(t => t.ObisoftUserId == User.Identity.GetUserId());
                //Not Responsed!
                if (Response == null)
                {
                    return ConflictSetResult;
                }

                Target.PostResponses.Remove(Response);
                await DbContext.SaveChangesAsync();
                return OkResult;
            });
        }

        [HttpPost]
        [ApiAuthorize]
        public async Task<string> CommetToPost(int? id/*PostsId*/, string Content)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.Posts.FindAsync(id);
                //Not Found
                if (Target == null)
                {
                    return NotFoundResult;
                }
                //Update
                var NewComment = new FirComment
                {
                    ObisoftUserId = User.Identity.GetUserId(),
                    PostsId = id ?? 0,
                    Content = Content,
                    PublishDate = DateTime.Now
                };
                DbContext.FirComments.Add(NewComment);
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = NewComment.FirCommentId
                });
            });
        }

        [HttpPost]
        [ApiAuthorize]
        public async Task<string> CommetToCommet(int? id/*FirCommentId*/, string Content)
        {
            return await this._apiReplyTool(async () =>
            {
                var Target = await DbContext.FirComments.FindAsync(id);
                if (Target == null)
                {
                    return NotFoundResult;
                }
                var NewCommet = new SecComment
                {
                    FirCommentId = id ?? 0,
                    ObisoftUserId = User.Identity.GetUserId(),
                    PublishDate = DateTime.Now,
                    Content = Content,
                };
                DbContext.SecComments.Add(NewCommet);
                await DbContext.SaveChangesAsync();
                return await _JsongAsync(new ObiValue<int>
                {
                    StatusCode = HttpStatusCode.OK,
                    Value = NewCommet.SecCommentId
                });
            });
        }

        #endregion

        #region WeChat API
        [HttpGet]
        // GET: /api/WeChatVerify
        public async Task<string> WeChatVerify(string signature, string timestamp, string nonce, string echostr)
        {
            if (await Verify(signature, timestamp, nonce))
            {
                Response.Write(echostr);
                Response.End();
            }
            return string.Empty;
        }

        [HttpPost]
        // POST: /api/WeChatVerify
        public async Task<string> WeChatVerify(string signature, string timestamp, string nonce, string echostr, object obj)
        {
            if (await Verify(signature, timestamp, nonce))
            {
                var s = Request.InputStream;
                var Result = await XMLDeserializeObjectAsync<xml>(s);
                var ReturnMessage = new xml
                {
                    Content = await Reply(Result.Content),
                    CreateTime = ConvertDateTimeInt(DateTime.Now),
                    ToUserName = Result.FromUserName,
                    FromUserName = Result.ToUserName,
                    MsgType = "text"
                };
                Response.Write(await XMLSerializeObjectAsync(ReturnMessage));
            }
            return string.Empty;
        }

        // GET: /api/AccessTokenTest
        public async Task<string> AccessTokenTest()
        {
            return await this._apiReplyTool(async () =>
            {
                return await AccessTokenAsync();
            });
        }

        // GET: /api/ApplyMenu
        public async Task<string> ApplyMenu()
        {
            return await this._apiReplyTool(async () =>
            {
                var Buttons = new Source
                {
                    button = new List<Button>(1)
                };
                var NewButton = new Button
                {
                    name = "HelloWorld1",
                    sub_button = new List<SubButton>(1)
                };
                NewButton.sub_button.Add(new SubButton
                {
                    name = "Hi",
                    type = "view",
                    url = await GenerateAuthUrlAsync($"https://{Request.Url.Authority}/api/AuthRedirect")
                });
                Buttons.button.Add(NewButton);
                return await _JsongAsync(Buttons);
            });
        }

        // GET: /api/AuthRedirect
        public async Task<ActionResult> AuthRedirect(string code, string state)
        {
            var Result = await AuthCodeToAccessTokenAsync(code);
            var WCUser = await UserInfomationAsync(Result.openid, await AccessTokenAsync());
            var LCuser = UserManager.Users.ToList().Find(t => t.openid == Result.openid);
            if (LCuser == null || LCuser.SchoolAccountSet == false)
            {
                return RedirectToAction("Bind", "WeChat", new { openid = Result.openid });//await Check(WCUser);
            }
            switch (state.ToLower().Trim())
            {
                case "checkmorning":
                    return RedirectToAction("CheckMorning", "WeChat", new { openid = Result.openid });//await Check(WCUser);
                case "schedule":
                    return RedirectToAction("Schedule", "WeChat", new { openid = Result.openid });//await Check(WCUser);
                case "grade":
                    return RedirectToAction("Grade", "WeChat", new { openid = Result.openid });//await Check(WCUser);
            }
            return null;
        }
        #endregion

        #region Other Service API
        public async Task<string> YooleeQueryPeccancy(string city, string carnum, string enginenum, string vin)
        {
            return await this._apiReplyTool(async () =>
            {
                var Result = await OtherService.QueryPeccancy(city, carnum, enginenum, vin);
                if (Result.status == 0)
                {
                    return await _JsongAsync(Result.data?.lists);
                }
                else if (Result.status == -22)
                {
                    return NotFoundResult;
                }
                else if (Result.status == -11)
                {
                    return NotAcceptableResult;
                }
                else
                {
                    return ErrorResult;
                }
            });
        }
        public async Task<string> YooleeQueryInsurance(int CarPrice, int CarYears = 2016)
        {
            return await this._apiReplyTool(async () =>
            {
                var Result = await OtherService.QueryInsurance(CarPrice, CarYears);
                return await _JsongAsync(Result);
            });
        }

        #endregion
    }
}