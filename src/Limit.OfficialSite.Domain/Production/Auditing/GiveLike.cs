using System;
using Limit.OfficialSite.Authorization.Users;
using Limit.OfficialSite.Domain.Entities;

namespace Limit.OfficialSite.Production.Auditing
{
    /// <summary>
    /// 点赞记录
    /// </summary>
    public class GiveLike : Entity<Guid>
    {
        /// <summary>
        /// 领域对象被初始化，应该是一个有具体意义的对象
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="ugcId">点赞内容的Id</param>
        public GiveLike(Guid userId, Guid ugcId)
        {
            UserId = userId;
            UgcId = ugcId;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        public LimitUser User { get; set; }

        /// <summary>
        /// 内容Id 
        /// </summary>
        public Guid UgcId { get; set; }
        public UgcBase UgcBase { get; set; }

        /// <summary>
        /// 点赞时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}