using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core.Handlers.Data;
using Ihis.FhirEngine.Core.Utility;
using static Hl7.Fhir.Model.Appointment;

namespace Synapxe.FhirWebApi.Custom.Data
{
    public sealed class AppointmentDataMapper : IFhirDataMapper<AppointmentModel, Appointment>
    {
        public AppointmentModel ReverseMap(Appointment appointment)
        {
            return new AppointmentModel
            {
                Id = Guid.Parse(appointment.Id),
                VersionId = int.TryParse(appointment.VersionId, out var vid) ? vid : 0,
                LastUpdated = appointment.Meta?.LastUpdated,
                Tag = appointment.Meta?.Tag.Select(x => x.Code).FirstOrDefault(),
                Status = EnumHelper.GetLiteral(appointment.Status),
                Start = appointment.Start,
                End = appointment.End,
                CancellationReason = appointment.CancellationReason?.Coding.FirstOrDefault()?.Code ?? appointment.CancellationReason?.Text,
                Patient = appointment.Participant
                    .Select(x => x.Actor.Reference)
                    .FirstOrDefault(x => x.StartsWith("Patient/")),
                Practitioner = appointment.Participant
                    .Select(x => x.Actor.Reference)
                    .FirstOrDefault(x => x.StartsWith("Practitioner/")),
                Location = appointment.Participant
                    .Select(x => x.Actor.Reference)
                    .FirstOrDefault(x => x.StartsWith("Location/")),
                Description = appointment.Description,
            };
        }

        public Appointment Map(AppointmentModel data)
        {
            var appointment = new Appointment
            {
                Id = data.Id.ToString("N").ToUpper(),
                Meta = new Meta
                {
                    VersionId = data.VersionId.ToString(),
                    LastUpdated = data.LastUpdated,
                },
                Status = EnumHelper.ParseLiteral<Appointment.AppointmentStatus>(data.Status),
                Start = data.Start,
                End = data.End,
                Participant = new List<Appointment.ParticipantComponent>(),
                Description = data.Description,
            };

            if (data.Tag != null)
            {
                appointment.Meta.Tag = new List<Coding>
                {
                    new Coding{ Code = data.Tag },
                };
            }

            if (data.Patient != null)
            {
                appointment.Participant.Add(new Appointment.ParticipantComponent
                {
                    Actor = new ResourceReference { Reference = data.Patient },
                    Status = ParticipationStatus.Tentative,
                });
            }

            if (data.Practitioner != null)
            {
                appointment.Participant.Add(new Appointment.ParticipantComponent
                {
                    Actor = new ResourceReference { Reference = data.Practitioner },
                    Status = ParticipationStatus.Tentative,
                });
            }

            if (data.Location != null)
            {
                appointment.Participant.Add(new Appointment.ParticipantComponent
                {
                    Actor = new ResourceReference { Reference = data.Location },
                    Status = ParticipationStatus.Tentative,
                });
            }

            if (data.CancellationReason != null)
            {
                appointment.CancellationReason = new CodeableConcept { Coding = new List<Coding> { new Coding { Code = data.CancellationReason } } };
            }

            return appointment;
        }
    }
}
