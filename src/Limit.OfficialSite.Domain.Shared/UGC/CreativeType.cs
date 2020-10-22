using System.ComponentModel;

namespace Limit.OfficialSite.UGC
{
    /// <summary>
    /// 创作类型
    /// </summary>
    public enum CreativeType
    {
        [Description("文章")]
        Article,

        [Description("视频")]
        Video,

        [Description("音乐")]
        Music,
         
        [Description("图片")]
        Picture
    }
}  