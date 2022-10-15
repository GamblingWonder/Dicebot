using DiceBot.Core;
using RestSharp;
using System.Net;

namespace Dicebot.Core
{
    public static class ConnectorHelpers
    {
        public static void ThowIfRequireCloudflare(this IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new CloudflareRequiredException();
            }
        }
    }

}