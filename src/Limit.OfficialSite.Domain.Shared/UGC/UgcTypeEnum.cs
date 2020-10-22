using System.ComponentModel;

namespace Limit.OfficialSite.Ugc
{
    /// <summary>
    /// 内容枚举
    /// </summary>
    public enum UgcTypeEnum
    {
        [Description("视频")]
        Video,

        [Description("音乐")]
        Music,

        [Description("照片")]
        Photo,

        [Description("随笔")]
        Essay
    }
}