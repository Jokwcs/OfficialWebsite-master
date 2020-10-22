using System.ComponentModel.DataAnnotations;

namespace Limit.OfficialSite.Production.Photo
{
    /// <summary>
    /// 照片集
    /// </summary>
    public class UgcPhotoAlbum
    {
        /// <summary>
        /// 照片路径
        /// </summary>
        [StringLength(50)]
        public string PhotoPath { get; set; } 
    }
}