using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleRegisteredEvent
    {
        public Sale Sale { get; }

        public SaleRegisteredEvent(Sale user)
        {
            Sale = user;
        }
    }
}
