using System.Collections.Generic;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.DataStore.Document.Models;
using CluedIn.RelatedEntities;

namespace CluedIn.Provider.RudiDoes
{
    public class RudiDoesRelatedEntitiesProvider : IRelatedEntitiesProvider
    {
        public IEnumerable<SuggestedSearch> GetRelatedEntitiesSearches(ExecutionContext context, Entity entity)
        {
            if (entity.Type != EntityType.Organization)
            {
                return new SuggestedSearch[0];
            }

            var searches = new List<SuggestedSearch>();
            if (RelatedEntitiesUtility.CypherFluentQueriesCount("{{RELATIONSHIP}} for {{ENTITY}}", string.Format("{0},{1}", "/RudiDoes", entity.Id.ToString()), context) > 0)
            {
                searches.Add(new SuggestedSearch {
                DisplayName = "RudiDoes",
                SearchQuery = "{{RELATIONSHIP}} for {{ENTITY}}",
                Tokens = string.Format("{0},{1}", "/RudiDoes", entity.Id.ToString()),
                Type = "List"
                });
            }
            return searches;
        }
    }
}