using Microsoft.EntityFrameworkCore;
using Synapxe.FhirWebApi.Custom.Data.Model;

namespace Synapxe.FhirWebApi.Custom.Data;

public class FhirModelDbContext : DbContext
{
    public FhirModelDbContext(DbContextOptions<FhirModelDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppointmentModel> Appointment => Set<AppointmentModel>();
    public DbSet<PatientModel> Patient => Set<PatientModel>();
    public DbSet<QuestionnaireModel> FormQuestionnaire => Set<QuestionnaireModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<QuestionnaireModel>()
        //    .OwnsMany(p => p.Questions, a =>
        //    {
        //        a.WithOwner().HasForeignKey("OID");
        //        a.Property<long>("OID");
        //        a.HasKey("OID");
        //    })
        //    .OwnsMany(p => p.QuestionOptions, a =>
        //    {
        //        a.WithOwner().HasForeignKey("OID");
        //        a.Property<long>("OID");
        //        a.HasKey("OID");
        //    });
        modelBuilder.Entity<QuestionnaireModel>()
            .OwnsMany(p => p.FormQuestions, a =>
            {
                a.WithOwner().HasForeignKey("FormId");
            });
    }
}
