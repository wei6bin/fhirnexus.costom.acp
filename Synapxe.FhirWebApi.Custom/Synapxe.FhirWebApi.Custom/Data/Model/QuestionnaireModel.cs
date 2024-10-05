using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hl7.Fhir.Introspection;
using Ihis.FhirEngine.Data.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;

namespace Synapxe.FhirWebApi.Custom.Data.Model;

[FhirType("Questionnaire", IsResource = true)]
[PrimaryKey(nameof(Id))]
public class QuestionnaireModel : IResourceEntity<long>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("OID")]
    [Key]
    public long Id { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public int? VersionId { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public bool IsHistory { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public DateTimeOffset? LastUpdated { get; set; }

    [Timestamp]
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public byte[]? TimeStamp { get; set; }

    public string? FormType { get; set; }

    public ICollection<FormQuestion_MA> FormQuestions { get; set; }

    public ICollection<Question_MA> Questions { get; set; }

    //public ICollection<QuestionOption_MA> QuestionOptions { get; set; }
}

[Owned]
public class FormQuestion_MA
{
    [Column("OID")]
    [Key]
    public long Id { get; set; }

    public long QuestionnaireOID { get; set; }

    public string? FormType { get; set; }

    public string? FormTypeName { get; set; }

    public string? DiseaseType { get; set; }

    public string? DiseaseTypeName { get; set; }
}

[Owned]
public class Question_MA
{
    [Column("OID")]
    [Key]
    public long Id { get; set; }

    public long QuestionnaireOID { get; set; }

    public string? QuestionText { get; set; }

    public ICollection<QuestionOption_MA> QuestionOptions { get; set; }
}

public class QuestionOption_MA
{
    [Column("OID")]
    [Key]
    public long Id { get; set; }

    public long QuestionOID { get; set; }

    public string? Type { get; set; }

    public string? Text { get; set; }
}
