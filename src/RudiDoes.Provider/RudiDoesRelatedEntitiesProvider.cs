using System.Collections.Generic;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.DataStore.Document.Models;
using CluedIn.RelatedEntities;
using Microsoft.Extensions.Logging;

namespace CluedIn.Provider.RudiDoes
{
    public class RudiDoesRelatedEntitiesProvider : IRelatedEntitiesProvider
    {
        public IEnumerable<SuggestedSearch> GetRelatedEntitiesSearches(ExecutionContext context, Entity entity)
        {
            // use the provided Log object
            var Log = context.Log;

            Log.LogInformation($"[RudiDoes] RudiDoesRelatedEntitiesProvider.GetRelatedEntitiesSearches({context}, {entity})");
            if (entity.Type != EntityType.Organization)
            {
                Log.LogInformation("[RudiDoes] Entity is not an Organization - nothing to suggest");
                return new SuggestedSearch[0];
            }

            var searches = new List<SuggestedSearch>();
            if (RelatedEntitiesUtility.CypherFluentQueriesCount("{{RELATIONSHIP}} for {{ENTITY}}", string.Format("{0},{1}", "/RudiDoes", entity.Id.ToString()), context) > 0)
            {
                Log.LogInformation("[RudiDoes] CypherFluentQueries matches - adding suggested search");
                searches.Add(new SuggestedSearch {
                DisplayName = "RudiDoes",
                SearchQuery = "{{RELATIONSHIP}} for {{ENTITY}}",
                Tokens = string.Format("{0},{1}", "/RudiDoes", entity.Id.ToString()),
                Type = "List"
                });
            }
            else
            {
                Log.LogInformation("[RudiDoes] CypherFluentQueries does not match - nothing to suggest");
            }
            return searches;
        }
    }
}
