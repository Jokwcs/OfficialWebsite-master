﻿namespace Limit.OfficialSite.Domain.Entities
{
    /// <summary>
    /// 定义基本实体类型的接口。系统中的所有实体都必须实现此接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键的类型</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 此实体的唯一标识符
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}