﻿using System.Data.Common;

namespace Blog.Common.Infrastructure
{
    public interface IDbConnectionFactory
    {
        public DbConnection Create();

        public DbConnection Create(string connectionString);
    }
}
