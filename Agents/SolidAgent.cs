using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SimpleSolidPrototype.Agents
{
    public class SolidAgent
    {
        private readonly string _webId;
        private readonly AuthenticationHeaderValue _authenticationHeader;

        private async Task<string> GetAsync(string resource)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_webId);
                client.DefaultRequestHeaders.Authorization = _authenticationHeader;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/turtle"));

                //client.DefaultRequestHeaders.Add("Origin", "https://localhost:44345");

                var responseMessage = await client.GetAsync(resource);
                //responseMessage.EnsureSuccessStatusCode();

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
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", accessToken);
            _webId = webId;
        }

        public async Task<string> GetPrivateFolderTurtle()
        {
            var content = await GetAsync("private");
            return content;
        }
    }
}
