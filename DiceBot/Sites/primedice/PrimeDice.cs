
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using System.Net.Http;

//using GraphQL.Client.Http;
using System.Globalization;
using DiceBot.Core;

using GraphQL.Client.Serializer.Newtonsoft;

using static DiceBot.PrimediceSchema;
using GraphQL.Client.Http;
using GraphQL;
using GraphQL.Client.Abstractions;
using Newtonsoft.Json;
using Connectors.Primedice;
using DiceBot.Core.Request;
using Dicebot.Core;
using DiceBot.Core.Connectors;

namespace DiceBot.Schema.BetKing
{


}
namespace DiceBot
{
    public class SiteSchemaBase
    {

    }

    public class PDStake : PrimediceSchema
    {

        public class ResponseBaseAs<TResult>
        {
            public TResult Result { get; set; }
            public Exception Error { get; set; }
            public HttpStatusCode HttpStatus { get; set; }

        }

        public class ResponseAs<TResult> : ResponseBaseAs<TResult>
        {
            public ResponseAs(TResult result)
            {
                Result = result;
            }
        }

        public class ErrorAs<TResult> : ResponseBaseAs<TResult>
        {
            public ErrorAs(Exception error)
            {
                base.Error = error;
            }
        }

        public class Errors
        {
            public List<string> path { get; set; }
            public string message { get; set; }
            public string errorType { get; set; }
            public string data { get; set; }
        }

        public class GenericResponse
        {
            [JsonProperty("data")]
            public GenericData Data { get; set; }

            [JsonProperty("errors")]
            public List<Errors> Errors { get; set; }
        }

        public class GenericData
        {
            [JsonProperty("user")]
            public pdUser User { get; set; }

            [JsonProperty("diceRoll")]
            public RollDice DiceBetResult { get; set; }

            [JsonProperty("primediceRoll")]
            public RollDice PrimediceRoll { get; set; }

            [JsonProperty("bet")]
            public RollDice Bet { get; set; }


        }

    }

    public class PrimediceSchema : SiteSchemaBase
    {

        public class Root<T>
        {
            public T Data { get; set; }
        }


        public class UserResponse
        {
            [JsonProperty("user")]
            public pdUser User { get; set; }
        }
        public class RollDiceResponse
        {
            [JsonProperty("primediceRoll")]
            public RollDice Roll { get; set; }
        }
        public class SeedResponse
        {
            [JsonProperty("pdSeed")]
            public pdSeed Seed { get; set; }
        }

        public class Sender
        {
            public string name { get; set; }
            public string __typename { get; set; }
        }

        public class Receiver
        {
            public string name { get; set; }
            public string __typename { get; set; }
        }

        public class _Tip
        {
            public string id { get; set; }
            public double amount { get; set; }
            public string currency { get; set; }
            public Sender sender { get; set; }
            public Receiver receiver { get; set; }
            public string __typename { get; set; }
        }
        public class Data2
        {
            public string message { get; set; }
            public _Tip tip { get; set; }
            public string __typename { get; set; }
        }
        public class pdSeed
        {
            public string seedHash { get; set; }
            public string seed { get; set; }
            public int nonce { get; set; }
        }
        public class pdUser
        {
            public string id { get; set; }
            public string name { get; set; }
            public List<object> roles { get; set; }
            public string __typename { get; set; }
            public Balance balance { get; set; }

            [JsonProperty("balances")]
            public Balance[] Balances { get; set; }

            [JsonProperty("statistic")]
            public List<Statistic> Statistics { get; set; }
            public pdSeed activeServerSeed { get; set; }
            public pdSeed activeClientSeed { get; set; }
        }

        public class ChatMessages
        {
            public string id { get; set; }
            public Data2 data { get; set; }
            public string createdAt { get; set; }
            public pdUser user { get; set; }
            public string __typename { get; set; }
        }
        public class Chat
        {
            public string id { get; set; }
            public string __typename { get; set; }
        }
        public class Messages
        {
            public Chat chat { get; set; }
            public string id { get; set; }
            public Data2 data { get; set; }
            public string createdAt { get; set; }
            public pdUser user { get; set; }
            public string __typename { get; set; }
        }
        public class DiceState
        {
            public double result { get; set; }
            public double target { get; set; }
            public string condition { get; set; }

        }
        public class RollDice
        {
            public string id { get; set; }
            public string iid { get; set; }
            public decimal payoutMultiplier { get; set; }
            public double amount { get; set; }
            public double payout { get; set; }
            public string createdAt { get; set; }
            public string currency { get; set; }
            public DiceState state { get; set; }

            [JsonProperty("user")]
            public pdUser User { get; set; }
            public string __typename { get; set; }
            public pdSeed serverSeed { get; set; }
            public pdSeed clientSeed { get; set; }
            public int nonce { get; set; }
            public Bet ToBet(decimal maxroll)
            {
                Bet bet = new Bet
                {
                    Amount = (decimal)amount,
                    Chance = state.condition.ToLower() == "above" ? maxroll - (decimal)state.target : (decimal)state.target,
                    high = state.condition.ToLower() == "above",
                    Currency = currency,
                    date = DateTime.Now,
                    Id = id,
                    Roll = (decimal)state.result,
                    UserName = User.name,
                    clientseed = clientSeed.seed,
                    serverhash = serverSeed.seedHash,
                    nonce = nonce
                };
                bool win = (((bool)bet.high ? (decimal)bet.Roll > maxroll - (decimal)(bet.Chance) : (decimal)bet.Roll < (decimal)(bet.Chance)));
                bet.Profit = win ? ((decimal)(payout - amount)) : ((decimal)-amount);
                return bet;
            }
        }
        public class Statistic
        {
            public string game { get; set; }
            public decimal bets { get; set; }
            public decimal wins { get; set; }
            public decimal losses { get; set; }
            public double betAmount { get; set; }
            public double? profit { get; set; }
            public string currency { get; set; }
            public string __typename { get; set; }
        }

        public class Data
        {
            public ChatMessages chatMessages { get; set; }
            public Messages messages { get; set; }
            public RollDice rollDice { get; set; }
            public pdUser user { get; set; }
            public RollDice bet { get; set; }
        }

        public class Payload
        {
            public Data data { get; set; }
        }

        public class RootObject
        {
            public string type { get; set; }
            public string id { get; set; }
            public Payload payload { get; set; }
        }
        public class Role
        {
            public string name { get; set; }
            public string __typename { get; set; }
        }
        public class Balance
        {
            public Available available { get; set; }
            public string __typename { get; set; }
        }
        public class Available
        {
            public double amount { get; set; }
            public string currency { get; set; }
            public string __typename { get; set; }
        }
    }

    public class PrimeDice : DiceSite
    {
        //protected string URL = "https://api.primedice.com/graphql";
        protected string URL = "https://primedice.com/_api/graphql";
        //protected string RolName = "primediceRoll";
        //protected string GameName = "CasinoGamePrimedice";
        //protected string EnumName = "CasinoGamePrimediceConditionEnum";
        protected string StatGameName = "primedice";

        private static string[] _sCurrencies = new string[]
        {
            "BTC",
            "ETH",
            "LTC",
            "DOGE",
            "BCH",
            "XRP",
            "TRX",
            "EOS",
            "BNB",
            "USDT",
            "APE",
            "BUSD",
            "CRO",
            "DAI",
            "LINK",
            "SAND",
            "SHIB",
            "UNI",
            "USDC"
        };


        public static string[] sCurrencies => _sCurrencies.Select(x => x.ToUpperInvariant()).OrderBy(x => x).ToArray();



        string accesstoken = "";

        DateTime LastSeedReset = new DateTime();

        public bool ispd = false;

        string username = "";

        long uid = 0;

        DateTime lastupdate = new DateTime();

        bool getid = false;

        string userid = "";

        int retrycount = 0;

        DateTime Lastbet = DateTime.Now;

        DBRandom R = new DBRandom();


        protected ClientSettings clientSettings;
        protected PrimeDiceConnector APIConnector;

        AuthorizeBrowser authorizeBrowser;

        public bool SafeSpeed { get; set; } = true;

        public PrimeDice(cDiceBot Parent)
        {
            _PasswordText = "API Key: ";
            maxRoll = 99.99m;

            AutoInvest = false;
            AutoWithdraw = true;
            ChangeSeed = true;
            AutoLogin = true;

            BetURL = "https://api.primedice.com/bets/";

            this.Currencies = sCurrencies;

            this.Currency = "Btc";

            this.Parent = Parent;

            Name = "PrimeDice";

            this.Tip = true;

            this.Vault = true;

            TipUsingName = true;

            SiteURL = "https://primedice.com";

            SiteDomain = "primedice.com";

            if (File.Exists("slow") || File.Exists("slow.txt"))
            {
                getid = true;
            }

            clientSettings = new ClientSettings(SiteDomain);

        }

        public override void UpdateMirror(string url)
        {
            if (url != "" && MirrorList.Contains(url))
            {
                BetURL = $"https://api.{url}/bets/";
                SiteURL = $"https://{url}/";
                //URL = $"https://api.{url}/graphql";
                URL = $"https://{url}/_api/graphql";
                //base.UpdateMirror(url);
            }
        }

        protected override void CurrencyChanged()
        {
            ForceUpdateStats = true;
        }

        public override bool Register(string Username, string Password)
        {
            return false;
        }


        private void InitAuthorizeProcess(string url)
        {

            // ------------------------------------------------------
            // The connection require manual authorization
            // ------------------------------------------------------

            // Open AuthorizeBrowser window
            // the user do the validation
            // after validation, he press continue
            // we pick the cookies and save it for future requests

            // ------------------------------------------------------

            authorizeBrowser = new AuthorizeBrowser
            {
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen,
                TargetUrl = url
            };

            authorizeBrowser.Show(this.Parent);
            authorizeBrowser.FormClosed += (s, e) =>
            {
                authorizeBrowser.Dispose();
            };

        }

        public override void AuthorizationCompleted(AuthorizationCompletedEventArgs e)
        {
            APIConnector.AuthorizationCompleted(e);
            Login("", APIConnector.ApiKey, "");
        }

        public override void Login(string Username, string Password, string otp)
        {
            try
            {

                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                clientSettings.Update(clientSettings.Site, Password);

                APIConnector = APIConnector ?? new PrimeDiceConnector(clientSettings);

                var req = new RequestPayload()
                {
                    operationName = "DiceBotLogin",
                    query = "query DiceBotLogin{user {activeServerSeed { seedHash seed nonce} activeClientSeed{seed} id balances{available{currency amount}} statistic {game bets wins losses betAmount profit currency}}}"
                };

                var auth = APIConnector.ExecuteSync(req);

                auth.ThowIfRequireCloudflare();

                var response = JsonConvert.DeserializeObject<PDStake.GenericResponse>(auth.Content);

                pdUser user = response.Data.User;

                userid = user.id;

                if (string.IsNullOrWhiteSpace(userid))
                {
                    finishedlogin(false);
                }
                else
                {

                    foreach (Statistic x in user.Statistics)
                    {
                        if (x.currency.ToLower() == Currency.ToLower() && x.game == StatGameName)
                        {
                            this.bets = (int)x.bets;
                            this.wins = (int)x.wins;
                            this.losses = (int)x.losses;
                            this.profit = x.profit.HasValue ? (decimal)x.profit.Value : 0;
                            this.wagered = (decimal)x.betAmount;
                            break;
                        }
                    }
                    foreach (Balance x in user.Balances)
                    {
                        if (x.available.currency.ToLower() == Currency.ToLower())
                        {
                            balance = (decimal)x.available.amount;
                            break;
                        }
                    }

                    finishedlogin(true);
                    ispd = true;

                    Thread t = new Thread(GetBalanceThread);
                    t.Start();

                    return;
                }

            }
            catch (CloudflareRequiredException ex)
            {
                InitAuthorizeProcess(clientSettings.ApiEndPoint);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    InitAuthorizeProcess(clientSettings.ApiEndPoint);
                    return;
                }
                finishedlogin(false);
            }
            catch (Exception ex)
            {
                Parent.DumpLog(ex.ToString(), -1);
                finishedlogin(false);
            }
        }

        void GetBalanceThread()
        {
            try
            {
                while (ispd)
                {
                    if (userid != null && ((DateTime.Now - lastupdate).TotalSeconds >= 30 || ForceUpdateStats))
                    {

                        ForceUpdateStats = false;
                        lastupdate = DateTime.Now;

                        var req = new RequestPayload
                        {
                            operationName = "DiceBotGetBalance",
                            query = "query DiceBotGetBalance{user {activeServerSeed { seedHash seed nonce} activeClientSeed{seed} id balances{available{currency amount}} statistic {game bets wins losses betAmount profit currency}}}"
                        };

                        var connectorResponse = APIConnector.ExecuteSync(req);
                        var response = JsonConvert.DeserializeObject<PDStake.GenericResponse>(connectorResponse.Content);

                        pdUser user = response.Data.User;

                        foreach (Statistic x in user.Statistics)
                        {
                            if (x.currency.ToLower() == Currency.ToLower() && x.game == StatGameName)
                            {
                                this.bets = (int)x.bets;
                                this.wins = (int)x.wins;
                                this.losses = (int)x.losses;
                                this.profit = x.profit.HasValue ? (decimal)x.profit.Value : 0;
                                this.wagered = (decimal)x.betAmount;
                                break;
                            }
                        }

                        foreach (Balance x in user.Balances)
                        {
                            if (x.available.currency.ToLower() == Currency.ToLower())
                            {
                                balance = (decimal)x.available.amount;
                                break;
                            }
                        }

                        Parent.updateBalance(balance);
                        Parent.updateWagered(wagered);
                        Parent.updateProfit(profit);
                        Parent.updateBets(bets);
                        Parent.updateWins(wins);
                        Parent.updateLosses(losses);

                    }
                    Thread.Sleep(ThreadBalanceStep);
                }
            }
            catch (Exception e)
            {
                Parent.DumpLog(e.ToString(), -1);
            }
        }

        void placebetthread(object bet)
        {

            try
            {

                if (SafeSpeed)
                {
                    Thread.Sleep(ThreadBetStep);
                }

                PlaceBetObj tmp5 = bet as PlaceBetObj;
                decimal amount = tmp5.Amount;
                decimal chance = tmp5.Chance;
                bool High = tmp5.High;

                decimal tmpchance = High ? maxRoll - chance : chance;

                var req = new RequestPayload()
                {
                    query = @"mutation DiceBotDiceBet($amount: Float! 
            $target: Float!
            $condition: CasinoGamePrimediceConditionEnum!
            $currency: CurrencyEnum!
            $identifier: String!){ primediceRoll(amount: $amount, target: $target,condition: $condition,currency: $currency, identifier: $identifier)" +
  " { id nonce currency amount payout state { ... on CasinoGamePrimedice { result target condition } } createdAt serverSeed{seedHash seed nonce} clientSeed{seed} user{balances{available{amount currency}} statistic{game bets wins losses betAmount profit currency}}}}",
                    variables = new
                    {
                        amount = amount,
                        target = tmpchance,
                        condition = (High ? "above" : "below"),
                        currency = Currency.ToLower(),
                        identifier = Utils.RandomString(16)
                    }
                };

                var connectorResponse = APIConnector.ExecuteSync(req);
                var response = JsonConvert.DeserializeObject<PDStake.GenericResponse>(connectorResponse.Content);

                if (response.Errors != null)
                {
                    if (response.Errors.Count > 0)
                    {
                        Parent.updateStatus(response.Errors[0].message);
                    }
                }

                if (response.Data != null)
                {

                    RollDice tmp = response.Data.PrimediceRoll;

                    Lastbet = DateTime.Now;
                    try
                    {

                        lastupdate = DateTime.Now;

                        foreach (Statistic x in tmp.User.Statistics)
                        {
                            if (x.currency.ToLower() == Currency.ToLower() && x.game == StatGameName)
                            {
                                this.bets = (int)x.bets;
                                this.wins = (int)x.wins;
                                this.losses = (int)x.losses;
                                this.profit = x.profit.HasValue ? (decimal)x.profit.Value : 0;
                                this.wagered = (decimal)x.betAmount;
                                break;
                            }
                        }

                        foreach (Balance x in tmp.User.Balances)
                        {
                            if (x.available.currency.ToLower() == Currency.ToLower())
                            {
                                balance = (decimal)x.available.amount;
                                break;
                            }
                        }

                        Bet tmpbet = tmp.ToBet(maxRoll);

                        if (getid)
                        {

                            string betid = tmpbet.Id;

                            var IdRequest = new RequestPayload
                            {
                                query = " query DiceBotGetBetId($betId: String!){bet(betId: $betId){iid}}",
                                variables = new
                                {
                                    betId = betid
                                }
                            };

                            var IdRequestResult = APIConnector.ExecuteSync<RollDice>(IdRequest);

                            if (IdRequestResult.iid != null)
                            {
                                tmpbet.Id = IdRequestResult.iid;
                                if (tmpbet.Id.Contains("house:"))
                                {
                                    tmpbet.Id = tmpbet.Id.Substring("house:".Length);
                                }
                            }

                        }

                        tmpbet.Guid = tmp5.Guid;

                        FinishedBet(tmpbet);

                        retrycount = 0;

                    }
                    catch (Exception e)
                    {
                        Parent.DumpLog(e.ToString(), -1);
                        Parent.updateStatus("Some kind of error happened. I don't really know graphql, so your guess as to what went wrong is as good as mine.");
                    }
                }
            }
            catch (AggregateException e)
            {
                if (retrycount++ < 3)
                {
                    Thread.Sleep(1000);
                    placebetthread(new PlaceBetObj(High, amount, chance, (bet as PlaceBetObj).Guid));
                    return;
                }
                if (e.InnerException.Message.Contains("429") || e.InnerException.Message.Contains("502"))
                {
                    Thread.Sleep(500);
                    placebetthread(new PlaceBetObj(High, amount, chance, (bet as PlaceBetObj).Guid));
                }
            }
            catch (Exception e2)
            {
                Parent.updateStatus("Error occured while trying to bet, retrying in 30 seconds. Probably.");
                Parent.DumpLog(e2.ToString(), -1);
            }

        }

        protected override void internalPlaceBet(bool High, decimal amount, decimal chance, string Guid)
        {
            this.High = High;
            new Thread(new ParameterizedThreadStart(placebetthread)).Start(new PlaceBetObj(High, amount, chance, Guid));
        }


        public override void ResetSeed(string customClientSeed = "")
        {
            try
            {

                var req = new RequestPayload
                {
                    operationName = "DiceBotRotateSeed",
                    query = "mutation DiceBotRotateSeed ($seed: String!){rotateServerSeed{ seed seedHash nonce } changeClientSeed(seed: $seed){seed}}",
                    variables = new
                    {
                        seed = CustomSeed.IsCustom ? CustomSeed.Value : R.Next(0, int.MaxValue).ToString()
                    }
                };

                var connectorResponse = APIConnector.ExecuteSync(req);
                var response = JsonConvert.DeserializeObject<PDStake.GenericResponse>(connectorResponse.Content);

                if (response.Data != null)
                {
                    // Todo:??
                }
                else if (response.Errors != null && response.Errors.Count > 0)
                {
                    foreach (var x in response.Errors)
                    {
                        Parent.DumpLog("GRAPHQL ERROR PD RESETSEED: " + x.message, 1);
                    }
                }

            }
            catch (Exception e)
            {
                Parent.updateStatus("Failed to reset seed.");
            }
        }

        public override void SetClientSeed(string Seed)
        {
            throw new NotImplementedException();
        }

        public override bool ReadyToBet()
        {
            //if ((amount * 100000000m)<=100000 && (DateTime.Now - Lastbet).TotalMilliseconds < 350)
            // return false;
            //else
            //    return true;
            return true;
        }

        protected override bool internalWithdraw(decimal Amount, string Address)
        {
            try
            {

                var req = new RequestPayload()
                {
                    operationName = "createWithdrawal",
                    query = "mutation DiceBotWithdrawal{createWithdrawal(currency:" + Currency.ToLower() + ", address:\"" + Address + "\",amount:" + amount.ToString("0.00000000", System.Globalization.NumberFormatInfo.InvariantInfo) + "){id name address hash amount walletFee createdAt status currency}}"
                };

                var connectorResponse = APIConnector.ExecuteSync(req);
                return connectorResponse.StatusCode == HttpStatusCode.OK;

            }
            catch
            {
                return false;
            }
        }

        public override bool InternalSendToVault(decimal amount)
        {
            try
            {

                var req = new RequestPayload
                {
                    query = "mutation CreateVaultDeposit($currency: CurrencyEnum!, $amount: Float!) {\n  createVaultDeposit(currency: $currency, amount: $amount) {\n    id\n    amount\n    currency\n    user {\n      id\n      balances {\n        available {\n          amount\n          currency\n          __typename\n        }\n        vault {\n          amount\n          currency\n          __typename\n        }\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n",
                    variables = new
                    {
                        currency = Currency.ToLower(),
                        amount = amount
                    }
                };

                var connectorResponse = APIConnector.ExecuteSync(req);

                //return connectorResponse.Data["createVaultDeposit"].id.Value != null;
                return connectorResponse.StatusCode == HttpStatusCode.OK;

            }
            catch (Exception e)
            {

            }
            return false;
        }

        public override decimal GetLucky(string server, string client, int nonce)
        {
            return sGetLucky(server, client, nonce);
        }

        new public static decimal sGetLucky(string server, string client, long nonce)
        {
            HMACSHA512 betgenerator = new HMACSHA512();

            int charstouse = 5;
            List<byte> serverb = new List<byte>();

            for (int i = 0; i < server.Length; i++)
            {
                serverb.Add(Convert.ToByte(server[i]));
            }

            betgenerator.Key = serverb.ToArray();

            List<byte> buffer = new List<byte>();
            string msg = client + "-" + nonce.ToString();
            foreach (char c in msg)
            {
                buffer.Add(Convert.ToByte(c));
            }

            byte[] hash = betgenerator.ComputeHash(buffer.ToArray());

            StringBuilder hex = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
                hex.AppendFormat("{0:x2}", b);


            for (int i = 0; i < hex.Length; i += charstouse)
            {

                string s = hex.ToString().Substring(i, charstouse);

                decimal lucky = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
                if (lucky < 1000000)
                {
                    lucky %= 10000;
                    return lucky / 100;

                }
            }
            return 0;
        }



        public override void Disconnect()
        {
            ispd = false;
            if (accesstoken != "")
            {
                try
                {
                    APIConnector = null;
                    //string sEmitResponse = Client.GetStringAsync("logout?api_key=" + accesstoken).Result;
                    accesstoken = "";
                }
                catch
                {

                }
            }
        }

        public override void Donate(decimal Amount)
        {
            SendTip("WinMachine", Amount);
        }

        public override bool InternalSendTip(string user, decimal amount)
        {
            try
            {

                string userid = GetUid(user);

                string chatid = "";
                string query = "mutation SendTip($userId: String!, $amount: Float!, $currency: CurrencyEnum!, $isPublic: Boolean, $chatId: String!, $tfaToken: String) {sendTip(userId: $userId, amount: $amount, currency: $currency, isPublic: $isPublic, chatId: $chatId, tfaToken: $tfaToken) { id amount currency user { id name __typename } sendBy { id name balances { available { amount currency __typename } vault { amount currency __typename } __typename } __typename } __typename } }";

                var req = new RequestPayload
                {
                    query = query,
                    variables = new
                    {
                        name = user,
                        amount = amount,
                        isPublic = true,
                        userId = userid,
                        chatId = chatid,
                        currency = Currency.ToLower(),
                        tfaToken = (string)null
                    }
                };

                var connectorResponse = APIConnector.ExecuteSync(req);
                var response = JsonConvert.DeserializeObject<dynamic>(connectorResponse.Content);

                return response.Data.sendTip.id.Value != null;

            }
            catch (Exception e)
            {
                Parent.updateStatus("Error occured while trying to send tip.");
                return false;

            }

        }

        public string GetUid(string username)
        {
            try
            {
                string userid = "";

                var req = new RequestPayload
                {
                    operationName = "DiceBotGetUid",
                    query = "query DiceBotGetUid($username: String!){ user(name: $username){id}}",
                    variables = new
                    {
                        username = username
                    }
                };

                var connectorResponse = APIConnector.ExecuteSync(req);
                var response = JsonConvert.DeserializeObject<dynamic>(connectorResponse.Content);

                userid = response.data.user.id.Value;

                return userid;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }




        public override void SendChatMessage(string Message)
        {
            Thread send = new Thread(new ParameterizedThreadStart(Send));
            send.Start(Message);
        }

        void Send(object _Message)
        {
            //if (accesstoken != "")
            //{
            //    try
            //    {
            //        string Message = (string)_Message;
            //        List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            //        pairs.Add(new KeyValuePair<string, string>("username", username));
            //        pairs.Add(new KeyValuePair<string, string>("userid", uid.ToString()));
            //        pairs.Add(new KeyValuePair<string, string>("room", "English"));
            //        pairs.Add(new KeyValuePair<string, string>("message", Message));
            //        pairs.Add(new KeyValuePair<string, string>("token", accesstoken));

            //        FormUrlEncodedContent Content = new FormUrlEncodedContent(pairs);
            //        string sEmitResponse = Client.PostAsync("send?api_key=" + accesstoken, Content).Result.Content.ReadAsStringAsync().Result;

            //    }
            //    catch
            //    {

            //    }
            //}
        }

        public override void GetSeed(long BetID)
        {
            throw new NotImplementedException();
        }

    }
}
