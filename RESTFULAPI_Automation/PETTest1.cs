using Newtonsoft.Json;
using RESTFULAPI_Automation.Model;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace RESTFULAPI_Automation
{
    [TestClass]
    public class PETTest1
    {
        private static HttpClient httpClient;
        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";
        private static readonly string PetEndpoint = "pet";
        private static string GetUrl(string endpoint) => $"{BaseURL}{endpoint}";
        private static Uri GetURI(string endpoint) => new Uri(GetUrl(endpoint));

        private readonly List<PetClass> CleanUpList = new List<PetClass>();

        [TestInitialize]
        public void TestInitialize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in CleanUpList)
            {
                await httpClient.DeleteAsync(GetURI($"{PetEndpoint}/{data.Id}"));
            }
        }


        [TestMethod]
        public async Task TestMethod()
        // Create Json Object
        {
            PetClass petData = new PetClass()
            {
                PetCategory = new PetCategory { Name = "Dogie" },
                Name = "Brownie",
                PhotoUrls = new string[] { "photo " },
                Tags = new PetCategory[] { new PetCategory { Name = "Tags" } },
                Status = "available"
            };

            // Serialize Content

            var request = JsonConvert.SerializeObject(petData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Request
            var httpResponse = await httpClient.PostAsync(GetURI(PetEndpoint), postRequest);

            // Get Status Code
            var obj = JsonConvert.DeserializeObject<PetClass>(httpResponse.Content.ReadAsStringAsync().Result);


            petData = new PetClass()
            {
                Id = obj.Id,
                PetCategory = new PetCategory { Name = "Dogie" },
                Name = "Pringles",
                PhotoUrls = new string[] { "photo " },
                Tags = new PetCategory[] { new PetCategory { Name = "Tags" } },
                Status = "available"
            };

            request = JsonConvert.SerializeObject(petData);
            postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            httpResponse = await httpClient.PutAsync(GetURI(PetEndpoint), postRequest);

            var updatedPet = JsonConvert.DeserializeObject<PetClass>(httpResponse.Content.ReadAsStringAsync().Result);

            CleanUpList.Add(updatedPet);

            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK, "Status code is not equal to 200");
            Assert.AreEqual(petData.Name, updatedPet.Name, "Pet name does not match");
        }

        }

    }
