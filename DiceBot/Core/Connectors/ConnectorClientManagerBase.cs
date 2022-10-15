using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DiceBot.Core.Connectors
{

    public interface ISiteConnector
    {

    }


    public abstract class ConnectorClientManagerBase
    {

        public string GraphqlEndpoint { get; protected set; }
        public string ApiEndpoint { get; protected set; }

        public string HostEndpoint { get; protected set; }

        public string URL { get; protected set; }
        public string Domain { get; protected set; }
        public string ApiKey { get; protected set; }

        public ClientSettings Settings { get; protected set; }

        public CookieContainer Cookies { get; protected set; }

        public string UserAgent { get; protected set; } = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36";


        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void AuthorizationCompleted(AuthorizationCompletedEventArgs e)
        {
            var cookies = new List<Cookie>();

            foreach (var item in e.Cookies)
            {
                if (item.Name.Equals(CookiesHelper.CloudflareCookieName))
                {

                    var ClearanceCookie = new Cookie()
                    {
                        Name = CookiesHelper.CloudflareCookieName,
                        Value = item.Value,
                        Domain = item.Domain,
                        //Path = "/",
                        Path = item.Path ?? "/",
                        Expired = false,
                        Secure = true,
                        Expires = item.Expires.Value,
                        HttpOnly = false
                    };

                    cookies.Add(ClearanceCookie);

                }
            }

            Settings.SetCookies(cookies);
            Settings.SetUserAgent(e.UserAgent);

            UpdateSharedRestClient();

        }


        public virtual void UpdateSharedRestClient()
        {

        }


    }
}
