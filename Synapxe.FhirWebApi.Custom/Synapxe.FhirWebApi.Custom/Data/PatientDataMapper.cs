using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core.Handlers.Data;
using Ihis.FhirEngine.Core.Utility;

namespace Synapxe.FhirWebApi.Custom.Data;

public sealed class PatientDataMapper : IFhirDataMapper<PatientModel, Patient>
{
    private readonly string IdentifierSystem_NRIC = "http://synapxe.sg/acp_nric";
    private readonly string IdentifierSystem_Citizenship = "http://synapxe.sg/acp_citizenship";
    private readonly string IdentifierSystem_MaritalStatus = "http://synapxe.sg/acp_maritalstatus";
    private readonly string IdentifierSystem_Race = "http://synapxe.sg/acp_race";
    private readonly string IdentifierSystem_Religion = "http://synapxe.sg/acp_religion";

    private readonly string Extension_Citizenship = "http://synapxe.sg/extension/acp_citizenship";
    private readonly string Extension_MaritalStatus = "http://synapxe.sg/extension/acp_maritalstatus";
    private readonly string Extension_Race = "http://synapxe.sg/extension/acp_race";
    private readonly string Extension_Religion = "http://synapxe.sg/extension/acp_religion";

    public Patient Map(PatientModel model)
    {
        var patient = new Patient
        {
            Id = model.Id.ToString(),
            Meta = new Meta
            {
                VersionId = model.VersionId.ToString(),
                LastUpdated = model.LastUpdated,
            },
            Name = [new HumanName { Text = model.Name }],
            Gender = EnumHelper.ParseLiteral<AdministrativeGender>(model.Gender),
            BirthDate = new FhirDateTime(model.DateofBirth).ToString(),
            Identifier =
            [
                new Identifier
                {
                    System = IdentifierSystem_NRIC,
                    Value = model.NRIC.ToString(),
                },
            ],
            Extension =
            [
                new() {
                    Url = Extension_Citizenship,
                    Value = new CodeableConcept
                    {
                        Coding =
                        [
                            new Coding
                            {
                                System = IdentifierSystem_Citizenship,
                                Code = model.CitizenshipCode,
                                Display = model.CitizenshipName
                            },
                        ],
                    },
                },
            ],
            Address =
            [
                new Address
                {
                    PostalCode = model.PostalCode,
                    State = model.Street,
                    Use = Address.AddressUse.Home,
                    Line = [model.Block, model.Level?? "", model.Unit?? ""]
                },
            ]
        };

        if (model.IsPassedAway != null && model.IsPassedAway == true && model.PassedAwaydate != null)
        {
            patient.Deceased = new FhirDateTime(model.PassedAwaydate.Value);
        }
        else
        {
            patient.Deceased = new FhirBoolean(false);
        }

        if (model.MartialStatusCode != null)
        {
            patient.Extension.Add(new()
            {
                Url = Extension_MaritalStatus,
                Value = new CodeableConcept
                {
                    Coding =
                    [
                        new Coding
                        {
                            System = IdentifierSystem_MaritalStatus,
                            Code = model.MartialStatusCode,
                            Display = model.MartialStatusName
                        },
                    ],
                },
            });
        }

        if (model.RaceCode != null)
        {
            patient.Extension.Add(new()
            {
                Url = Extension_Race,
                Value = new CodeableConcept
                {
                    Coding =
                    [
                        new Coding
                        {
                            System = IdentifierSystem_Race,
                            Code = model.RaceCode,
                            Display = model.RaceName
                        }
                    ]
                }
            });
        }

        if (model.ReligionCode != null)
        {
            patient.Extension.Add(new()
            {
                Url = Extension_Religion,
                Value = new CodeableConcept
                {
                    Coding =
                    [
                        new Coding
                        {
                            System = IdentifierSystem_Religion,
                            Code = model.ReligionCode,
                            Display = model.ReligionName
                        }
                    ]
                }
            });
        }

        return patient;
    }

    public PatientModel ReverseMap(Patient resource)
    {
        var patientModel = new PatientModel
        {
            Id = long.TryParse(resource.Id, out long resourceId) ? resourceId : default,
            VersionId = int.TryParse(resource.VersionId, out var vid) ? vid : 0,
            LastUpdated = resource.Meta?.LastUpdated,
            Name = resource.Name.FirstOrDefault()?.Text,
            NRIC = resource.Identifier.FirstOrDefault(x => x.System == IdentifierSystem_NRIC)?.Value,
            DateofBirth = DateTime.Parse(resource.BirthDate),
            Gender = resource.Gender.ToString()
        };

        var citizenship = resource.Extension.FirstOrDefault(x => x.Url == Extension_Citizenship)?.Value as CodeableConcept;
        if (citizenship != null)
        {
            patientModel.CitizenshipCode = citizenship.Coding.FirstOrDefault()?.Code;
            patientModel.CitizenshipName = citizenship.Coding.FirstOrDefault()?.Display;
        }

        resource.Address.ForEach(address =>
        {
            patientModel.Block = address.Line.FirstOrDefault();
            patientModel.Level = address.Line.ElementAtOrDefault(1);
            patientModel.Unit = address.Line.ElementAtOrDefault(2);
            patientModel.Street = address.State;
            patientModel.PostalCode = address.PostalCode;
        });

        return patientModel;
    }
}