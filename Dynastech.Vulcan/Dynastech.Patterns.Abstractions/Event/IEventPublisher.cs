using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface IEventPublisher
    {
        /// <summary>
        /// 发布事件，采用默认的RoutingKey
        /// 1. 如添加了 MessageRouteAttribute, 则 RoutingKey 会采用MessageRoute.RoutingKey
        /// 2. 如没有添加 MessageRouteAttribute, 则 RoutingKey 会采用 @event.GetType().Name
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default) => PublishAsync(null, @event, cancellationToken);

        /// <summary>
        /// 发布事件，采用默认的RoutingKey
        /// </summary>
        /// <param name="routingKey">
        /// 如果 routingKey 参数为空：
        /// 1. 如添加了 MessageRouteAttribute, 则 RoutingKey 会采用MessageRoute.RoutingKey
        /// 2. 如没有添加 MessageRouteAttribute, 则 RoutingKey 会采用 @event.GetType().Name
        /// </param>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync(string routingKey, IEvent @event, CancellationToken cancellationToken = default);
    }
}
