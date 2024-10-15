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

        public DbSet<AcpFormEntity> Questionnaire => Set<AcpFormEntity>();
        public DbSet<AcpFormAnswerEntity> Form => Set<AcpFormAnswerEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseFhirConventions(this);
            modelBuilder.Entity<AcpFormEntity>()
                .Ignore(x => x.Contained)
                .Ignore(x => x.Extension)
                .Ignore(x => x.ModifierExtension)
                .Ignore(x => x.Meta)
                .Ignore(x => x.Text)
                .OwnsMany(x => x.FormQuestions, fq => fq
                    .ToTable("FormQuestions_MA")
                    .Ignore(x => x.Extension)
                    .Ignore(x => x.ModifierExtension))
                .OwnsMany(x => x.Questions, q => q
                .ToTable("Question_MA")
                    .Ignore(x => x.Extension)
                     .Ignore(x => x.ModifierExtension))
                .OwnsMany(x => x.WorksheetQuestions, ws => ws
                .ToTable("WorksheetQuestion_MA")
                .Ignore(x => x.Extension)
                      .Ignore(x => x.ModifierExtension))
                .OwnsMany(x => x.QuestionOptions, qo => qo
                .ToTable("QuestionOption_MA")
                .Ignore(x => x.Extension)
                      .Ignore(x => x.ModifierExtension));

            modelBuilder.Entity<AcpFormAnswerEntity>()
                .Ignore(x => x.Contained)
                .Ignore(x => x.Extension)
                .Ignore(x => x.ModifierExtension)
                .Ignore(x => x.Meta)
                .Ignore(x => x.Text)
                .OwnsMany(x => x.FormAnswers, fa => fa
                .ToTable("FormAnswer")
                .Ignore(x => x.Extension)
                      .Ignore(x => x.ModifierExtension))
                .OwnsMany(x => x.FormExtensions, fe => fe
                .ToTable("FormExtension")
                .Ignore(x => x.Extension)
                      .Ignore(x => x.ModifierExtension))
                .OwnsMany(x => x.FormNHSContacts, fc => fc
                .ToTable("FormNHSContacts")
                .Ignore(x => x.Extension)
                      .Ignore(x => x.ModifierExtension));
        }
    }
}
