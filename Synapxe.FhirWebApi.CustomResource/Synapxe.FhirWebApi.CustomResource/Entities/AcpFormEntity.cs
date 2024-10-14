using Hl7.Fhir.Introspection;
using Ihis.FhirEngine.Data.Models;

namespace Synapxe.FhirWebApi.CustomResource.Entities;

[CustomFhirResource]
[FhirType("AcpForm", "http://hl7.org/fhir/StructureDefinition/AcpForm")]
public partial class AcpFormEntity : ResourceEntity
{
    public string FormType { get; set; }
}
