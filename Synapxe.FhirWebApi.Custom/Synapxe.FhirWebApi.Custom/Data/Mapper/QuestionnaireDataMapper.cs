using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Ihis.FhirEngine.Core.Handlers.Data;
using Synapxe.FhirWebApi.Custom.Data.Model;

namespace Synapxe.FhirWebApi.Custom.Data.Mapper;

public class QuestionnaireDataMapper : IFhirDataMapper<QuestionnaireModel, Questionnaire>
{
    public Questionnaire Map(QuestionnaireModel resource)
    {
        return new Questionnaire
        {
            Id = resource.Id.ToString(),
            Status = PublicationStatus.Active,
            Title = resource.FormType,
        };
    }

    public QuestionnaireModel ReverseMap(Questionnaire resource)
    {
        return new QuestionnaireModel
        {
            Id = long.Parse(resource.Id),
            FormType = PublicationStatus.Active.GetLiteral(),
        };
    }
}
