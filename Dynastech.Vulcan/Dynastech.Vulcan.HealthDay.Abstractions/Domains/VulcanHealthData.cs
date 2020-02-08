using Dynastech.Patterns;
using System;

namespace Dynastech.Vulcan.HealthDay
{
    public class VulcanHealthData : VulcanData
    {
        /// <summary>
        /// 是否健康
        /// </summary>
        public bool IsHealth { get; set; }

        /// <summary>
        /// 健康状态异常描述
        /// </summary>
        public string UnhealthDescription { get; set; }

        /// <summary>
        /// 是否去过湖北
        /// </summary>
        public bool IsGotoHuBei { get; set; }

        /// <summary>
        /// 是否去过武汉
        /// </summary>
        public bool IsGotoWuHan { get; set; }

        /// <summary>
        /// 是否经过湖北
        /// </summary>
        public bool IsThroughHuBei { get; set; }

        /// <summary>
        /// 是否经过武汉
        /// </summary>
        public bool IsThroughWuHan { get; set; }

        /// <summary>
        /// 什么时候返回苏州
        /// </summary>
        public DateTime? WhenBackCity { get; set; }
    }
}