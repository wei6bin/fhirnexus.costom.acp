using Hl7.Fhir.Model;
using Hl7.Fhir.Model.CdsHooks;
using Ihis.FhirEngine.Core.Handlers.Data;
using Synapxe.FhirWebApi.CustomResource.Entities;

namespace Synapxe.FhirWebApi.CustomResource.Data;

public class AcpFormDataMapper : IFhirDataMapper<AcpFormEntity, AcpForm>
{
    public AcpForm Map(AcpFormEntity resource)
    {
        return new AcpForm
        {
            Id = resource.Id.ToString(),
            Meta = new Meta
            {
                VersionId = resource.VersionId.ToString(),
                LastUpdated = resource.LastUpdated,
            },
            VersionId = resource.VersionId.ToString(),
            FormType = resource.FormType,
            FormQuestions = resource.FormQuestions?.Select(q => new AcpForm.FormQuestionComponent
            {
                OID = q.OID,
                QuestionnaireOID = q.QuestionnaireOID,
                FormType = q.FormType,
                FormTypeName = q.FormTypeName,
                DiseaseType = q.DiseaseType,
                DiseaseTypeName = q.DiseaseTypeName
            }).ToList(),
            WorksheetQuestions = resource.WorksheetQuestions?.Select(q => new AcpForm.WorksheetQuestionComponent
            {
                OID = q.OID,
                WorksheetType = q.WorksheetType,
                WorksheetTypeName = q.WorksheetTypeName,
                DiseaseType = q.DiseaseType,
                DiseaseTypeName = q.DiseaseTypeName,
                QuestionOID = q.QuestionOID,
                QuestionTitle = q.QuestionTitle,
                IsMandatory = q.IsMandatory,
                DisplayOrder = q.DisplayOrder
            }).ToList(),
            Questions = resource.Questions?.Select(q => new AcpForm.QuestionComponent
            {
                OID = q.OID,
                QuestionText = q.QuestionText,
                StatusCode = q.StatusCode,
                StatusCodeName = q.StatusCodeName,
                CSSText = q.CSSText,
                StyleText = q.StyleText
            }).ToList(),
            QuestionOptions = resource.QuestionOptions?.Select(q => new AcpForm.QuestionOptionComponent
            {
                OID = q.OID,
                QuestionOID = q.QuestionOID,
            }).ToList()
        };
    }

    public AcpFormEntity ReverseMap(AcpForm resource)
    {
        return new AcpFormEntity {
            Id = resource.Id,
            VersionId = int.TryParse(resource.VersionId, out var vid) ? vid : 0,
            FormType = resource.FormType,
            FormQuestions = resource.FormQuestions?.Select(q => new AcpFormEntity.FormQuestionComponent
            {
                OID = q.OID ?? 0,
                QuestionnaireOID = q.QuestionnaireOID ?? 0,
                FormType = q.FormType,
                FormTypeName = q.FormTypeName,
                DiseaseType = q.DiseaseType,
                DiseaseTypeName = q.DiseaseTypeName
            }).ToList() ?? new(),
            WorksheetQuestions = resource.WorksheetQuestions?.Select(q => new AcpFormEntity.WorksheetQuestionComponent
            {
                OID = q.OID ?? 0,
                WorksheetType = q.WorksheetType,
                WorksheetTypeName = q.WorksheetTypeName,
                DiseaseType = q.DiseaseType,
                DiseaseTypeName = q.DiseaseTypeName,
                QuestionOID = q.QuestionOID ?? 0,
                QuestionTitle = q.QuestionTitle,
                IsMandatory = q.IsMandatory ?? false,
                DisplayOrder = q.DisplayOrder ?? 0
            }).ToList() ?? new(),
            Questions = resource.Questions?.Select(q => new AcpFormEntity.QuestionComponent
            {
                OID = q.OID ?? 0,
                QuestionText = q.QuestionText,
                StatusCode = q.StatusCode,
                StatusCodeName = q.StatusCodeName,
                CSSText = q.CSSText,
                StyleText = q.StyleText
            }).ToList() ?? new(),
            QuestionOptions = resource.QuestionOptions?.Select(q => new AcpFormEntity.QuestionOptionComponent
            {
                OID = q.OID ?? 0,
                QuestionOID = q.QuestionOID ?? 0,
            }).ToList() ?? new()
        };
    }
}