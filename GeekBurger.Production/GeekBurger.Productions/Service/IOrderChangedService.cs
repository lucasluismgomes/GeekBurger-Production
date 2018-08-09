using GeekBurger.Orders.Contract.Messages;

namespace GeekBurger.Productions.Service
{
    public interface IOrderChangedService
    {
        void SendMessagesAsync();
        void AddToMessageList(OrderChangedMessage orderChanged);
    }
}
