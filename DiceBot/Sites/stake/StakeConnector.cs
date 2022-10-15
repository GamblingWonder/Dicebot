using Connectors.Stake.Response;
using Dicebot.Core;
using DiceBot.Core;
using DiceBot.Core.Connectors;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using static DiceBot.PDStake;

namespace Connectors.Stake
{
    public class StakeConnector : ConnectorClientManagerBase, ISiteConnector
    {

        public RestClient SharedRestClient { get; private set; }

        public StakeConnector()
        {
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36 Edg/106.0.1370.34";
        }

        public StakeConnector(string site, string apikey) : this()
        {
            Domain = site;
            ApiKey = apikey;
            HostEndpoint = $"https://{Domain}";
            ApiEndpoint = $"https://{Domain}/_api";
            GraphqlEndpoint = $"https://{Domain}/_api/graphql";
        }

        public StakeConnector(ClientSettings settings) : this()
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



        //public async Task<IRestResponse> Authorize()
        //{
        //    try
        //    {
        //        var payload = new BetQuery
        //        {
        //            operationName = "initialUserRequest",
        //            variables = new BetClass() { },
        //            query = "query initialUserRequest {\n  user {\n    ...UserAuth\n    __typename\n  }\n}\n\nfragment UserAuth on User {\n  id\n  name\n  email\n  hasPhoneNumberVerified\n  hasEmailVerified\n  hasPassword\n  intercomHash\n  createdAt\n  hasTfaEnabled\n  mixpanelId\n  hasOauth\n  isKycBasicRequired\n  isKycExtendedRequired\n  isKycFullRequired\n  kycBasic {\n    id\n    status\n    __typename\n  }\n  kycExtended {\n    id\n    status\n    __typename\n  }\n  kycFull {\n    id\n    status\n    __typename\n  }\n  flags {\n    flag\n    __typename\n  }\n  roles {\n    name\n    __typename\n  }\n  balances {\n    ...UserBalanceFragment\n    __typename\n  }\n  activeClientSeed {\n    id\n    seed\n    __typename\n  }\n  previousServerSeed {\n    id\n    seed\n    __typename\n  }\n  activeServerSeed {\n    id\n    seedHash\n    nextSeedHash\n    nonce\n    blocked\n    __typename\n  }\n  __typename\n}\n\nfragment UserBalanceFragment on UserBalance {\n  available {\n    amount\n    currency\n    __typename\n  }\n  vault {\n    amount\n    currency\n    __typename\n  }\n  __typename\n}\n"
        //        };
        //        CreateOrUseDefaultRestClient();
        //        var request = CreateDefaultRestRequest(ApiKey);
        //        request.AddJsonBody(payload);
        //        var restResponse = SharedRestClient.ExecuteAsync(request).Result;
        //        return restResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        //luaPrint(ex.Message);
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        public async Task<IRestResponse> SendToVault(string currency, decimal sentamount)
        {
            try
            {

                var payload = new BetQuery
                {
                    variables = new BetClass()
                    {
                        currency = currency.ToLower(),
                        amount = sentamount

                    },
                    query = "mutation CreateVaultDeposit($currency: CurrencyEnum!, $amount: Float!) {\n  createVaultDeposit(currency: $currency, amount: $amount) {\n    id\n    amount\n    currency\n    user {\n      id\n      balances {\n        available {\n          amount\n          currency\n          __typename\n        }\n        vault {\n          amount\n          currency\n          __typename\n        }\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n"
                };

                CreateOrUseDefaultRestClient();

                var request = CreateDefaultRestRequest(ApiKey);

                request.AddJsonBody(payload);

                var restResponse = SharedRestClient.ExecuteAsync(request).Result;

                return restResponse;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IRestResponse> SendTip()
        {

            try
            {

                //BetQuery payload = new BetQuery
                //{
                //    variables = new BetClass()
                //    {
                //        seed = RandomString(10)
                //    },
                //    query = "mutation RotateSeedPair($seed: String!) {\n  rotateSeedPair(seed: $seed) {\n    clientSeed {\n      user {\n        id\n        activeClientSeed {\n          id\n          seed\n          __typename\n        }\n        activeServerSeed {\n          id\n          nonce\n          seedHash\n          nextSeedHash\n          __typename\n        }\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n"
                //};
                //CreateOrUseDefaultRestClient();
                //var request = CreateDefaultRestRequest(ApiKey);
                //request.AddJsonBody(payload);
                //var restResponse = await SharedRestClient.ExecuteAsync(request);
                //return restResponse;

                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<IRestResponse> CheckBalance()
        {
            try
            {

                var payload = new BetQuery
                {
                    query = "query UserBalances {\n  user {\n    id\n    balances {\n      available {\n        amount\n        currency\n        __typename\n      }\n      vault {\n        amount\n        currency\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n"
                };

                CreateOrUseDefaultRestClient();

                var request = CreateDefaultRestRequest(ApiKey);

                request.AddJsonBody(payload);

                var restResponse = SharedRestClient.ExecuteAsync(request).Result;

                return restResponse;


            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public async Task<IRestResponse> PlaceRouletteBet(string currencySelected)
        {
            try
            {

                var payload = new BetQuery
                {
                    variables = new BetClass()
                    {
                        //numbers = number.Count > 0 ? number : new List<Values> { },
                        //rows = row.Count > 0 ? row : new List<Values> { },
                        //colors = color.Count > 0 ? color : new List<Values> { },
                        //parities = parity.Count > 0 ? parity : new List<Values> { },
                        //ranges = range.Count > 0 ? range : new List<Values> { },
                        currency = currencySelected,
                        identifier = RandomString(21)

                    },
                    query = "mutation RouletteBet($currency: CurrencyEnum!, $colors: [RouletteBetColorsInput!], $numbers: [RouletteBetNumbersInput!], $parities: [RouletteBetParitiesInput!], $ranges: [RouletteBetRangesInput!], $rows: [RouletteBetRowsInput!], $identifier: String!) {\n  rouletteBet(\n    currency: $currency\n    colors: $colors\n    numbers: $numbers\n    parities: $parities\n    ranges: $ranges\n    rows: $rows\n    identifier: $identifier\n  ) {\n    ...CasinoBet\n    state {\n      ...RouletteStateFragment\n    }\n  }\n}\n\nfragment CasinoBet on CasinoBet {\n  id\n  active\n  payoutMultiplier\n  amountMultiplier\n  amount\n  payout\n  updatedAt\n  currency\n  game\n  user {\n    id\n    name\n  }\n}\n\nfragment RouletteStateFragment on CasinoGameRoulette {\n  result\n  colors {\n    amount\n    value\n  }\n  numbers {\n    amount\n    value\n  }\n  parities {\n    amount\n    value\n  }\n  ranges {\n    amount\n    value\n  }\n  rows {\n    amount\n    value\n  }\n}\n"
                };

                CreateOrUseDefaultRestClient();

                var request = CreateDefaultRestRequest(ApiKey);

                request.AddJsonBody(payload);

                var restResponse = await SharedRestClient.ExecuteAsync(request);

                return restResponse;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IRestResponse> PlaceKenoBet(BetQuery payload)
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
                return null;
            }
        }
        public async Task<IRestResponse> PlaceLimboBet(BetQuery payload)
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
                return null;
            }
        }
        public async Task<IRestResponse> PlaceDiceBet(BetQuery payload)
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
                return null;
            }
        }

        /*
        public async Task<ResponseBaseAs<TResult>> PlaceDiceBet(decimal amount, decimal target, string condition, string currency)
        {
            try
            {
              

                CreateOrUseDefaultRestClient();

                var request = CreateDefaultRestRequest(ApiKey);

                var payload = new RequestPayload()
                {
                    operationName = "DiceRoll",
                    query = @"mutation DiceRoll($amount: Float! 
  $target: Float!
  $condition: CasinoGameDiceConditionEnum!
  $currency: CurrencyEnum!
  $identifier: String!){ diceRoll(amount: $amount, target: $target, condition: $condition, currency: $currency, identifier: $identifier)" +
       " { id nonce currency amount payout state { ... on CasinoGameDice { result target condition } } createdAt serverSeed{seedHash seed nonce} clientSeed{seed} user{balances{available{amount currency}} statistic{game bets wins losses betAmount profit currency}}}}",
                    variables = new BetClass
                    {
                        amount = amount,
                        target = target,
                        condition = condition,
                        currency = currency,
                        identifier = RandomString(21)
                    }
                };

                request.AddJsonBody(payload);

                var restResponse = SharedRestClient.ExecuteAsync(request).Result;

                return restResponse;

            }
            catch (Exception ex)
            {
                //luaPrint(ex.Message);
                return null;
            }
        }
        */


        public async Task<IRestResponse> ResetSeeds()
        {
            try
            {

                var payload = new BetQuery
                {
                    variables = new BetClass()
                    {
                        seed = RandomString(10)

                    },
                    query = "mutation RotateSeedPair($seed: String!) {\n  rotateSeedPair(seed: $seed) {\n    clientSeed {\n      user {\n        id\n        activeClientSeed {\n          id\n          seed\n          __typename\n        }\n        activeServerSeed {\n          id\n          nonce\n          seedHash\n          nextSeedHash\n          __typename\n        }\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n"
                };

                CreateOrUseDefaultRestClient();

                var request = CreateDefaultRestRequest(ApiKey);

                request.AddJsonBody(payload);

                var restResponse = await SharedRestClient.ExecuteAsync(request);


                return restResponse;


            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public async Task<ResponseBaseAs<TResult>> ExecuteBet<TResult>(RequestPayload payload)
        {
            try
            {
                CreateOrUseDefaultRestClient();
                var request = CreateDefaultRestRequest(ApiKey);
                request.AddJsonBody(payload);
                var restResponse = SharedRestClient.ExecuteAsync(request).Result;
                return new ResponseAs<TResult>(JsonConvert.DeserializeObject<TResult>(restResponse.Content));
            }
            catch (Exception ex)
            {
                return new ErrorAs<TResult>(ex);
            }

        }

        public async Task<IRestResponse> Execute(RequestPayload payload)
        {
            try
            {
                CreateOrUseDefaultRestClient();
                var request = CreateDefaultRestRequest(ApiKey);
                request.AddJsonBody(payload);
                var restResponse = SharedRestClient.ExecuteAsync(request).Result;
                return restResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public TResult AuthorizeSync<TResult>(RequestPayload payload)
        {
            //try
            // {

            CreateOrUseDefaultRestClient(true);

            var request = CreateDefaultRestRequest(ApiKey, true);

            request.AddJsonBody(payload);

            var restResponse = SharedRestClient.ExecuteAsync(request).Result;

            restResponse.ThowIfRequireCloudflare();

            var r = JsonConvert.DeserializeObject<TResult>(restResponse.Content);

            return r;

            //return new ResponseAs<TResult>(JsonConvert.DeserializeObject<TResult>(restResponse.Content))
            //{
            //    HttpStatus = HttpStatusCode.OK
            //};

            //if (restResponse.StatusCode == HttpStatusCode.Forbidden)
            //{
            //    return new ResponseAs<TResult>(default(TResult))
            //    {
            //        HttpStatus = HttpStatusCode.Forbidden
            //    };
            //}

            //return new ResponseAs<TResult>(JsonConvert.DeserializeObject<TResult>(restResponse.Content))
            //{
            //    HttpStatus = HttpStatusCode.OK
            //};

            // }
            // catch (CloudflareRequiredException ex)
            // {
            //     return new ErrorAs<TResult>(ex);
            // }
            //  catch (Exception ex)
            //  {
            //      return new ErrorAs<TResult>(ex);
            //  }

        }


        //public ResponseBaseAs<TResult> AuthorizeSync<TResult>(RequestPayload payload)
        //{
        //    try
        //    {

        //        CreateOrUseDefaultRestClient(true);

        //        var request = CreateDefaultRestRequest(ApiKey, true);

        //        request.AddJsonBody(payload);

        //        var restResponse = SharedRestClient.ExecuteAsync(request).Result;

        //        if (restResponse.StatusCode == HttpStatusCode.Forbidden)
        //        {
        //            return new ResponseAs<TResult>(default(TResult))
        //            {
        //                HttpStatus = HttpStatusCode.Forbidden
        //            };
        //        }

        //        return new ResponseAs<TResult>(JsonConvert.DeserializeObject<TResult>(restResponse.Content))
        //        {
        //            HttpStatus = HttpStatusCode.OK
        //        };

        //    }
        //    catch (CloudflareRequiredException ex)
        //    {
        //        return new ErrorAs<TResult>(ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ErrorAs<TResult>(ex);
        //    }

        //}

        public async Task<ResponseBaseAs<TResult>> Execute<TResult>(RequestPayload payload)
        {
            try
            {

                CreateOrUseDefaultRestClient();

                var request = CreateDefaultRestRequest(ApiKey);

                request.AddJsonBody(payload);

                var restResponse = SharedRestClient.ExecuteAsync(request).Result;

                if (restResponse.StatusCode == HttpStatusCode.Forbidden)
                {
                    return new ResponseAs<TResult>(default(TResult))
                    {
                        HttpStatus = HttpStatusCode.Forbidden
                    };
                }

                return new ResponseAs<TResult>(JsonConvert.DeserializeObject<TResult>(restResponse.Content))
                {
                    HttpStatus = HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new ErrorAs<TResult>(ex);
            }

        }

    }
}