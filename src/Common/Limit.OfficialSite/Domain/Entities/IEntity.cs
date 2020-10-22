namespace Limit.OfficialSite.Domain.Entities
{
    /// <summary>
    /// 对于最常用的主键类型， <see cref="IEntity{TPrimaryKey}"/> 的快捷方式 (<see cref="int"/>).
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }
}