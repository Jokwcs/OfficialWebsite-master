using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Limit.OfficialSite.Authorization.Users;
using Limit.OfficialSite.Domain.Entities;
using Limit.OfficialSite.Production.Auditing;
using Limit.OfficialSite.Production.Category;

namespace Limit.OfficialSite.Production
{
    /// <summary>
    /// 作品基类
    /// </summary>
    public abstract class UgcBase : AggregateRoot<Guid>
    {
        protected UgcBase()
        {
            GiveLikes = new List<GiveLike>();
        }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        [StringLength(300)]
        public string SubTitle { get; set; }

        /// <summary>
        /// 作者
        /// </summary> 
        public Guid AuthorId { get; set; }
        public LimitUser Author { get; set; }

        /// <summary>
        /// 创作时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创作地点
        /// </summary>
        [StringLength(100)]
        public string CreatingLocation { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>  
        public DateTime PublishTime { get; set; }

        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }

        #region Audit

        #region 点赞记录

        /// <summary>
        /// 点赞数
        /// </summary>
        public long GiveLikeNumber { get; set; }

        /// <summary>
        /// 点赞记录
        /// </summary>
        public List<GiveLike> GiveLikes { get; set; }

        /// <summary>
        /// 添加一个点赞记录
        /// </summary>
        protected virtual void AddGiveLike(Guid userId)
        {
            // 避免重复点赞
            if (GiveLikes.Exists(x => x.UserId == userId))
            {
                return;
            }

            var record = new GiveLike(userId, Id);
            GiveLikes.Add(record);

            GiveLikeNumber += 1;
        }

        /// <summary>
        /// 移除一个点赞记录
        /// </summary>
        protected virtual void RemoveGiveLike(Guid userId)
        {
            var record = GiveLikes.FirstOrDefault(x => x.UserId == userId);

            if (record == null)
            {
                return;
            }

            GiveLikes.Remove(record);

            GiveLikeNumber -= 1;
        }

        #endregion

        #region 普通查看记录

        /// <summary>
        /// 查看次数
        /// </summary>
        public long WatchNumber { get; set; }

        /// <summary>
        /// 查看记录
        /// </summary>
        public List<Watch> Watches { get; set; }

        /// <summary>
        /// 添加一个普通查看记录
        /// </summary>
        /// <param name="userId"></param>
        protected virtual void AddWatchRecord(Guid userId)
        {
            var record = new Watch(userId, Id);

            Watches.Add(record);

            WatchNumber += 1;
        }

        /// <summary>
        /// 移除一个普通查看记录
        /// </summary>
        /// <param name="userId"></param>
        protected virtual void RemoveWatchRecord(Guid userId)
        {
            var record = Watches.FirstOrDefault(x => x.UserId == userId);

            if (record == null)
            {
                return;
            }

            Watches.Remove(record);

            WatchNumber -= 1;
        }

        #endregion

        #endregion 

        /// <summary>
        /// 类别Id
        /// </summary>
        public Guid UgcCategoryId { get; set; }

    }
}