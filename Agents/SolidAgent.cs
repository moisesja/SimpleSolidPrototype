using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SimpleSolidPrototype.Agents
{
    public class SolidAgent
    {
        /// <summary>
        /// HttpClient is intended to be instantiated once per application, rather than per-use.
        /// </summary>
        private static readonly HttpClient _client = new HttpClient();

        private readonly string _webId;
        private readonly AuthenticationHeaderValue _authenticationHeader;

        private async Task<string> GetAsync(string resource)
        {
            _client.BaseAddress = new Uri(_webId);
            _client.DefaultRequestHeaders.Authorization = _authenticationHeader;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/turtle"));

            var responseMessage = await _client.GetAsync(resource);
            var responseText = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //var json = await getResponse.Content.ReadAsStringAsync();
                //dynamic obj = (dynamic)JsonConvert.DeserializeObject(json);

                //throw new FindMyRelativesBusinessException(string.Format("{0} - {1}", obj.error, obj.error_description));
            }

            return responseText;
        }

        public SolidAgent(string accessToken, string webId)
        {
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", accessToken);
            _webId = webId;
        }

        public async Task<string> GetPrivateFolderTurtle()
        {
            var content = await GetAsync("Private");

            return content;
        }
    }
}
