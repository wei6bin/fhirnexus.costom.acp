using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core.Handlers.Data;
using Synapxe.FhirWebApi.CustomResource.Entities;

namespace Synapxe.FhirWebApi.CustomResource.Data;

public class AcpFormDataMapper : IFhirDataMapper<AcpFormEntity, AcpForm>
{
    public AcpForm Map(AcpFormEntity resource)
    {
        return new AcpForm
        {
            Id = resource.Id.ToString("N"),
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
                FormType = q.FormType,
                FormTypeName = q.FormTypeName,
                DiseaseType = q.DiseaseType,
                DiseaseTypeName = q.DiseaseTypeName,
                QuestionOID = q.QuestionOID,
                QuestionTitle = q.QuestionTitle,
                IsMandatory = q.IsMandatory,
                DisplayOrder = q.DisplayOrder
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
            Questions = resource.Questions?.Select(q => new AcpForm.Question
            {
                OID = q.OID,
                QuestionText = q.QuestionText,
                StatusCode = q.StatusCode,
                StatusCodeName = q.StatusCodeName,
                CSSText = q.CSSText,
                StyleText = q.StyleText,
                QuestionOptions = q.QuestionOptions?.Select(qo => new AcpForm.QuestionOptionComponent
                {
                    OID = qo.OID,
                    QuestionOID = qo.QuestionOID,
                    Label = qo.Label,
                    Value = qo.Value,
                    DisplayOrder = qo.DisplayOrder,
                    Type = qo.Type,
                    TypeRef = qo.TypeRef,
                    ColumnIndex = qo.ColumnIndex,
                    CSSText = qo.CSSText,
                    ParentOptionOID = qo.ParentOptionOID,
                    ControlEvents = qo.ControlEvents,
                    SourceCodeSystem = qo.SourceCodeSystem,
                    OtherAttributes = qo.OtherAttributes,
                    StatusCode = qo.StatusCode,
                    StatusCodeName = qo.StatusCodeName,
                }).ToList(),
            }).ToList(),
        };
    }

    public AcpFormEntity ReverseMap(AcpForm resource)
    {
        return new AcpFormEntity
        {
            Id = long.TryParse(resource.Id, out var id) ? id : 0,
            VersionId = int.TryParse(resource.VersionId, out var vid) ? vid : 0,
            FormType = resource.FormType,
            FormQuestions = resource.FormQuestions?.Select(q => new AcpFormEntity.FormQuestionComponent
            {
                OID = q.OID ?? 0,
                FormType = q.FormType,
                FormTypeName = q.FormTypeName,
                DiseaseType = q.DiseaseType,
                DiseaseTypeName = q.DiseaseTypeName,
                QuestionOID = q.QuestionOID ?? 0,
                QuestionTitle = q.QuestionTitle,
                IsMandatory = q.IsMandatory ?? false,
                DisplayOrder = q.DisplayOrder ?? 0
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
            Questions = resource.Questions?.Select(q => new AcpFormEntity.Question
            {
                OID = q.OID ?? 0,
                QuestionText = q.QuestionText,
                StatusCode = q.StatusCode,
                StatusCodeName = q.StatusCodeName,
                CSSText = q.CSSText,
                StyleText = q.StyleText,
                QuestionOptions = q.QuestionOptions?.Select(qo => new AcpFormEntity.QuestionOptionComponent
                {
                    OID = qo.OID ?? 0,
                    QuestionOID = qo.QuestionOID ?? 0,
                    Label = qo.Label,
                    Value = qo.Value,
                    DisplayOrder = qo.DisplayOrder ?? 0,
                    Type = qo.Type,
                    TypeRef = qo.TypeRef,
                    ColumnIndex = qo.ColumnIndex ?? 0,
                    CSSText = qo.CSSText,
                    ParentOptionOID = qo.ParentOptionOID ?? 0,
                    ControlEvents = qo.ControlEvents,
                    SourceCodeSystem = qo.SourceCodeSystem,
                    OtherAttributes = qo.OtherAttributes,
                    StatusCode = qo.StatusCode,
                    StatusCodeName = qo.StatusCodeName,
                }).ToList() ?? new()
            }).ToList() ?? new(),
        };
    }
}