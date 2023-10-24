using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Posts
{
    public sealed record PostViewedEvent(Guid PostId, DateTime ViewedOnUtc);
}
