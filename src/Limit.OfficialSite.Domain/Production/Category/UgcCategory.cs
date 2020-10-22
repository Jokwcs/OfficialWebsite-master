using System;
using System.Collections.Generic;
using Limit.OfficialSite.Domain.Entities;

namespace Limit.OfficialSite.Production.Category
{
    /// <summary>
    /// 作品分类
    /// </summary>
    public sealed class UgcCategory : AggregateRoot<Guid>
    {
        /// <summary>
        /// 创建一个舞蹈分类
        /// </summary>
        /// <param name="name">名称</param>
        public UgcCategory(string name)
        {
            Name = name;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 排序字段-等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 排序字段-序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        public List<UgcBase> UgcBase { get; set; }
    }
}