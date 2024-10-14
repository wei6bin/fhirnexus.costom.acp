using Ihis.FhirEngine.Core.Handlers.Data;
using Synapxe.FhirWebApi.CustomResource.Entities;

namespace Synapxe.FhirWebApi.CustomResource.Data;

public class AcpFormDataMapper : IFhirDataMapper<AcpFormEntity, AcpForm>
{
    public AcpForm Map(AcpFormEntity resource)
    {
        throw new NotImplementedException();
    }

    public AcpFormEntity ReverseMap(AcpForm resource)
    {
        throw new NotImplementedException();
    }
}