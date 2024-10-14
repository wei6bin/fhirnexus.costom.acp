// -------------------------------------------------------------------------------------------------
// Copyright (c) Integrated Health Information Systems Pte Ltd. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Synapxe.FhirWebApi.CustomResource.Entities;

namespace Synapxe.FhirWebApi.CustomResource.Data
{
    public class FhirModelDbContext : DbContext
    {
        public FhirModelDbContext(DbContextOptions<FhirModelDbContext> options)
            : base(options)
        {
        }

        public DbSet<AcpFormEntity> AcpQuestionnaire => Set<AcpFormEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseFhirConventions(this);
            modelBuilder.Entity<AcpFormEntity>()
                .Ignore(x => x.Contained);
        }
    }
}
