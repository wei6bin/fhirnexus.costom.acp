using Hl7.Fhir.Model;
using Hl7.Fhir.Model.CdsHooks;
using Ihis.FhirEngine.Core.Handlers.Data;
using Synapxe.FhirWebApi.CustomResource.Entities;

namespace Synapxe.FhirWebApi.CustomResource.Data;

public class AcpFormAnswerDataMapper : IFhirDataMapper<AcpFormAnswerEntity, AcpFormAnswer>
{
    public AcpFormAnswer Map(AcpFormAnswerEntity resource)
    {
        return new AcpFormAnswer
        {
            Id = resource.Id.ToString(),
            Meta = new Meta
            {
                VersionId = resource.VersionId.ToString(),
                LastUpdated = resource.LastUpdated,
            },
            CreatedOn = resource.CreatedOn,
            CreatedBy = resource.CreatedBy,
            CreatedByName = resource.CreatedByName,
            ModifiedOn = resource.ModifiedOn,
            ModifiedBy = resource.ModifiedBy,
            ModifiedByName = resource.ModifiedByName,
            PatientOID = resource.PatientOID,
            IsHistory = resource.IsHistory,
            Version = resource.Version,
            StatusCode = resource.StatusCode,
            StatusName = resource.StatusName,
            FormTypeCode = resource.FormTypeCode,
            FormTypeName = resource.FormTypeName,
            PlaceOfDocumentation = resource.PlaceOfDocumentation,
            CreatedOrganizationCode = resource.CreatedOrganizationCode,
            CreatedOrganizationName = resource.CreatedOrganizationName,
            ModifiedOrganizationCode = resource.ModifiedOrganizationCode,
            ModifiedOrganizationName = resource.ModifiedOrganizationName,
            FormAnswers = resource.FormAnswers?.Select(q => new AcpFormAnswer.FormAnswerComponent
            {
                OID = q.OID,
                FormOID = q.FormOID,
                FormQuestionOID = q.FormQuestionOID,
                QuestionOptionOID = q.QuestionOptionOID,
                Answer = q.Answer,
                CreatedOn = q.CreatedOn,
                CreatedBy = q.CreatedBy,
                CreatedByName = q.CreatedByName,
                ModifiedOn = q.ModifiedOn,
                ModifiedBy = q.ModifiedBy,
                ModifiedByName = q.ModifiedByName,
            }).ToList(),
            FormExtensions = resource.FormExtensions?.Select(q => new AcpFormAnswer.FormExtensionComponent
            {
                OID = q.OID,
            }).ToList(),
            FormNHSContacts = resource.FormNHSContacts?.Select(q => new AcpFormAnswer.FormNHSContactComponent
            {
                OID = q.OID,
            }).ToList(),
        };
    }

    public AcpFormAnswerEntity ReverseMap(AcpFormAnswer resource)
    {
        return new AcpFormAnswerEntity
        {
            Id = resource.Id,
            VersionId = int.TryParse(resource.VersionId, out var vid) ? vid : 0,
            IsHistory = resource.IsHistory ?? false,
            Version = resource.Version ?? 1,
            CreatedOn = resource.CreatedOn ?? throw new ArgumentNullException(),
            CreatedBy = resource.CreatedBy ?? 0,
            CreatedByName = resource.CreatedByName,
            ModifiedOn = resource.ModifiedOn ?? throw new ArgumentNullException(),
            ModifiedBy = resource.ModifiedBy ?? 0,
            ModifiedByName = resource.ModifiedByName,
            PatientOID = resource.PatientOID ?? throw new ArgumentNullException(),
            StatusCode = resource.StatusCode,
            StatusName = resource.StatusName,
            FormTypeCode = resource.FormTypeCode,
            FormTypeName = resource.FormTypeName,
            PlaceOfDocumentation = resource.PlaceOfDocumentation,
            CreatedOrganizationCode = resource.CreatedOrganizationCode,
            CreatedOrganizationName = resource.CreatedOrganizationName,
            ModifiedOrganizationCode = resource.ModifiedOrganizationCode,
            ModifiedOrganizationName = resource.ModifiedOrganizationName,

            FormAnswers = resource.FormAnswers?.Select(q => new AcpFormAnswerEntity.FormAnswerComponent
            {
                OID = q.OID ?? 0,
                FormOID = q.FormOID ?? 0,
                FormQuestionOID = q.FormQuestionOID ?? 0,
                QuestionOptionOID = q.QuestionOptionOID ?? 0,
                Answer = q.Answer,
                CreatedOn = q.CreatedOn ?? throw new ArgumentNullException(),
                CreatedBy = q.CreatedBy ?? 0,
                CreatedByName = q.CreatedByName,
                ModifiedOn = q.ModifiedOn ?? throw new ArgumentNullException(),
                ModifiedBy = q.ModifiedBy ?? 0,
                ModifiedByName = q.ModifiedByName,
            }).ToList(),
            FormExtensions = resource.FormExtensions?.Select(q => new AcpFormAnswerEntity.FormExtensionComponent
            {
                OID = q.OID ?? 0,
            }).ToList(),
            FormNHSContacts = resource.FormNHSContacts?.Select(q => new AcpFormAnswerEntity.FormNHSContactComponent
            {
                OID = q.OID ?? 0,
            }).ToList(),
        };
    }
}