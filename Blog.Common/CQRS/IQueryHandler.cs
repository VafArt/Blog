using Blog.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.CQRS
{
    public interface IQueryHandler<in IQuery, TQueryResult>
    {
        Task<Result<TQueryResult>> Handle(IQuery query, CancellationToken cancellation);
    }
}
