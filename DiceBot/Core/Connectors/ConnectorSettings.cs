using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DiceBot.Core.Connectors
{

    public class ConnectorLoginData
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TwoFA { get; set; }
    }

    public class ConnectorSettings
    {
        public enum Connection
        {
            DEFAULT,
            WSS
        }

        public Connection Mode { get; set; } = ConnectorSettings.Connection.DEFAULT;

        public List<Cookie> PersistentCookies { get; set; }

        public string Site { get; set; }
        public string ApiKey { get; set; }

        public int Timeout { get; set; } = 1000;

        public string Referrer { get; set; }

        public string UserAgent { get; set; } = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36";


        public List<KeyValuePair<string, object>> CustomPairs { get; set; }

        public ConnectorLoginData LoginData { get; set; }

        //KeyValuePair<string, string>

        public ConnectorSettings()
        {
        }

        public ConnectorSettings(string site, string apiKey) : this()
        {
            Update(site, apiKey);
        }

        public void AddLoginData(string username, string email, string password, string twofa = "")
        {

            if (LoginData == null)
            {
                LoginData = new ConnectorLoginData();
            }

            LoginData.Username = username;
            LoginData.Email = email;
            LoginData.Password = password;
            LoginData.TwoFA = twofa;

        }

        public void AddCustomPair(string key, string value)
        {
            if (CustomPairs == null)
            {
                CustomPairs = new List<KeyValuePair<string, object>>();
            }
            CustomPairs.Add(new KeyValuePair<string, object>(key, value));
        }

        public object TryGetPairValue(string key)
        {
            if (CustomPairs == null)
            {
                return "";
            }
            return CustomPairs.FirstOrDefault(x => x.Key == key).Value;
        }

        public void SetCookies(List<Cookie> cookies)
        {
            PersistentCookies = cookies;
        }

        public void AddCookie(Cookie cookie)
        {
            if (PersistentCookies == null)
            {
                PersistentCookies = new List<Cookie>();
            }
            PersistentCookies.Add(cookie);
        }

        public CookieContainer GetCookieContainer()
        {
            CookieContainer Cookies = new CookieContainer();
            if (PersistentCookies != null)
            {
                foreach (var item in PersistentCookies)
                {
                    Cookies.Add(item);
                }
            }
            return Cookies;
        }

        public string GetCookieValue(string name)
        {

            string cookieValue = "";

            if (PersistentCookies != null)
            {
                foreach (var item in PersistentCookies)
                {
                    if (item.Name.Equals(name))
                    {
                        cookieValue = item.Value;
                        break;
                    }
                }
            }

            return cookieValue;
        }

        public void SetUserAgent(string userAgent)
        {
            UserAgent = userAgent;
        }

        public virtual void Update(string site = "", string apiKey = "")
        {
            if (!string.IsNullOrEmpty(site))
            {
                this.Site = site;
            }
            if (!string.IsNullOrEmpty(apiKey))
            {
                this.ApiKey = apiKey;
            }
        }

    }
}