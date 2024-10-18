using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core.Handlers.Data;

namespace Synapxe.FhirWebApi.Relational.Data;

public class QuestionnaireResponseDataMapper : IFhirDataMapper<QuestionnaireResponseEntity, QuestionnaireResponse>
{
    public QuestionnaireResponse Map(QuestionnaireResponseEntity resource)
    {
        throw new NotImplementedException();
    }

    public QuestionnaireResponseEntity ReverseMap(QuestionnaireResponse resource)
    {
        throw new NotImplementedException();
    }
}
