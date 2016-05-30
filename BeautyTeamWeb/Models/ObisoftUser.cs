using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using BeautyTeamWeb.Services;
using System;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace BeautyTeamWeb.Models
{
    /// <summary>
    /// Basic Information
    /// </summary>
    public partial class AnotherUser : IdentityUser
    {
        /// <summary>
        /// Nickname of the user.
        /// </summary>
        public virtual string NickName { get; set; }
        /// <summary>
        /// Real Name of the User
        /// </summary>
        public virtual string RealName { get; set; }
        /// <summary>
        ///The address of the user's icon
        /// </summary>
        [DataType(DataType.ImageUrl)]
        public virtual string IconImage { get; set; } = "https://obisoft.oss-cn-beijing.aliyuncs.com/WebSiteIcon/Icon.jpg";
        ///<summary>
        ///His personal description, not more than 150 length.
        ///</summary>
        [MaxLength(150)]
        public virtual string Description { get; set; } = "Nothing to say.";
        /// <summary>
        /// All Posts he posts
        /// </summary>
        public virtual List<PersonalPosts> PersonalPostss { get; set; }
    }
    ///<summary>
    ///User Definition
    ///</summary>
    public partial class ObisoftUser : AnotherUser
    {
        public async System.Threading.Tasks.Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ObisoftUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
    /// <summary>
    /// School Information
    /// </summary>
    public partial class ObisoftUser
    {
        /// <summary>
        /// If this user has binded a school, he will have the schoolid as a number, and the number is the school id, use api can get more information. Otherwise, it will be null.
        /// </summary>
        public virtual int? SchoolId { get; set; } = null;
        /// <summary>
        /// If this user has binded a school, he will have the schoolid as a string, and this is his AAO account. Otherwise, it will be empty.
        /// </summary>
        public virtual string SchoolAccount { get; set; }
        /// <summary>
        /// If this user has binded a school, he will have the SchoolPassAes as a string, and this is his AAO account password after AES. Otherwise, it will be empty.
        /// </summary>
        public virtual string SchoolPassAes { get; set; }
        /// <summary>
        /// This will tell if the user has bind a school.
        /// </summary>
        public virtual bool SchoolBinded => SchoolId != null;
        /// <summary>
        /// This will find the user shcool infromation. Won;t tell client when serilize to JSON.
        /// </summary>
        /// <returns></returns>
        public virtual School UserSchool() => SchoolService.FindSchoolById(SchoolId);
        /// <summary>
        /// This will tell if the user has bind his school and also set his account. Won't be true if he did not bind his school even when he has an account set.
        /// </summary>
        public virtual bool SchoolAccountSet => !string.IsNullOrWhiteSpace(SchoolAccount) && SchoolBinded;
    }
    /// <summary>
    /// BeautyTeam info
    /// </summary>
    public partial class ObisoftUser
    {
        /// <summary>
        /// Groups he joined.
        /// </summary>
        public virtual List<GU_Relation> GU_Relations { get; set; }
        /// <summary>
        /// Team Project tasks he has.
        /// </summary>
        public virtual List<TU_Relation> TU_Relations { get; set; }
        /// <summary>
        /// Team Event he has.
        /// </summary>
        public virtual List<EU_Relation> EU_Relations { get; set; }
        /// <summary>
        /// Personal Events he has.
        /// </summary>
        public virtual List<PersonalEvent> PersonalEvents { get; set; }
        /// <summary>
        /// Personal Tasks he has.
        /// </summary>
        public virtual List<PersonalTask> PersonalTasks { get; set; }
        /// <summary>
        /// These are all invitaions this user revieved.
        /// </summary>
        public virtual List<Invitation> Invitations { get; set; }

    }
    /// <summary>
    /// Privacy Info of a User.
    /// </summary>
    public partial class ObisoftUser
    {
        /// <summary>
        /// If this is set allow, others may find if I am free right now.
        /// </summary>
        public virtual bool AllowSeeIfImFree { get; set; } = true;
        /// <summary>
        /// If this is set allow, others may see what I am doing right now.
        /// </summary>
        public virtual bool AllowSeeWhatImDoing { get; set; } = false;
        /// <summary>
        /// If this is set allow, others like my boss can add event to my calendar directly.
        /// Even if this is not set allow, Team events or team tasks can also set to my calendar.
        /// </summary>
        public virtual bool AllowAddtoMyCalendar { get; set; } = true;
        /// <summary>
        /// If this is set allow, others can see my full calendar.
        /// </summary>
        public virtual bool AllowSeeMyCalendar { get; set; } = false;
        /// <summary>
        /// If this is set allow, others can see my school and account information.
        /// </summary>
        public virtual bool AllowSeeMySchoolAndAccount { get; set; } = true;
        //public virtual int FriendsPartId { get; set; }
        //public virtual async System.Threading.Tasks.Task DeletFriendPart(FriendsPart PartId, BeautyTeamDbContext Context = null)
        //{
        //    Context = Context ?? new BeautyTeamDbContext();
        //    var OldPart = await Context.FriendsParts.FindAsync(PartId);
        //    if (OldPart.ParentId != Id)
        //    {
        //        throw new Exception("Target Part Is Not User's Part.");
        //    }
        //    Context.FriendsParts.Remove(OldPart);
        //    await Context.SaveChangesAsync();
        //}
        public virtual async Task<int> AddFriend(ObisoftUser TargetFriend, int MyPartId, int HisPartId, BeautyTeamDbContext Context = null)
        {
            Context = Context ?? new BeautyTeamDbContext();
            //Find Part
            var MyPart = await Context.FriendsParts.FindAsync(MyPartId);
            var HisPart = await Context.FriendsParts.FindAsync(HisPartId);
            if (MyPart == null || HisPart == null)
            {
                throw new Exception("Part Id is invalid!");
            }
            //Check if part has correct parent
            if (MyPart.ParentId != Id || HisPart.ParentId != TargetFriend.Id)
            {
                throw new Exception("Part is not this user's part!");
            }
            //Double Relation
            var NewRelation = new FU_Relation
            {
                FriendId = TargetFriend.Id,
                ParentId = MyPart.FriendsPartId
            };
            var BNewRelation = new FU_Relation
            {
                FriendId = Id,
                ParentId = HisPart.FriendsPartId
            };
            //Double Way Add
            MyPart.Friends.Add(NewRelation);
            HisPart.Friends.Add(BNewRelation);

            await Context.SaveChangesAsync();
            return NewRelation.FU_RelationId;
        }
        public virtual async System.Threading.Tasks.Task DeleteFriend(ObisoftUser TargetFriend, BeautyTeamDbContext Context)
        {
            Context = Context ?? new BeautyTeamDbContext();
            //Find Double FU_Relation
            var MyRelation = Context.FU_Relation.Where(t => t.Parent.ParentId == Id && t.FriendId == TargetFriend.Id).First();
            var HisRelation = Context.FU_Relation.Where(t => t.Parent.ParentId == TargetFriend.Id && t.FriendId == Id).First();
            if (MyRelation == null || HisRelation == null)
            {
                throw new Exception("Incomplete Relation with two.");
            }
            //Delet the relation
            Context.FU_Relation.Remove(MyRelation);
            Context.FU_Relation.Remove(HisRelation);
            await Context.SaveChangesAsync();
            return;
        }
        public virtual List<FriendsPart> FriendsPart { get; set; } = new List<FriendsPart>();

        public virtual List<ObisoftUser> AllFriends()
        {
            var AllFriends = new List<ObisoftUser>();
            foreach(var Part in FriendsPart)
            {
                foreach(var FU_Relation in Part.Friends)
                {
                    AllFriends.Add(FU_Relation.Friend);
                }
            }
            return AllFriends;
        }
    }
    /// <summary>
    /// WeChat info
    /// </summary>
    public partial class ObisoftUser
    {
        /// <summary>
        /// His openid From WeChat.
        /// </summary>
        public virtual string openid { get; set; }
    }


    public class FriendsPart
    {
        public virtual int FriendsPartId { get; set; }

        public virtual List<FU_Relation> Friends { get; set; } = new List<FU_Relation>();
        public virtual string PartName { get; set; }
        //Parent
        public virtual string ParentId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser Parent { get; set; }
    }
    public class FU_Relation
    {
        public virtual int FU_RelationId { get; set; }
        //Parent
        public virtual int ParentId { get; set; }
        [JsonIgnore]
        public virtual FriendsPart Parent { get; set; }

        public virtual string FriendId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser Friend { get; set; }
    }
}