using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SimpleSolidPrototype.Agents
{
    public class SolidAgent
    {
        private readonly string _webId;
        private readonly string _accessToken;
        private readonly AuthenticationHeaderValue _authenticationHeader;

        private void ListHeaders(HttpClient client)
        {
            foreach (var header in client.DefaultRequestHeaders)
            {
                System.Diagnostics.Debug.WriteLine($"{header.Key} - {header.Value.First()}");
            }
        }

        private async Task<string> GetAsync(string resource)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(_webId);

                //client.DefaultRequestHeaders.Authorization = _authenticationHeader;

                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept
                //client.DefaultRequestHeaders.Add("Origin", "https://localhost:44345");
                //client.DefaultRequestHeaders.Add("Referer", "https://localhost:44345");

                var url = $"{_webId}/{resource}";

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Authorization = _authenticationHeader;
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/turtle"));

                var responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

                var responseText = await responseMessage.Content.ReadAsStringAsync();
                
                if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    //var json = await getResponse.Content.ReadAsStringAsync();
                    //dynamic obj = (dynamic)JsonConvert.DeserializeObject(json);

                    //throw new FindMyRelativesBusinessException(string.Format("{0} - {1}", obj.error, obj.error_description));
                }

                return responseText;
            }
        }

        public SolidAgent(string accessToken, string webId)
        {
            _accessToken = accessToken;
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", accessToken);
            _webId = webId;
        }

        public async Task<string> GetPrivateFolderTurtle()
        {            
            return await GetAsync("inbox/");
        }
    }
}
