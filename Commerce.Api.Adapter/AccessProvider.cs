﻿using Nop.Api.Adapter.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Nop.Api.Adapter
{
    public class AccessProvider
    {
        public static string Token = "";
        public static string ClientId = WebConfigurationManager.AppSettings["ClientId"];
        public static string ClientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
        public static string ServerUrl = WebConfigurationManager.AppSettings["ServerUrl"];
        public static string RedirectUrl = WebConfigurationManager.AppSettings["RedirectUrl"];

        public void GetAccessToken()
        {
            var nopAuthorizationManager = new AuthorizationManager();

            string authUrl = nopAuthorizationManager.BuildAuthUrl(RedirectUrl, new string[] { });

            var request = WebRequest.Create(authUrl);

            request.Credentials = CredentialCache.DefaultCredentials;

            var response = request.GetResponse();
        }
    }
}
