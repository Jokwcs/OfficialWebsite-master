using Newtonsoft.Json;

namespace Limit.OfficialSite.Json.Extensions
{
    /// <summary>
    /// Json序列化扩展类 
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 将一个 <see cref="object"/> 对象序列化成一段Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            //TODO:待扩展  1.是否驼峰拼写法；2.是否缩进

            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 使用自定义 <see cref="JsonSerializerSettings"/> 返回反序列化字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value)
        {
            return value.FromJsonString<T>(new JsonSerializerSettings());
        }

        /// <summary>
        /// 使用自定义 <see cref="JsonSerializerSettings"/> 返回反序列化字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T FromJsonString<T>(this string value, JsonSerializerSettings settings)
        {
            return value != null
                ? JsonConvert.DeserializeObject<T>(value, settings)
                : default;
        }
    }
}