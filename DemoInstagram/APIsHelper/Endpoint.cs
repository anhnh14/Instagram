using DemoInstagram.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoInstagram.APIsHelper
{
    public class Endpoint
    {
        HttpClient client = new HttpClient();

        //Get profile about owner of the access_token. 
        public async Task<Profile> getProfile()
        {
            string path = Configuaration.API_USER + "self/?access_token=" + Global.TOKEN;
            HttpResponseMessage response = await client.GetAsync(path);
            Profile profile = new Profile();
            var jsonString = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                dynamic stuff = JObject.Parse(jsonString);
                profile = JsonConvert.DeserializeObject<Profile>(stuff["data"].ToString());

            }
            else
            {
                var jsonError = response.Content.ReadAsStringAsync().Result;
                dynamic error = JObject.Parse(jsonError);
                ErrorAPis errorApis = JsonConvert.DeserializeObject<ErrorAPis>(error["meta"].ToString());
                throw new Exception(errorApis.error_message);
            }
            return profile;
        }

        //Get the most recent media published by the owner of the access_token. 
        public async Task<Picture> getRecentImage()
        {
            string path = Configuaration.API_USER + "self/media/recent?access_token=" + Global.TOKEN;
            HttpResponseMessage response = await client.GetAsync(path);
            List<string> list = new List<string>();
            string jsonString = response.Content.ReadAsStringAsync().Result;

            Picture picture = new Picture();
            if (response.IsSuccessStatusCode)
            {
                dynamic stuff = JObject.Parse(jsonString);
                var v = stuff["data"];
                JArray v1 = JArray.Parse(v.ToString());
                if (v1.Count > 0)
                {
                    string id = v1[0]["id"].ToString();
                    var standImage = v1[0]["images"]["standard_resolution"];
                    picture = JsonConvert.DeserializeObject<Picture>(standImage.ToString());
                    picture.id = id;
                }

            }
            else
            {
                var jsonError = response.Content.ReadAsStringAsync().Result;
                dynamic error = JObject.Parse(jsonError);
                ErrorAPis errorApis = JsonConvert.DeserializeObject<ErrorAPis>(error["meta"].ToString());
                throw new Exception(errorApis.error_message);
            }
            return picture;
        }

        //Get a list of users matching the query. 
        public async Task<List<Profile>> searchUser(string userName)
        {
            string path = Configuaration.API_USER + "search?q=" + userName + "&access_token=" + Global.TOKEN;
            HttpResponseMessage response = await client.GetAsync(path);
            Profile profile = new Profile();
            List<Profile> listProfile = new List<Profile>();
            var jsonString = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                dynamic stuff = JObject.Parse(jsonString);
                foreach (var item in stuff["data"])
                {
                    profile = JsonConvert.DeserializeObject<Profile>(item.ToString());
                    listProfile.Add(profile);
                }
            }
            else
            {
                var jsonError = response.Content.ReadAsStringAsync().Result;
                dynamic error = JObject.Parse(jsonError);
                ErrorAPis errorApis = JsonConvert.DeserializeObject<ErrorAPis>(error["meta"].ToString());
                throw new Exception(errorApis.error_message);
            }
            return listProfile;
        }

        //Get the most recent media published by user from search list
        public async Task<Picture> getImageRecentPublishByUser(string userId)
        {
            string path = Configuaration.API_USER + userId + "/media/recent/?access_token=" + Global.TOKEN;
            HttpResponseMessage response = await client.GetAsync(path);
            List<string> list = new List<string>();
            string jsonString = response.Content.ReadAsStringAsync().Result;
            Picture image = new Picture();
            if (response.IsSuccessStatusCode)
            {
                dynamic stuff = JObject.Parse(jsonString);
                var v = stuff["data"];
                JArray v1 = JArray.Parse(v.ToString());
                if (v1.Count > 0)
                {
                    string id = v1[0]["id"].ToString();
                    var standImage = v1[0]["images"]["standard_resolution"];
                    image = JsonConvert.DeserializeObject<Picture>(standImage.ToString());
                    image.id = id;
                }
            }
            else
            {
                var jsonError = response.Content.ReadAsStringAsync().Result;
                dynamic error = JObject.Parse(jsonError);
                ErrorAPis errorApis = JsonConvert.DeserializeObject<ErrorAPis>(error["meta"].ToString());
                throw new Exception(errorApis.error_message);
            }
            return image;
        }

        public async void postComment(string comment, string pictureId)
        {
            var client = new HttpClient();

            var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("access_token",Global.TOKEN),
                    new KeyValuePair<string, string>("text", comment)
                });
            try
            {
                HttpResponseMessage response = await client.PostAsync(Configuaration.API_MEDIA + pictureId + "/comments", requestContent);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
           
        }

        public async Task<List<Comment>> loadComments(string imageId)
        {
            string path = Configuaration.API_MEDIA + imageId+ "/comments?access_token=" + Global.TOKEN;
            HttpResponseMessage response = await client.GetAsync(path);
            List<Comment> listComment = new List<Comment>();
            var jsonString = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                dynamic stuff = JObject.Parse(jsonString);
                foreach (var item in stuff["data"])
                {
                    var comment = JsonConvert.DeserializeObject<Comment>(item.ToString());
                    listComment.Add(comment);
                }
            }
            else
            {
                var jsonError = response.Content.ReadAsStringAsync().Result;
                dynamic error = JObject.Parse(jsonError);
                ErrorAPis errorApis = JsonConvert.DeserializeObject<ErrorAPis>(error["meta"].ToString());
                throw new Exception(errorApis.error_message);
            }
            return listComment;
        }

        public async void likeImage(string pictureId)
        {
            var client = new HttpClient();

            var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("access_token",Global.TOKEN),
                });
            try
            {
                HttpResponseMessage response = await client.PostAsync(Configuaration.API_MEDIA + pictureId + "/likes", requestContent);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
