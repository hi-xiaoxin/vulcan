using System;
using System.ComponentModel.DataAnnotations;

namespace Dynastech.Patterns
{
    /// <summary>
    /// 事件日志（原则上只有微服务记日志）
    /// </summary>
    [Display(Name ="事件日志")]
    public class EventLog : Event
    {
        /// <summary>
        /// 事件源
        /// 1. 现在这种情况，BFF直接调用DLL， source = 配置的一个值“Identity”
        /// 2. 微服务情况，BFF调用微服务，source = 配置的一个值“Identity”
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 调用的客户端
        /// 1. 现在这种情况，BFF直接调用DLL， clienid = 配置的值“identity.admin" "identity.profile"
        /// 2. 微服务情况，BFF调用微服务，clienid = 从claims中取得（外部传得）
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 调用得用户
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 消息原始数据，默认Builder采用Json序列化
        /// </summary>
        public string MessageData { get; set; }

        /// <summary>
        /// 该事件所包含的消息的类型，比如： CreateUserCommand , GetUserQuery
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// 与MessageType对应的显示名称，比如： 创建用户 , 查询用户
        /// </summary>
        public string MessageDisplayType { get; set; }

        /// <summary>
        /// 消息的分类，区分是Command，还是Query
        /// </summary>
        public MessageKind MessageKind { get; set; }

        /// <summary>
        /// 针对COmmand，是返回受影响的聚合
        /// 针对 Query， 是返回影响的行数
        /// </summary>
        public AffectedAggregatesDescriptor AffectedAggregates { get; set; }

        /// <summary>
        /// 是否异常
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 异常触发的堆栈信息
        /// </summary>
        public string ErrorStackTrace { get; set; }
    }
}
