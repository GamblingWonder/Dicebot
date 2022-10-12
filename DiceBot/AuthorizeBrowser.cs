using CefSharp;
using DiceBot.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceBot
{

    public partial class AuthorizeBrowser : Form
    {
        public string TargetUrl { get; set; }

        public AuthorizeBrowser()
        {
            InitializeComponent();

            this.Load += AuthorizeBrowser_Load;
        }

        private void AuthorizeBrowser_Load(object sender, EventArgs e)
        {
            urlPath.Text = TargetUrl;

            goBtn_Click(this, null);
        }

        private async void button4_Click(object sender, EventArgs e)
        {

            var getUserAgentScript = @"(function () { return navigator.userAgent;})();";

            var cookies = chromiumWebBrowser1.GetCookieManager().VisitAllCookiesAsync().Result;

            var agent = await chromiumWebBrowser1.GetMainFrame().EvaluateScriptAsync(getUserAgentScript).ContinueWith(t =>
            {
                return t.Result.Result.ToString();
            });

            var args = new AuthorizationCompletedEventArgs()
            {
                UserAgent = agent,
                Cookies = cookies
            };

            (this.Owner as IAuthorizationHub).OnAuthorizationCompleted(this, args);

            this.Close();

        }

        private void reloadBtn_Click(object sender, EventArgs e)
        {
           // chromiumWebBrowser1.GetBrowser().ShowDevTools();
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.LoadUrl(urlPath.Text);

            chromiumWebBrowser1.ConsoleMessage += ChromiumWebBrowser1_ConsoleMessage;
            chromiumWebBrowser1.FrameLoadEnd += ChromiumWebBrowser1_FrameLoadEnd;
            chromiumWebBrowser1.LoadingStateChanged += ChromiumWebBrowser1_LoadingStateChanged;

        }
        private void ChromiumWebBrowser1_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            Debug.WriteLine(e.IsLoading);
        }

        private void ChromiumWebBrowser1_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
        }

        private void ChromiumWebBrowser1_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            Debug.WriteLine(e.Message);
        }
        private void cookiesBtn_Click(object sender, EventArgs e)
        {
            var ddd = chromiumWebBrowser1.GetCookieManager().VisitAllCookiesAsync().Result;
            var d = Cef.GetGlobalCookieManager().VisitAllCookiesAsync().Result;
            string cookie = "";
            foreach (var item in ddd)
            {
                cookie += item.Name + "=" + item.Value + ";";
            }
        }
    }
}
