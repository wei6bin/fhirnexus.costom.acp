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

        public DbSet<AppointmentModel> Appointments => Set<AppointmentModel>();

        public DbSet<EducationModel> Education => Set<EducationModel>();

        public DbSet<AcpFormEntity> AcpForm => Set<AcpFormEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseFhirConventions(this);
        }
    }
}
