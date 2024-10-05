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
    public DbSet<PatientModel> PatientFHIR => Set<PatientModel>();
    public DbSet<QuestionnaireModel> Questionnaire => Set<QuestionnaireModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionnaireModel>()
            .OwnsMany(qm => qm.FormQuestions, qmBuilder =>
            {
                qmBuilder.WithOwner().HasForeignKey(x => x.QuestionnaireOID);
            })
            .OwnsMany(qm => qm.Questions, qmBuilder =>
            {
                qmBuilder.WithOwner().HasForeignKey(x => x.QuestionnaireOID);
                qmBuilder.OwnsMany(q => q.QuestionOptions, qBuilder =>
                {
                    qBuilder.WithOwner().HasForeignKey(x => x.QuestionOID);
                });
            });
    }
}
