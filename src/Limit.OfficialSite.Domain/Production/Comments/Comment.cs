using System;
using System.Collections.Generic;
using Limit.OfficialSite.Domain.Entities;

namespace Limit.OfficialSite.Comments
{
    /// <summary>
    /// 评论
    /// </summary>
    public sealed class Comment : AggregateRoot<Guid>
    {
        private Comment()
        {
            Replies = new List<CommentReply>();
        } 

        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary> 
        /// 用户头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }


        public Guid CreativeId { get; set; }

        public DateTime CreateTime { get; set; }


        public List<CommentReply> Replies { get; set; }

        public Guid AddReplayAndGetId(string reContent, string parentId, string headImg, string userName, bool isSpokesman)
        {
            var reply = new CommentReply(Id, reContent, parentId, headImg, userName, isSpokesman);

            Replies.Add(reply);

            return reply.Id;
        }
    }
}