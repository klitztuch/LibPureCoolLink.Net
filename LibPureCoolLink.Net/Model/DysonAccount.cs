using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace LibPureCoolLink.Net.Model
{
    public class DysonAccount
    {
        private readonly string _api;
        private readonly string _country;
        private readonly string _email;
        private readonly string _password;

        #region Ctor

        /// <summary>
        /// </summary>
        /// <param name="api"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="country"></param>
        public DysonAccount(string api,
            string email,
            string password,
            string country)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _email = email ?? throw new ArgumentNullException(nameof(email));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            _country = country ?? throw new ArgumentNullException(nameof(country));
        }

        #endregion

        public bool IsLoggedIn { get; set; }
        public Authentication Authentication { get; set; }

        #region Methods

        public bool Login()
        {
            var requestApi = $"https://{_api}/v1/userregistration/authenticate?country={_country}";
            var user = new Login
            {
                Email = _email,
                Password = _password
            };
            var client = new RestClient(requestApi);
            var request = new RestRequest()
                .AddJsonBody(JsonSerializer.Serialize(user));

            var response = client.Post<Authentication>(request);
            if (response.StatusCode != HttpStatusCode.OK || response.Data == null) return false;

            Authentication = response.Data;
            IsLoggedIn = true;
            return true;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        public IEnumerable<IDevice> GetDevices()
        {
            if (!IsLoggedIn) throw new AuthenticationException("User is not logged in.");

            var requestApi = $"https://{_api}/v2/provisioningservice/manifest";
            var client = new RestClient(requestApi)
            {
                Authenticator = new HttpBasicAuthenticator(Authentication.Account, Authentication.Password)
            };
            var request = new RestRequest();

            var response = client.Get<IEnumerable<Device>>(request);
            return response.Data;
        }

        #endregion
    }
}