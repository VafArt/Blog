using Blog.Common.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.CQRS
{
    public interface IQueryHandler<in TQuery, TQueryResult>
        where TQuery : IQuery
        where TQueryResult : Result
    {
        Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
    }
}
