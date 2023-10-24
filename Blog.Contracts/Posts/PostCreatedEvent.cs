using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Posts
{
    public sealed record PostCreatedEvent(Guid PostId, string Title, DateTime CreatedOnUtc);
}
