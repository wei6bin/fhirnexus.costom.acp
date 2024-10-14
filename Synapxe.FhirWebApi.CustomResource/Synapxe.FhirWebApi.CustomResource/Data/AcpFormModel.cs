using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Introspection;
using Ihis.FhirEngine.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Synapxe.FhirWebApi.CustomResource.Data;

[FhirType("AcpForm", IsResource = true)]
[PrimaryKey(nameof(Id))]
public class AcpFormModel : IResourceEntity<long>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("OID")]
    [Key]
    public long Id { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public int? VersionId { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public bool IsHistory { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public DateTimeOffset? LastUpdated { get; set; }

    [Timestamp]
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public byte[]? TimeStamp { get; set; }

    public string? FormType { get; set; }
}
