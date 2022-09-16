using MediatR;

namespace Sat.Recruitment.Common.Models
{
    public abstract class CommandRequest<T,TResponse> 
        : IRequest<TResponse> 
         where T : class
    {
        protected CommandRequest( T item)
        {
            this.Item = item;
        }
         public T Item { get; }
    }
}
