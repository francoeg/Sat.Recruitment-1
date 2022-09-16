

using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Commons.Models
{
    public abstract class 
         BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IMapper _imapper;

        protected BaseRequestHandler( IMapper imapper)
        {
            this._imapper = imapper;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
