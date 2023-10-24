using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.ApplicationUsers
{
    public sealed class UserCreatedEvent
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = String.Empty;
    }
}
