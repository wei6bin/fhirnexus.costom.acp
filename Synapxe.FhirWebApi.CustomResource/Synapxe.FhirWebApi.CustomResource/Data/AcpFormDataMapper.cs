using Ihis.FhirEngine.Core.Handlers.Data;
using Synapxe.FhirWebApi.CustomResource.Entities;

namespace Synapxe.FhirWebApi.CustomResource.Data;

public class AcpFormDataMapper : IFhirDataMapper<AcpFormModel, AcpForm>
{
    public AcpForm Map(AcpFormModel resource)
    {
        throw new NotImplementedException();
    }

    public AcpFormModel ReverseMap(AcpForm resource)
    {
        throw new NotImplementedException();
    }
}