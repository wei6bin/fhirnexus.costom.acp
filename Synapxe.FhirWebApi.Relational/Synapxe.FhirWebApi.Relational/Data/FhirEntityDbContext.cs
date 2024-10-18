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

    public DbSet<QuestionnaireEntity> Questionnaire => Set<QuestionnaireEntity>();

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

        //    .OwnsMany(x => x.Item, i => i.ToTable("Questionnaire_Item_Item").Ignore(x => x.Extension).Ignore(x => x.ModifierExtension)
        //        .OwnsMany(x => x.AnswerOption, i => i.Ignore(x => x.Extension).Ignore(x => x.ModifierExtension))
        //        .OwnsMany(x => x.EnableWhen, i => i.Ignore(x => x.Extension).Ignore(x => x.ModifierExtension))));
    }
}
