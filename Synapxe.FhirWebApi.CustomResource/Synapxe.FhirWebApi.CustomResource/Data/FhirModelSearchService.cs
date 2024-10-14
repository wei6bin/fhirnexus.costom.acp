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
            throw new InvalidSearchOperationException($"SearchService '{Id}' does not support the searching the resource type '{resourceType}'.");
        }

        public Task<SearchResult> SearchCompartmentAsync(string compartmentType, string compartmentId, string? resourceType, IReadOnlyList<(string, string)> queryParameters, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
