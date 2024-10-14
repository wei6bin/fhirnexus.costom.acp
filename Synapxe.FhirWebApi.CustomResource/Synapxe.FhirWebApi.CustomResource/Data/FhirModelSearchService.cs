using Ihis.FhirEngine.Core;
using Ihis.FhirEngine.Core.Exceptions;
using Ihis.FhirEngine.Core.Extensions;
using Ihis.FhirEngine.Core.Handlers.Data;
using Ihis.FhirEngine.Core.Search;

namespace Synapxe.FhirWebApi.CustomResource.Data
{
    public class FhirModelSearchService : IContextSearchService<FhirModelDbContext>
    {
        private readonly FhirModelDbContext dbContext;
        private readonly IFhirDataMapperFactory dataMapperFactory;

        public FhirModelSearchService(FhirModelDbContext dbContext, IFhirDataMapperFactory dataMapperFactory)
        {
            this.dbContext = dbContext;
            this.dataMapperFactory = dataMapperFactory;
        }

        public string Id => nameof(FhirModelSearchService);

        public IEnumerable<string> AcceptedTypes { get; } = new[]
        {
            "Education",
            "Appointment"
        };

        public async Task<SearchResult> SearchAsync(string resourceType, IReadOnlyList<(string, string)> queryParameters, bool isHistory, CancellationToken cancellationToken)
        {
            if (resourceType == "Appointment")
            {
                IQueryable<AppointmentModel> query = dbContext.Appointments;
                int pageCount = 10;
                int skip = 0;
                var unsupported = new List<(string, string)>();

                foreach (var (searchParam, modifier, value) in queryParameters.ProcessSearchParams(ref pageCount, ref skip))
                {
                    query = searchParam switch
                    {
                        "_tag" => query.WhereAny(value, str => x => x.Tag == str, modifier),
                        "patient" => query.WhereAny(value, str => x => x.Patient == str, modifier),
                        "actor" => query.WhereAny(value, str => x => x.Patient == str || x.Location == str || x.Practitioner == str, modifier),
                        "date" => query.WherePartialDateTimeMatch(value, x => x.Start!.Value, x => x.End!.Value, modifier),
                        KnownQueryParameterNames.LastUpdated => query.WherePartialDateTimeMatch(value, x => x.LastUpdated!.Value, modifier),
                        KnownQueryParameterNames.Id when Guid.TryParse(value, out var idguid) => query.Where(x => x.Id == idguid),
                        KnownQueryParameterNames.Id => query.Where(x => false),
                        KnownQueryParameterNames.Sort => query.OrderByFhir(value, builder => builder.OrderFor(KnownQueryParameterNames.LastUpdated, e => e.LastUpdated)),
                        _ => query.AddUnsupportedSearchParameter(unsupported, searchParam + modifier, value),
                    };
                }

                return await query.ToPagedSearchResultAsync<AppointmentModel, Hl7.Fhir.Model.Appointment>(
                    dataMapperFactory,
                    pageCount,
                    skip,
                    unsupported,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (resourceType == "Education")
            {
                IQueryable<EducationModel> query = dbContext.Education;
                int pageCount = 10;
                int skip = 0;
                var unsupported = new List<(string, string)>();

                foreach (var (searchParam, modifier, value) in queryParameters.ProcessSearchParams(ref pageCount, ref skip))
                {
                    query = searchParam switch
                    {
                        "_tag" => query.WhereAny(value, str => x => x.Tag == str, modifier),
                        "institute" => query.WhereAny(value, str => x => x.Institute == str, modifier),
                        "study" => query.WhereStringMatch(value, x => x.Study, modifier),
                        KnownQueryParameterNames.LastUpdated => query.WherePartialDateTimeMatch(value, x => x.LastUpdated!.Value, modifier),
                        KnownQueryParameterNames.Id when Guid.TryParse(value, out var idguid) => query.Where(x => x.Id == idguid),
                        KnownQueryParameterNames.Id => query.Where(x => false),
                        KnownQueryParameterNames.Sort => query.OrderByFhir(value, builder => builder.OrderFor(KnownQueryParameterNames.LastUpdated, e => e.LastUpdated)),
                        _ => query.AddUnsupportedSearchParameter(unsupported, searchParam + modifier, value),
                    };
                }

                return await query.ToPagedSearchResultAsync<EducationModel, Synapxe.FhirWebApi.CustomResource.Entities.Education>(
                    dataMapperFactory,
                    pageCount,
                    skip,
                    unsupported,
                    cancellationToken).ConfigureAwait(false);
            }
            else
            {
                throw new InvalidSearchOperationException($"SearchService '{Id}' does not support the searching the resource type '{resourceType}'.");
            }
        }

        public Task<SearchResult> SearchCompartmentAsync(string compartmentType, string compartmentId, string? resourceType, IReadOnlyList<(string, string)> queryParameters, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
