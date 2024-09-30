using System.ComponentModel.DataAnnotations.Schema;
using Hl7.Fhir.Introspection;
using Ihis.FhirEngine.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Synapxe.FhirWebApi.Custom.Data
{
    [FhirType("Appointment", IsResource = true)]
    [PrimaryKey(nameof(Id), nameof(VersionId))]
    public class AppointmentModel : IResourceEntity<Guid>
    {
        public Guid Id { get; set; }

        public int? VersionId { get; set; }

        public bool IsHistory { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset? Start { get; set; }

        public DateTimeOffset? End { get; set; }

        public string? Patient { get; set; }

        public string? Practitioner { get; set; }

        public string? Location { get; set; }

        public DateTimeOffset? LastUpdated { get; set; }

        public string? CancellationReason { get; set; }

        public string? Tag { get; set; }

        public byte[]? TimeStamp { get; set; }
    }
}
