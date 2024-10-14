using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hl7.Fhir.Introspection;
using Ihis.FhirEngine.Data.Models;

namespace Synapxe.FhirWebApi.CustomResource.Entities;


[CustomFhirResource]
[FhirType("AcpFormAnswer", "http://hl7.org/fhir/StructureDefinition/AcpFormAnswer", IsResource = true)]
public partial class AcpFormAnswerEntity : ResourceEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long OID { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public long CreatedBy { get; set; }
    public string CreatedByName { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
    public long ModifiedBy { get; set; }
    public string ModifiedByName { get; set; }
    public long PatientOID { get; set; }
    public bool IsHistory { get; set; }
    public int Version { get; set; }
    public string StatusCode { get; set; }
    public string StatusName { get; set; }
    public string FormTypeCode { get; set; }
    public string FormTypeName { get; set; }
    public string? PlaceOfDocumentation { get; set; }
    public DateTimeOffset? DateOfSession { get; set; }
    public string? ProgramName { get; set; }
    public string? PatientHasSignCode { get; set; }
    public string? PatientHasSignName { get; set; }
    public DateTimeOffset? PatientSigningDate { get; set; }
    public string? PatientNotSignedReason { get; set; }
    public string? NHSIdentified { get; set; }
    public string? NHSNotIdetifiedReason { get; set; }
    public string? IsPhysicianHasSigned { get; set; }
    public DateTimeOffset? PhysicianSignedDate { get; set; }
    public string? PhysicianNotSignedReason { get; set; }
    public string? FacilitatorName { get; set; }
    public string? FacilitatorNRIC { get; set; }
    public string? FacilitatorDesignation { get; set; }
    public string? FacilitatorEmail { get; set; }
    public DateTimeOffset? FacilitatorSignedDate { get; set; }
    public string? FacilitatorInstitution { get; set; }
    public bool? LivingWellStatement { get; set; }
    public bool? FollowupPlan { get; set; }
    public bool? UploadedManualForm { get; set; }
    public bool? SupportingOthers { get; set; }
    public string? SupportingOthersText { get; set; }
    public string? ACPContactNumber { get; set; }
    public string? PhysicianName { get; set; }
    public string? PhysicianMCR { get; set; }
    public bool IsDeleted { get; set; }
    public string? DeletedReasonCode { get; set; }
    public string? DeletedReasonName { get; set; }
    public string? DeletedReasonOthersText { get; set; }
    public string? DeletionRequester { get; set; }
    public string CreatedOrganizationCode { get; set; }
    public string CreatedOrganizationName { get; set; }
    public string ModifiedOrganizationCode { get; set; }
    public string ModifiedOrganizationName { get; set; }
    public DateTimeOffset? PublishedDate { get; set; }
    public long? PublishedBy { get; set; }
    public string? PublishedByName { get; set; }
    public string? PublishedOrganizationCode { get; set; }
    public string? PublishedOrganizationName { get; set; }
    public string? FacilitatorFullNRIC { get; set; }
    public string? CreatedOrganizationHCICode { get; set; }
    public string? ModifiedOrganizationHCICode { get; set; }
    public string? PublishedOrganizationHCICode { get; set; }
    public string? LastModifiedByEmail { get; set; }

    public List<FormAnswer> FormAnswers { get; set; } = new();
    public List<FormExtension> FormExtensions { get; set; } = new();
    public List<FormNHSContact> FormNHSContacts { get; set; } = new();

    [FhirType("AcpFormAnswer#FormAnswer")]
    public class FormAnswer : BackboneEntity
    {
        public long OID { get; set; }
        public long FormOID { get; set; }
        public long FormQuestionOID { get; set; }
        public long QuestionOptionOID { get; set; }
        public string Answer { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
    }

    [FhirType("AcpFormAnswer#FormExtension")]
    public class FormExtension : BackboneEntity
    {
        public long OID { get; set; }
        public long FormOID { get; set; }
        public string PhysicianNoSignatureReasonCode { get; set; }
        public string PhysicianNoSigOtherReason { get; set; }
        public string PatientNoSignatureReasonCode { get; set; }
        public string PatientNoSigOtherReason { get; set; }
        public long? CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public string SettingCode { get; set; }
        public string SettingOther { get; set; }
        public string DiseaseOfConcernCode { get; set; }
        public string DiseaseOfConcernOther { get; set; }
    }

    [FhirType("AcpFormAnswer#FormNHSContact")]
    public class FormNHSContact : BackboneEntity
    {
        public long OID { get; set; }
        public long FormOID { get; set; }
        public string Name { get; set; }
        public string RelationshipCode { get; set; }
        public string RelationshipName { get; set; }
        public string OtherRelationship { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public bool? IsPrimaryNHS { get; set; }
        public bool? SignedHardcopy { get; set; }
        public DateTimeOffset? SignedDate { get; set; }
        public string NotSignedReason { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public long ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
    }
}
