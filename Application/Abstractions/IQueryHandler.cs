using MediatR;
using Core.Abstractions;

namespace Application.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{

}
