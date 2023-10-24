using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Posts
{
    public sealed class PostModifiedEvent
    {
        public Guid PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreatedOnUtc { get; set; }
    }
}
