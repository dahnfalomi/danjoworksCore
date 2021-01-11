using danjoworksCoreLibrary.IPAddress.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace danjoworksCoreLibrary.IPAddress
{
    public class IPAddressService : IIPAddressService
    {
        private const string IPFindUrl = "https://api.ipfind.com/me?auth=4c4b618d-bcfb-4dd6-b5be-6d899a771fe6";
        private readonly HttpClient client;

        public IPAddressService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IPFindResponse> GetIPAddressLocation()
        {
            var httpResponse = await client.GetAsync(IPFindUrl);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<IPFindResponse>(content);

            return tasks;
        }
    }
}
