namespace DiceBot.Core.Connectors
{
    public class ClientSettings : ConnectorSettings
    {

        public string ApiEndPoint { get; set; }

        public string WebSocketEndPoint { get; set; }

        public string GraphQLEndPoint { get; set; }

        public string SiteFormat { get; set; } = "https://{0}";

        public string EndpointFormat { get; set; } = "https://{0}";


        public override void Update(string site = "", string apiKey = "")
        {

            if (!string.IsNullOrEmpty(site))
            {
                this.Site = site;
                this.Referrer = $"https://{site}";
                this.ApiEndPoint = $"https://{site}/_api";
                this.WebSocketEndPoint = $"wss://{site}/_api/graphql";
                this.GraphQLEndPoint = $"https://{site}/_api/graphql";
            }

            if (!string.IsNullOrEmpty(apiKey))
            {
                this.ApiKey = apiKey;
            }

        }



        public ClientSettings() : base()
        {
        }

        public ClientSettings(string siteDomain) : base()
        {
            Update(siteDomain);
        }

    }
}
