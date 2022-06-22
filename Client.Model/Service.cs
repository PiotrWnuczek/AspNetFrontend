namespace Client.Model
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Text.Json;
    using System.Text;

    public class Service
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly string serviceHost;
        private readonly ushort servicePort;

        public Service()
        {
            this.serviceHost = "localhost";
            this.servicePort = 5000;
        }

        public Item[] GetItems()
        {
            string callUri = "GetItems";
            Item[] items = this.CallWebService<Item[]>(HttpMethod.Get, callUri);
            return items;
        }

        public Item[] GetItem(string searchText)
        {
            string callUri = String.Format("GetItem/{0}", searchText);
            Item[] items = this.CallWebService<Item[]>(HttpMethod.Get, callUri);
            return items;
        }

        public void AddItem(string NewItemId)
        {
            Task<string> call = AddItemCall(NewItemId);
            call.Wait();
        }

        public async Task<string> AddItemCall(string NewItemId)
        {
            Item item = new(NewItemId);
            var json = JsonSerializer.Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient httpClient = new();
            string requestUri = String.Format("http://{0}:{1}/AddItem", this.serviceHost, this.servicePort);
            httpClient.DefaultRequestHeaders.Add("Accept", "text/plain");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, data);
            httpResponseMessage.EnsureSuccessStatusCode();
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return httpResponseContent;
        }

        public DataType CallWebService<DataType>(HttpMethod httpMethod, string webServiceUri)
        {
            Task<string> webServiceCall = this.CallWebService(httpMethod, webServiceUri);
            webServiceCall.Wait();
            string jsonResponseContent = webServiceCall.Result;
            DataType result = this.ConvertJson<DataType>(jsonResponseContent);
            return result;
        }

        public async Task<string> CallWebService(HttpMethod httpMethod, string callUri)
        {
            string httpUri = String.Format("http://{0}:{1}/{2}", this.serviceHost, this.servicePort, callUri);
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, httpUri);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return httpResponseContent;
        }

        private DataType ConvertJson<DataType>(string json)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.PropertyNameCaseInsensitive = true;
            return JsonSerializer.Deserialize<DataType>(json, jsonSerializerOptions);
        }
    }
}
