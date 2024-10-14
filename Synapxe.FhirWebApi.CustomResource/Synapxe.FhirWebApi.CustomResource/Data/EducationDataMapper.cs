using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core.Handlers.Data;
using Synapxe.FhirWebApi.CustomResource.Entities;

namespace Synapxe.FhirWebApi.CustomResource.Data
{
    public sealed class EducationDataMapper : IFhirDataMapper<EducationModel, Education>
    {
        public EducationModel ReverseMap(Education education)
        {
            return new EducationModel
            {
                Id = Guid.Parse(education.Id),
                VersionId = int.TryParse(education.VersionId, out var vid) ? vid : 0,
                LastUpdated = education.Meta?.LastUpdated,
                Tag = education.Meta?.Tag.Select(x => x.Code).FirstOrDefault(),
                Study = education.Study,
                HasGraduated = (education.Graduated as FhirBoolean)?.Value,
                GraduatedDate = (education.Graduated as Date)?.Value,
                Institute = education.Institute?.Reference,
                Subject = education.Subject?.Reference,
            };
        }

        public Education Map(EducationModel data)
        {
            var education = new Education
            {
                Id = data.Id.ToString("N").ToUpper(),
                Meta = new Meta
                {
                    VersionId = data.VersionId.ToString(),
                    LastUpdated = data.LastUpdated,
                },
                Graduated = data.GraduatedDate != null ? new Date(data.GraduatedDate) : new FhirBoolean(data.HasGraduated),
                Study = data.Study,
                Institute = new ResourceReference(data.Institute),
                Subject = new ResourceReference(data.Subject),
            };

            if (data.Tag != null)
            {
                education.Meta.Tag = new List<Coding>
                {
                    new Coding{ Code = data.Tag },
                };
            }

            return education;
        }
    }
}
