using DiceBot;
using DiceBot.Core;
using DiceBot.Core.Connectors;
using DiceBot.Core.Request;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Connectors.Primedice
{

    public class PrimeDiceConnector : ConnectorClientManagerBase, ISiteConnector
    {

        public RestClient SharedRestClient { get; private set; }

        public PrimeDiceConnector()
        {
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34";
        }

        public PrimeDiceConnector(string site, string apikey) : this()
        {
            Domain = site;
            ApiKey = apikey;
            HostEndpoint = $"https://{Domain}";
            ApiEndpoint = $"https://{Domain}/_api";
            GraphqlEndpoint = $"https://{Domain}/_api/graphql";
        }

        public PrimeDiceConnector(ClientSettings settings) : this()
        {
            Domain = settings.Site;
            ApiKey = settings.ApiKey;
            HostEndpoint = $"https://{Domain}";
            ApiEndpoint = $"https://{Domain}/_api";
            GraphqlEndpoint = $"https://{Domain}/_api/graphql";
            Settings = settings;
        }

        public override void UpdateSharedRestClient()
        {
            base.UpdateSharedRestClient();

            if (Settings.PersistentCookies != null)
            {
                foreach (var cookie in Settings.PersistentCookies)
                {
                    this.Cookies.Add(cookie);
                }
            }

            SharedRestClient.CookieContainer = this.Cookies;
            SharedRestClient.UserAgent = Settings.UserAgent ?? UserAgent;

        }


        private void CreateOrUseDefaultRestClient(bool dispose = false)
        {

            if (dispose == true)
            {
                SharedRestClient = null;
                Cookies = null;
            }

            if (SharedRestClient != null)
            {
                return;
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            this.Cookies = new CookieContainer();

            if (Settings.PersistentCookies != null)
            {
                foreach (var cookie in Settings.PersistentCookies)
                {
                    this.Cookies.Add(cookie);
                }
            }

            SharedRestClient = new RestClient(new Uri(GraphqlEndpoint));
            SharedRestClient.CookieContainer = this.Cookies;
            SharedRestClient.UserAgent = Settings.UserAgent ?? UserAgent;// "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36";

        }



        private RestRequest CreateDefaultRestRequest(string apiKey, bool includeCloudflareHeaders = false)
        {
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("authorization", string.Format("Bearer {0}", apiKey));
            restRequest.AddHeader("x-access-token", apiKey);
            restRequest.AddHeader("Content-type", "application/json");

            if (includeCloudflareHeaders)
            {
                restRequest.AddHeader("referer", Settings.Referrer);
                restRequest.AddHeader("Upgrade-Insecure-Requests", "1");
            }

            return restRequest;
        }


        public IRestResponse ExecuteSync(RequestPayload payload)
        {
            try
            {
                CreateOrUseDefaultRestClient();
                var request = CreateDefaultRestRequest(ApiKey);
                request.AddJsonBody(payload);
                var restResponse = SharedRestClient.ExecuteAsync(request).Result;
                return restResponse;
            }
            catch (WebException ex)
            {

                Debug.WriteLine(ex);

                //if (e.Status == . == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.ServiceUnavailable)
                //{
                //    throw new CloudflareRequiredException();
                //}
                throw new Exception(ex.Message, ex);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public TResult ExecuteSync<TResult>(RequestPayload payload)
        {
            try
            {
                var restResponse = ExecuteSync(payload);
                var response = JsonConvert.DeserializeObject<TResult>(restResponse.Content);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public async Task<IRestResponse> ExecuteAsync(RequestPayload payload)
        {
            try
            {
                CreateOrUseDefaultRestClient();
                var request = CreateDefaultRestRequest(ApiKey);
                request.AddJsonBody(payload);
                var restResponse = await SharedRestClient.ExecuteAsync(request);
                return restResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }

}