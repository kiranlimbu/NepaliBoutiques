using MediatR;
using Core.Abstractions;

namespace Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
