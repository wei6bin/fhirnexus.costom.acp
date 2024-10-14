using Hl7.Fhir.Introspection;
using Ihis.FhirEngine.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Synapxe.FhirWebApi.CustomResource.Data
{
    [FhirType("Education", IsResource = true)]
    [PrimaryKey(nameof(Id), nameof(VersionId))]
    public class EducationModel : IResourceEntity<Guid>
    {
        public Guid Id { get; set; }

        public int? VersionId { get; set; }

        public bool IsHistory { get; set; }

        public string? Study { get; set; }

        public string? Institute { get; set; }

        public DateTimeOffset? LastUpdated { get; set; }

        public bool? HasGraduated { get; set; }

        public string? GraduatedDate { get; set; }

        public string? Subject { get; set; }

        public string? Tag { get; set; }

        public byte[]? TimeStamp { get; set; }
    }
}
