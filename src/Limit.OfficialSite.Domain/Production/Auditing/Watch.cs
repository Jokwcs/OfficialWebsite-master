using System;
using Limit.OfficialSite.Authorization.Users;
using Limit.OfficialSite.Domain.Entities;

namespace Limit.OfficialSite.Production.Auditing
{
    /// <summary>
    /// 普通查看记录
    /// </summary>
    public class Watch : Entity<Guid>
    {
        /// <summary>
        /// 创建一个普通查看记录的实例
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="ugcId">内容Id</param>
        public Watch(Guid userId, Guid ugcId)
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
        /// 查看时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}