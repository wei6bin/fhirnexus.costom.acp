using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core.Handlers.Data;
using Synapxe.FhirWebApi.Custom.Data.Model;

namespace Synapxe.FhirWebApi.Custom.Data.Mapper
{
    public class QuestionnaireResponseDataMapper : IFhirDataMapper<QuestionnaireResponseModel, QuestionnaireResponse>
    {
        public QuestionnaireResponse Map(QuestionnaireResponseModel resource)
        {
            throw new NotImplementedException();
        }

        public QuestionnaireResponseModel ReverseMap(QuestionnaireResponse resource)
        {
            throw new NotImplementedException();
        }
    }
}
