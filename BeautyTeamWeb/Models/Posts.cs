using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyTeamWeb.Models
{
    //1. 个人的盆友圈
    //2. 团队的事项版
    public abstract class Posts
    {
        public virtual int PostsId { get; set; }
        [MaxLength(40)]
        public virtual string Title { get; set; }
        [DataType(DataType.Html)]
        public virtual string Content { get; set; }
        public virtual int Views { get; set; } = 0;
        public virtual List<PostResponse> PostResponses { get; set; } = new List<PostResponse>();
        public virtual List<FirComment> Comments { get; set; } = new List<FirComment>();
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public virtual DateTime PublishDate { get; set; } = DateTime.Now;
    }
    public class GroupPosts : Posts
    {
        public virtual int GroupId { get; set; }
        [JsonIgnore]
        public virtual Group Group { get; set; }

        public virtual string PublisherFormGroupId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser PublisherFormGroup { get; set; }

    }
    public class PersonalPosts : Posts
    {
        public virtual string ObisoftUserId { get; set; }
        [JsonIgnore]
        public virtual ObisoftUser ObisoftUser { get; set; }
    }
    public enum PostResposneType : short
    {
        Roger=1,
        Like=2,
        Love=4,
        Sorry=8,
        Angry=16,
        Wow=32
    }
    public class PostResponse
    {
        public virtual int PostResponseId { get; set; }

        public virtual int PostsId { get; set; }
        public virtual Posts Posts { get; set; }

        public virtual ObisoftUser ObisoftUser { get; set; }
        public virtual string ObisoftUserId { get; set; }

        public virtual PostResposneType PostResposneType { get; set; }
    }
    public class FirComment
    {
        public virtual int FirCommentId { get; set; }
        public virtual string Content { get; set; }
        public virtual string ObisoftUserId { get; set; }
        public virtual ObisoftUser ObisoftUser { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual List<SecComment> SecComments { get; set; }
        public virtual int PostsId { get; set; }
        public virtual Posts Posts { get; set; }
    }
    public class SecComment
    {
        [Key]
        public virtual int SecCommentId { get; set; }
        public virtual string Content { get; set; }
        public virtual string ObisoftUserId { get; set; }
        public virtual ObisoftUser ObisoftUser { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual int FirCommentId { get; set; }
        public virtual FirComment Parent { get; set; }
    }
}