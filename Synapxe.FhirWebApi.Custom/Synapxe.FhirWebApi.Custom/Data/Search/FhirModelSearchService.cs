using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core;
using Ihis.FhirEngine.Core.Exceptions;
using Ihis.FhirEngine.Core.Extensions;
using Ihis.FhirEngine.Core.Handlers.Data;
using Ihis.FhirEngine.Core.Search;
using Synapxe.FhirWebApi.Custom.Data.Model;

namespace Synapxe.FhirWebApi.Custom.Data.Search
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

        public IEnumerable<string> AcceptedTypes { get; } =
        [
            "Appointment",
            "Patient",
            "Questionnaire"
        ];

        public async Task<SearchResult> SearchAsync(string resourceType, IReadOnlyList<(string, string)> queryParameters, bool isHistory, CancellationToken cancellationToken)
        {
            switch (resourceType)
            {
                case "Appointment":
                    return await SearchAppointment(queryParameters, cancellationToken).ConfigureAwait(false);
                case "Patient":
                    return await SearchPatient(queryParameters, cancellationToken).ConfigureAwait(false);
                case "Questionnaire":
                    return await SearchQuestionnaire(queryParameters, cancellationToken).ConfigureAwait(false);
                default:
                    throw new InvalidSearchOperationException($"SearchService '{Id}' does not support the searching the resource type '{resourceType}'.");
            }
        }

        private async Task<SearchResult> SearchPatient(IReadOnlyList<(string, string)> queryParameters, CancellationToken cancellationToken)
        {
            IQueryable<PatientModel> query = dbContext.Patient;
            int pageCount = 10;
            int skip = 0;
            var unsupported = new List<(string, string)>();

            foreach (var (searchParam, modifier, value) in queryParameters.ProcessSearchParams(ref pageCount, ref skip))
            {
                query = searchParam switch
                {
                    "gender" => query.WhereAny(value, str => x => x.Gender == str, modifier),
                    "birthdate" => query.WherePartialDateTimeMatch(value, x => x.DateofBirth, modifier),
                    "identifier" => query.WhereAny(value, str => x => x.NRIC == str, modifier),
                    "name" => query.WhereAny(value, str => x => x.Name == str, modifier),
                    KnownQueryParameterNames.LastUpdated => query.WherePartialDateTimeMatch(value, x => x.LastUpdated!.Value, modifier),
                    KnownQueryParameterNames.Id when long.TryParse(value, out var idguid) => query.Where(x => x.Id == idguid),
                    KnownQueryParameterNames.Id => query.Where(x => false),
                    KnownQueryParameterNames.Sort => query.OrderByFhir(value, builder => builder.OrderFor(KnownQueryParameterNames.LastUpdated, e => e.LastUpdated)),
                    _ => query.AddUnsupportedSearchParameter(unsupported, searchParam + modifier, value),
                };
            }

            return await query.ToPagedSearchResultAsync<PatientModel, Patient>(
                dataMapperFactory,
                pageCount,
                skip,
                unsupported,
                cancellationToken).ConfigureAwait(false);
        }

        private async Task<SearchResult> SearchAppointment(IReadOnlyList<(string, string)> queryParameters, CancellationToken cancellationToken)
        {
            IQueryable<AppointmentModel> query = dbContext.Appointment;
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

            return await query.ToPagedSearchResultAsync<AppointmentModel, Appointment>(
                dataMapperFactory,
                pageCount,
                skip,
                unsupported,
                cancellationToken).ConfigureAwait(false);
        }

        private async Task<SearchResult> SearchQuestionnaire(IReadOnlyList<(string, string)> queryParameters, CancellationToken cancellationToken)
        {
            IQueryable<QuestionnaireModel> query = dbContext.Questionnaire;
            int pageCount = 10;
            int skip = 0;
            var unsupported = new List<(string, string)>();

            foreach (var (searchParam, modifier, value) in queryParameters.ProcessSearchParams(ref pageCount, ref skip))
            {
                query = searchParam switch
                {
                    "status" => query.WhereAny(value, str => x => true, modifier),
                    "name" => query.WhereAny(value, str => x => x.FormType == str, modifier),
                    KnownQueryParameterNames.LastUpdated => query.WherePartialDateTimeMatch(value, x => x.LastUpdated!.Value, modifier),
                    KnownQueryParameterNames.Id when long.TryParse(value, out var idguid) => query.Where(x => x.Id == idguid),
                    KnownQueryParameterNames.Id => query.Where(x => false),
                    KnownQueryParameterNames.Sort => query.OrderByFhir(value, builder => builder.OrderFor(KnownQueryParameterNames.LastUpdated, e => e.LastUpdated)),
                    _ => query.AddUnsupportedSearchParameter(unsupported, searchParam + modifier, value),
                };
            }

            return await query.ToPagedSearchResultAsync<QuestionnaireModel, Questionnaire>(
                dataMapperFactory,
                pageCount,
                skip,
                unsupported,
                cancellationToken).ConfigureAwait(false);
        }

        public Task<SearchResult> SearchCompartmentAsync(string compartmentType, string compartmentId, string? resourceType, IReadOnlyList<(string Key, string Value)> queryParameters, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
