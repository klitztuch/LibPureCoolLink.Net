using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serialization.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LibPureCoolLink.Net.Model
{
    public class DysonAccount
    {
        private readonly string _api;
        private readonly string _email;
        private readonly string _password;
        private readonly string _country;

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="country"></param>
        public DysonAccount(string api, string email, string password, string country)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _email = email ?? throw new ArgumentNullException(nameof(email));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            _country = country ?? throw new ArgumentNullException(nameof(country));
        }
        
        #endregion

        #region Methods

        public bool Login()
        {
            var requestApi = $"https://{_api}/v1/userregistration/authenticate?country={_country}";
            var user = new Login()
            {
                Email = _email,
                Password = _password
            };
            var client = new RestClient(requestApi);
            var request = new RestRequest()
                .AddJsonBody(JsonSerializer.Serialize(user));

            var response = client.Post<Authentication>(request);

            return response.Data != null;
        }

        #endregion
    }
}