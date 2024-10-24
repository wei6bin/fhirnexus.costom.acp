// -------------------------------------------------------------------------------------------------
// Copyright (c) Integrated Health Information Systems Pte Ltd. All rights reserved.
// -------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace Synapxe.FhirWebApi.Relational.Data;

public class FhirEntityDbContext : DbContext
{
    public FhirEntityDbContext(DbContextOptions<FhirEntityDbContext> options)
        : base(options)
    {
    }

    public DbSet<CarePlanEntity> CarePlan => Set<CarePlanEntity>();
    public DbSet<QuestionnaireEntity> Questionnaire => Set<QuestionnaireEntity>();
    public DbSet<QuestionnaireResponseEntity> QuestionnaireResponse => Set<QuestionnaireResponseEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseFhirConventions(this);
        modelBuilder.Entity<QuestionnaireEntity>()
            .Ignore(x => x.Extension).Ignore(x => x.ModifierExtension).Ignore(x => x.Contained)
            .OwnsOne(x => x.Meta, i => i.Ignore(x => x.Profile).Ignore(x => x.Security).Ignore(x => x.Tag))
            .OwnsMany(x => x.Item, i => i
                .ToTable("Questionnaire_Item").Ignore(x => x.ElementId).Ignore(x => x.Extension).Ignore(x => x.ModifierExtension)
                .OwnsMany(x => x.AnswerOption, i => i.ToTable("Questionnaire_Item_AnswerOption").Ignore(x => x.Extension).Ignore(x => x.ModifierExtension))
                .OwnsMany(x => x.EnableWhen, i => i.ToTable("Questionnaire_Item_EnableWhen").Ignore(x => x.Extension).Ignore(x => x.ModifierExtension)));

        modelBuilder.Entity<QuestionnaireResponseEntity>()
            .Ignore(x => x.Extension).Ignore(x => x.ModifierExtension).Ignore(x => x.Contained)
            .OwnsOne(x => x.Meta, i => i.Ignore(x => x.Profile).Ignore(x => x.Security).Ignore(x => x.Tag))
            .OwnsMany(x => x.Item, i => i.ToTable("QuestionnaireResponse_Item").Ignore(x => x.ElementId).Ignore(x => x.Extension).Ignore(x => x.ModifierExtension)
                .OwnsMany(x => x.Answer, i => i.ToTable("QuestionnaireResponse_Item_Answer").Ignore(x => x.Extension).Ignore(x => x.ModifierExtension)));
    }
}
