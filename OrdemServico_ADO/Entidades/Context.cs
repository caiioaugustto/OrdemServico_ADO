using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Entidades
{
    public class Context : DbContext
    {
        public DbSet<Login> Login { get; set; }

        public DbSet<Fornecedor> Fornecedor { get; set; }

        public DbSet<OrdemServico> OrdemServico { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<OrdemServico>().HasRequired(m => m.Fornecedor);
        }
    }
}