using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using happyhour_api.Models;
using Newtonsoft.Json;
using happyhour_api.Enums;

namespace happyhour_api.Controllers
{
    [Route("api/[controller]")]
    public class SignupController : Controller
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
        [Produces("application/json")]
        public string Post(SlackCommandPayload slackCommandPayload)
        {
            HappyHourDB employStreamDb = HttpContext.RequestServices.GetService(typeof(HappyHourDB)) as HappyHourDB;
            var returnString = String.Empty;
            if(slackCommandPayload.text == null) {
                return "Let us know what time you want to receive happy/cute gifs! 'happyhour Morning' for 10:30, 'happyhour Afternoon' for 3:30 or 'happyhour Both' for both! Because you deserve it!";
            } else {
                switch (slackCommandPayload.text.ToLower()) {
                    case "emergency":
                        SendResponseAsync(slackCommandPayload.response_url);
                        break;
                    case "morning":
                        slackCommandPayload.opt_in_time = OptedInTime.Morning;
                        returnString = "Thanks! You'll start receiving (hopefully) cute gifs at 10:30 every moring!";
                        break;
                    case "afternoon":
                        slackCommandPayload.opt_in_time = OptedInTime.Afternoon;
                        returnString = "Thanks! You'll start receiving (hopefully) cute gifs at 3:30 every afternoon!";
                        break;
                    case "both":
                        slackCommandPayload.opt_in_time = OptedInTime.Both;
                        returnString = "Oh no! Things must be rough. We'll make sure you get a little pick me up at 10:30am and 3:30pm!";
                        break;
                    default:
                        break;
                }

                if(employStreamDb.DoesUserExist(slackCommandPayload)) {
                    employStreamDb.UpdateUser(slackCommandPayload);
                } else {
                    employStreamDb.SaveUser(slackCommandPayload);
                }
            }
            return returnString;
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
            var content = new StringContent("{\"text\":\"Hope this helps!\",\"attachments\":[{\"fallback\":\"Your gif for the day!\",\"image_url\": \"" + giphyUrl + "\"}]}", Encoding.UTF8, "application/json");
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
