using Connectors.Stake.Response;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static DiceBot.PDStake;

namespace Connectors.Stake.Response
{

    public class betBase
    {
        public string id { get; set; }
        public string iid { get; set; }

    }

    #region ROULETTE

    public partial class State
    {
        public List<Values> colors { get; set; }
        public List<Values> numbers { get; set; }
        public List<Values> parities { get; set; }
        public List<Values> ranges { get; set; }
        public List<Values> rows { get; set; }

    }

    public partial class Values
    {
        public string value { get; set; }
        public decimal amount { get; set; }
    }

    public class rouletteBet : betBase
    {
        public string id { get; set; }
        public string iid { get; set; }
        public double payoutMultiplier { get; set; }
        public decimal amount { get; set; }
        public decimal payout { get; set; }
        public string updatedAt { get; set; }
        public string currency { get; set; }
        public string game { get; set; }
        public Active user { get; set; }
        public State state { get; set; }
    }

    #endregion

    #region KENO

    public partial class kenoBet : betBase
    {
        public string id { get; set; }
        public string iid { get; set; }
        public double payoutMultiplier { get; set; }
        public decimal amount { get; set; }
        public decimal payout { get; set; }
        public string updatedAt { get; set; }
        public string currency { get; set; }
        public Active user { get; set; }
        public State state { get; set; }
    }

    #endregion

    #region LIMBO

    public partial class limboBet : betBase
    {
        public string id { get; set; }
        public string iid { get; set; }
        public double payoutMultiplier { get; set; }
        public decimal amount { get; set; }
        public decimal payout { get; set; }
        public string updatedAt { get; set; }
        public string currency { get; set; }
        public string game { get; set; }
        public Active user { get; set; }
        public State state { get; set; }
    }

    #endregion


    #region DICE

    public class diceBet : betBase
    {

    }

    #endregion

    public class RequestPayload
    {
        public string operationName { get; set; }

        public string query { get; set; }

        public object variables { get; set; }

        public string identifier { get; set; }

        public RequestPayload()
        {

        }

    }

    public partial class BetQuery
    {
        public string operationName { get; set; }
        public string query { get; set; }
        public BetClass variables { get; set; }
    }

    public partial class BetClass
    {
        public string identifier { get; set; }
        public decimal amount { get; set; }
        public decimal target { get; set; }
        public string currency { get; set; }
        public string game { get; set; }
        public string guess { get; set; }
        public int minesCount { get; set; }
        public List<int> fields { get; set; }
        public string seed { get; set; }
        public string risk { get; set; }
        public List<int> numbers { get; set; }
        public double multiplierTarget { get; set; }

        public string condition { get; set; }
    }

    public partial class Card
    {
        public string rank { get; set; }
        public string suit { get; set; }
    }

    public partial class Data
    {
        public availableBalances availableBalances { get; set; }
        public chatMessages chatMessages { get; set; }
        public crash crash { get; set; }
        public Betdata data { get; set; }
        public List<Errors> errors { get; set; }
    }

    public class Errors
    {
        public List<string> path { get; set; }
        public string message { get; set; }
        public string errorType { get; set; }
        public string data { get; set; }
    }

    public partial class ActiveData
    {
        public User data { get; set; }
        public List<Errors> errors { get; set; }
    }

    public partial class User
    {
        public Active user { get; set; }
    }

    public partial class Active
    {
        public string id { get; set; }
        public string name { get; set; }
        public limboBet activeCasinoBet { get; set; }
        public List<Balances> balances { get; set; }
    }

    public partial class Balances
    {
        public Available available { get; set; }
    }

    public partial class Available
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }

    public partial class Betdata
    {
        public limboBet limboBet { get; set; }
        public kenoBet kenoBet { get; set; }
        public rouletteBet rouletteBet { get; set; }
        public object rotateSeedPair { get; set; }
        public object createVaultDeposit { get; set; }
    }

    public partial class State
    {
        public List<int> drawnNumbers { get; set; }
        public List<int> selectedNumbers { get; set; }
        public double result { get; set; }
        public double multiplierTarget { get; set; }

    }

    public partial class Rounds
    {
        public int field { get; set; }
        public double payoutMultiplier { get; set; }
    }

    public partial class BalancesData
    {
        public User data { get; set; }
        public List<Errors> errors { get; set; }
    }

    public partial class messageData
    {
        public messageData() => this.payload = new messagePayload();

        public string id { get; set; }

        public string type { get; set; }

        public messagePayload payload { get; set; }
    }
    public partial class messageErrors
    {
        public messageErrors() => this.message = (string)null;

        public string[] path { get; set; }

        public string message { get; set; }
    }

    public partial class ChatInputs
    {
        public ChatInputs()
        {

        }
    }

    public partial class context
    {
        public string url { get; set; }
    }

    public partial class chatMessages
    {
        public string id { get; set; }
        public ChatData data { get; set; }
        public ChatUser user { get; set; }
    }

    public partial class ChatData
    {
        public string message { get; set; }
    }

    public partial class ChatUser
    {
        public string name { get; set; }
    }

    public partial class availableBalances
    {
        public decimal amount { get; set; }
        public Balance balance { get; set; }
    }

    public partial class Balance
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }

    public partial class crash
    {
        public Event @event { get; set; }
    }

    public partial class Event
    {
        public string id { get; set; }
        public string status { get; set; }
        public object multiplier { get; set; }
        public string startTime { get; set; }
        public object nextRoundIn { get; set; }
        public int elapsed { get; set; }
        public string timestamp { get; set; }
    }

    public class messagePayload
    {
        public messagePayload()
        {
            ///this.variables = new object();
            //this.extensions = new object();
            this.data = new Data();
            this.errors = new List<messageErrors>();
        }

        public string accessToken { get; set; }

        public string operationName { get; set; }

        public string key { get; set; }

        public string query { get; set; }

        public string language { get; set; }

        public string lockdownToken { get; set; }

        public BetClass variables { get; set; }

        public string requestPolicy { get; set; }

        public bool preferGetMethod { get; set; }

        public bool suspense { get; set; }

        public context context { get; set; }

        public Data data { get; set; }

        public List<messageErrors> errors { get; set; }
    }


}

namespace Connectors.Stake
{

    public class ClientSettings
    {

        public enum Connection
        {
            DEFAULT,
            WSS
        }

        public Connection Mode { get; set; } = ClientSettings.Connection.DEFAULT;
        public string Site { get; set; }
        public string ApiKey { get; set; }
        public string ApiEndPoint { get; set; }
        public string WebSocketEndPoint { get; set; }
        public string GraphQLEndPoint { get; set; }
        public string Referrer { get; set; }
        public int Timeout { get; set; } = 1000;
        public string UserAgent { get; private set; } = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36";

        public void Update(string site = "", string apiKey = "")
        {

            if (!string.IsNullOrEmpty(site))
            {
                //this.Site = site;
                //this.Referrer = $"https://api.{site}";
                //this.ApiEndPoint = $"https://api.{site}";
                //this.WebSocketEndPoint = $"wss://api.{site}/graphql";
                //this.GraphQLEndPoint = $"https://api.{site}/graphql";

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

        public ClientSettings()
        {
        }

        public ClientSettings(string site, string apiKey)
        {
            Update(site, apiKey);
        }

    }
    public class APIClientManager
    {

        public ClientSettings Settinrg { get; private set; }
        public CookieContainer cc { get; private set; }
        public RestClient SharedRestClient { get; private set; }

        public string ApiUrl { get; private set; }
        public string URL { get; set; }
        public string Domain { get; set; }
        public string ApiKey { get; set; }

        public APIClientManager()
        {

        }

        public APIClientManager(string site, string apikey)
        {
            Domain = site;
            ApiKey = apikey;
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void CreateOrUseDefaultRestClient(bool dispose = false)
        {

            if (dispose == true)
            {
                SharedRestClient = null;
                cc = null;
            }

            if (SharedRestClient != null)
            {
                return;
            }

            //ApiUrl = "https://api." + Domain + "/graphql";
            ApiUrl = "https://" + Domain + "/_api/graphql";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            this.cc = new CookieContainer();

            //var options = new RestClientOptions()
            //{
            //    BaseUrl = new Uri(ApiUrl),
            //    CookieContainer = this.cc,
            //    UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36"
            //};

            //SharedRestClient = new RestClient(options);

            SharedRestClient = new RestClient(new Uri(ApiUrl));
            SharedRestClient.CookieContainer = this.cc;
            SharedRestClient.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36";

        }

        private RestRequest CreateDefaultRestRequest(string apiKey)
        {
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("authorization", string.Format("Bearer {0}", apiKey));
            restRequest.AddHeader("x-access-token", apiKey);
            restRequest.AddHeader("Content-type", "application/json");
            return restRequest;
        }

        public async Task<IRestResponse> Authorize()
        {

            try
            {

                var payload = new BetQuery
                {
                    operationName = "initialUserRequest",
                    variables = new BetClass() { },
                    query = "query initialUserRequest {\n  user {\n    ...UserAuth\n    __typename\n  }\n}\n\nfragment UserAuth on User {\n  id\n  name\n  email\n  hasPhoneNumberVerified\n  hasEmailVerified\n  hasPassword\n  intercomHash\n  createdAt\n  hasTfaEnabled\n  mixpanelId\n  hasOauth\n  isKycBasicRequired\n  isKycExtendedRequired\n  isKycFullRequired\n  kycBasic {\n    id\n    status\n    __typename\n  }\n  kycExtended {\n    id\n    status\n    __typename\n  }\n  kycFull {\n    id\n    status\n    __typename\n  }\n  flags {\n    flag\n    __typename\n  }\n  roles {\n    name\n    __typename\n  }\n  balances {\n    ...UserBalanceFragment\n    __typename\n  }\n  activeClientSeed {\n    id\n    seed\n    __typename\n  }\n  previousServerSeed {\n    id\n    seed\n    __typename\n  }\n  activeServerSeed {\n    id\n    seedHash\n    nextSeedHash\n    nonce\n    blocked\n    __typename\n  }\n  __typename\n}\n\nfragment UserBalanceFragment on UserBalance {\n  available {\n    amount\n    currency\n    __typename\n  }\n  vault {\n    amount\n    currency\n    __typename\n  }\n  __typename\n}\n"
                };

                CreateOrUseDefaultRestClient();

                var request = CreateDefaultRestRequest(ApiKey);

                request.AddJsonBody(payload);

                var restResponse = SharedRestClient.ExecuteAsync(request).Result;

                return restResponse;

            }
            catch (Exception ex)
            {
                //luaPrint(ex.Message);
                throw new Exception(ex.Message, ex);
            }

        }

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
                //luaPrint(ex.Message);

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
                //luaPrint(ex.Message);
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
                //luaPrint(ex.Message);
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
                //luaPrint(ex.Message);
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
                //luaPrint(ex.Message);
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
                //luaPrint(ex.Message);
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
                //luaPrint(ex.Message);
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
                //luaPrint(ex.Message);
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


        public async Task<ResponseBaseAs<TResult>> Execute<TResult>(RequestPayload payload)
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

        /*
        public async Task<IRestResponse> Execute(BetQuery payload)
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
                //luaPrint(ex.Message);
                return null;
            }

        }
        */
    }
}