using System;
using Limit.OfficialSite.Domain.Entities;

namespace Limit.OfficialSite.Comments
{
    /// <summary>
    /// 评论回复
    /// </summary>
    public sealed class CommentReply : Entity<Guid>
    {
        private CommentReply()
        {

        }

        public CommentReply(Guid commentId, string reContent, string parentId, string headImg, string userName, bool isSpokesman)
        {
            Id = Guid.NewGuid();
            CommentId = commentId;
            ReContent = reContent;
            ParentId = parentId;
            HeadImg = headImg;
            UserName = userName;
            IsSpokesman = isSpokesman;
        }

        /// <summary>
        /// 回复内容
        /// </summary> 
        public string ReContent { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否为楼主
        /// </summary>
        public bool IsSpokesman { get; set; }

        public Guid CommentId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}