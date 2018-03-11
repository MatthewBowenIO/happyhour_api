using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using happyhour_api.Models;
using Newtonsoft.Json;

namespace happyhour_api.Controllers
{
    [Route("api/[controller]")]
    public class SignupController
    {
        private static readonly HttpClient client = new HttpClient();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(SlashCommandPayload slashCommandPayload)
        {
            var variable = slashCommandPayload;
            var responseUrl = slashCommandPayload.response_url;

            var response = SendResponseAsync(responseUrl);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public async Task<string> SendResponseAsync(string responseUrl) {
            var giphyUrl = await GetGiphyGif();

            var content = new StringContent("{\"text\":\"Hope this helps!\",\"attachments\":[{\"title\":\"Look at that little guy!\",\"fallback\":\"Your gif for the day!\",\"image_url\": \"" + giphyUrl + "\"}]}", Encoding.UTF8, "application/json");

            var response = await client.PostAsync(responseUrl, content);

            return "Success";
        }

        public async Task<string> GetGiphyGif() {
            var results = await client.GetAsync("http://api.giphy.com/v1/gifs/random?tag=puppy&api_key=RP1iehzMsudM8gbnWC4y255gKuPZMuwN");
            var giphyPayload = JsonConvert.DeserializeObject<GiphyPayload>(results.Content.ReadAsStringAsync().Result);

            return giphyPayload.data.images.original.url;
        }
    }
}
