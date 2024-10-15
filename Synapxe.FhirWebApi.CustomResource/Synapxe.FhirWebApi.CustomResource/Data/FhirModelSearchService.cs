using Hl7.Fhir.Model;
using Ihis.FhirEngine.Core;
using Ihis.FhirEngine.Core.Exceptions;
using Ihis.FhirEngine.Core.Extensions;
using Ihis.FhirEngine.Core.Handlers.Data;
using Ihis.FhirEngine.Core.Search;
using Synapxe.FhirWebApi.CustomResource.Entities;

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
            "AcpForm"
        };

        public async Task<SearchResult> SearchAsync(string resourceType, IReadOnlyList<(string, string)> queryParameters, bool isHistory, CancellationToken cancellationToken)
        {
            switch (resourceType)
            {
                case "AcpForm":
                    return await NewMethod(queryParameters, cancellationToken).ConfigureAwait(false);
                default:
                    throw new InvalidSearchOperationException($"SearchService '{Id}' does not support the searching the resource type '{resourceType}'.");
            }
        }

        private async Task<SearchResult> NewMethod(IReadOnlyList<(string, string)> queryParameters, CancellationToken cancellationToken)
        {
            IQueryable<AcpFormEntity> query = dbContext.Questionnaire;
            int pageCount = 10;
            int skip = 0;
            var unsupported = new List<(string, string)>();

            foreach (var (searchParam, modifier, value) in queryParameters.ProcessSearchParams(ref pageCount, ref skip))
            {
                query = searchParam switch
                {
                    "formType" => query.WhereAny(value, str => x => x.FormType == str, modifier),
                    KnownQueryParameterNames.Sort => query.OrderByFhir(value, builder => builder.OrderFor(KnownQueryParameterNames.LastUpdated, e => e.LastUpdated)),
                    _ => query.AddUnsupportedSearchParameter(unsupported, searchParam + modifier, value),
                };
            }

            return await query.ToPagedSearchResultAsync<AcpFormEntity, AcpForm>(
                dataMapperFactory,
                pageCount,
                skip,
                unsupported,
                cancellationToken).ConfigureAwait(false);
        }

        public Task<SearchResult> SearchCompartmentAsync(string compartmentType, string compartmentId, string? resourceType, IReadOnlyList<(string, string)> queryParameters, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
