using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hl7.Fhir.Introspection;
using Ihis.FhirEngine.Data.Models;

namespace Synapxe.FhirWebApi.CustomResource.Entities;

[CustomFhirResource]
[FhirType("AcpForm", "http://hl7.org/fhir/StructureDefinition/AcpForm", IsResource = true)]
public partial class AcpFormEntity : ResourceEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long OID { get; set; }

    public string FormType { get; set; }

    public List<FormQuestionComponent> FormQuestions { get; set; } = new();

    public List<WorksheetQuestionComponent> WorksheetQuestions { get; set; } = new();

    public List<QuestionComponent> Questions { get; set; } = new();

    //public List<QuestionOptionComponent> QuestionOptions { get; set; } = new();

    [FhirType("AcpForm#FormQuestion", IsNestedType = true)]
    public class FormQuestionComponent : BackboneEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long OID { get; set; }

        public required string FormType { get; set; }

        public required string FormTypeName { get; set; }

        public string? DiseaseType { get; set; }

        public string? DiseaseTypeName { get; set; }

        public long QuestionOID { get; set; }

        public string? QuestionTitle { get; set; }

        public bool IsMandatory { get; set; }

        public int DisplayOrder { get; set; }

        public string StatusCode { get; set; }

        public string StatusCodeName { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public long CreatedBy { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }

        public long ModifiedBy { get; set; }

        public string? Code { get; set; }
    }

    [FhirType("AcpForm#WorksheetQuestion", IsNestedType = true)]
    public class WorksheetQuestionComponent : BackboneEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long OID { get; set; }

        public string WorksheetType { get; set; }

        public string WorksheetTypeName { get; set; }

        public string? DiseaseType { get; set; }

        public string? DiseaseTypeName { get; set; }

        public long QuestionOID { get; set; }

        public string? QuestionTitle { get; set; }

        public bool IsMandatory { get; set; }

        public int DisplayOrder { get; set; }

        public string StatusCode { get; set; }

        public string StatusCodeName { get; set; }

        //public byte UI_Version { get; set; }

        public string? InfoHTML { get; set; }
    }

    [FhirType("AcpForm#Question", IsNestedType = true)]
    public class QuestionComponent : BackboneEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long OID { get; set; }

        public string QuestionText { get; set; }

        public string StatusCode { get; set; }

        public string StatusCodeName { get; set; }

        public string? CSSText { get; set; }

        public string? StyleText { get; set; }
    }

    //[FhirType("AcpForm#QuestionOption", IsNestedType = true)]
    //public class QuestionOptionComponent : BackboneEntity
    //{
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [Key]
    //    public long OID { get; set; }

    //    public long QuestionOID { get; set; }

    //    public string Label { get; set; }

    //    public string Type { get; set; }

    //    public string TypeRef { get; set; }

    //    public string Text { get; set; }

    //    public string? Value { get; set; }

    //    public int DisplayOrder { get; set; }

    //    public int ColumnIndex { get; set; }

    //    public string? CSSText { get; set; }

    //    public long ParentOptionOID { get; set; }

    //    public string? ControlEvents { get; set; }

    //    public string? SourceCodeSystem { get; set; }

    //    public string? OtherAttributes { get; set; }

    //    public string StatusCode { get; set; }

    //    public string StatusCodeName { get; set; }
    //}
}
