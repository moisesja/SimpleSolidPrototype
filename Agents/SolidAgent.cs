﻿using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleSolidPrototype.Agents
{
    public class SolidAgent
    {
        private readonly string _webId;
        private readonly string _accessToken;
        private readonly AuthenticationHeaderValue _authenticationHeader;
        private readonly string _origin;

        private void ListHeaders(HttpClient client)
        {
            foreach (var header in client.DefaultRequestHeaders)
            {
                System.Diagnostics.Debug.WriteLine($"{header.Key} - {header.Value.First()}");
            }
        }

        private async Task<string> GetAsync(string resource)
        {
            var httpClientHandler = new HttpClientHandler();

            // TODO: Do this conditionally based on environment
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; // DEBUGGING ONLY
            
            using (var client = new HttpClient(httpClientHandler))
            {
                //client.BaseAddress = new Uri(_webId);

                //client.DefaultRequestHeaders.Authorization = _authenticationHeader;

                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept
                //client.DefaultRequestHeaders.Add("Origin", "https://localhost:44345");
                //client.DefaultRequestHeaders.Add("Referer", "https://localhost:44345");

                var url = $"{_webId}/{resource}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = _authenticationHeader;
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/turtle"));
                request.Headers.Add("Origin", _origin);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    //var json = await getResponse.Content.ReadAsStringAsync();
                    dynamic obj = (dynamic)JsonConvert.DeserializeObject(responseContent);

                    //throw new FindMyRelativesBusinessException(string.Format("{0} - {1}", obj.error, obj.error_description));
                }

                return responseContent;
            }
        }

        public SolidAgent(string accessToken, string webId, string origin)
        {
            _accessToken = accessToken;
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", accessToken);
            _webId = webId;
            _origin = origin;
        }

        public async Task<string> GetAccounts()
        {            
            return await GetAsync("private/");
        }
    }
}
