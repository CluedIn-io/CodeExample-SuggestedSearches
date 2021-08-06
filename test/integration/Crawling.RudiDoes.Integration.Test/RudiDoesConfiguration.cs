using System.Collections.Generic;
using CluedIn.Crawling.RudiDoes.Core;

namespace CluedIn.Crawling.RudiDoes.Integration.Test
{
  public static class RudiDoesConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { RudiDoesConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
