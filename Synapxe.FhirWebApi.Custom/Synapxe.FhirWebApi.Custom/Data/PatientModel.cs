using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hl7.Fhir.Introspection;
using Ihis.FhirEngine.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Synapxe.FhirWebApi.Custom.Data
{
    [FhirType("Patient", IsResource = true)]
    [PrimaryKey(nameof(Id), nameof(VersionId))]
    public class PatientModel : IResourceEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int? VersionId { get; set; }

        public bool IsHistory { get; set; }

        public string? Name { get; set; }

        public string NRIC { get; set; }

        public string CitizenshipCode { get; internal set; }

        public string CitizenshipName { get; internal set; }

        public string? Gender { get; set; }

        public DateTime DateofBirth { get; set; }

        public DateTimeOffset? LastUpdated { get; set; }

        [Timestamp]
        public byte[]? TimeStamp { get; set; }

        public string? MartialStatusCode { get; set; }

        public string? MartialStatusName { get; set; }

        public string? RaceCode { get; set; }

        public string? RaceName { get; set; }

        public string? ReligionCode { get; set; }

        public string? ReligionName { get; set; }

        public bool? IsPassedAway { get; set; }

        public DateTime? PassedAwaydate { get; set; }

        public string Block { get; set; }
        public string? Level { get; set; }
        public string? Unit { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
