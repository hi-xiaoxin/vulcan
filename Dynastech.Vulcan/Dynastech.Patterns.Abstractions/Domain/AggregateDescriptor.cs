using System;

namespace Dynastech.Patterns
{
    public class AggregateDescriptor
    {
        public AggregateDescriptor() { }

        public AggregateDescriptor(Guid id, string name, string type, string displayType = null)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.DisplayType = displayType;
        }

        /// <summary>
        /// 聚合实例ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 聚合实例名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 聚合的类型名称，例如 User
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 聚合的类型显示名称，与Type属性对应，例如 用户
        /// </summary>
        public string DisplayType { get; set; }
    }
}
