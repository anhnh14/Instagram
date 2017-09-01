using DemoInstagram.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoInstagram.APIsHelper
{
    public class Endpoint
    {
        /// <summary>
        /// Get profile about owner of the access_token. 
        /// </summary>
        /// <returns>Profile</returns>
        public async Task<Profile> getProfile()
        {
            string path = Configuaration.API_USER + "self/?access_token=" + Global.TOKEN;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    Profile profile = new Profile();
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic stuff = JObject.Parse(jsonString);
                        profile = JsonConvert.DeserializeObject<Profile>(stuff["data"].ToString());
                    }
                    else
                    {
                        ErrorRequest(response);
                    }
                    return profile;
                }
            }


        }

        /// <summary>
        /// Get the most recent media published by the owner of the access_token. 
        /// </summary>
        /// <returns>Picture</returns>
        public async Task<Picture> getRecentImage()
        {
            string path = Configuaration.API_USER + "self/media/recent?access_token=" + Global.TOKEN;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    List<string> list = new List<string>();
                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    Picture picture = new Picture();
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic stuff = JObject.Parse(jsonString);
                        var v = stuff[Configuaration.KEY_API_DATA];
                        JArray v1 = JArray.Parse(v.ToString());
                        if (v1.Count > 0)
                        {
                            string id = v1[0][Configuaration.KEY_API_ID].ToString();
                            var standImage = v1[0][Configuaration.KEY_API_IMAGES][Configuaration.KEY_API_IMAGES_STANDARD_RESOLUTION];
                            picture = JsonConvert.DeserializeObject<Picture>(standImage.ToString());
                            picture.id = id;
                        }
                    }
                    else
                    {
                        ErrorRequest(response);
                    }
                    return picture;
                }
            }


        }

        /// <summary>
        /// Get a list of users matching the query. 
        /// </summary>
        /// <param name="userName">input name of user</param>
        /// <returns>List of user</returns>
        public async Task<List<Profile>> searchUser(string userName)
        {
            string path = Configuaration.API_USER + "search?q=" + userName + "&access_token=" + Global.TOKEN;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    Profile profile = new Profile();
                    List<Profile> listProfile = new List<Profile>();
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic stuff = JObject.Parse(jsonString);
                        foreach (var item in stuff[Configuaration.KEY_API_DATA])
                        {
                            profile = JsonConvert.DeserializeObject<Profile>(item.ToString());
                            listProfile.Add(profile);
                        }
                    }
                    else
                    {
                        ErrorRequest(response);
                    }
                    return listProfile;
                }
            }

        }

        /// <summary>
        /// Get the most recent media published by user from search list
        /// </summary>
        /// <param name="userId">user id from instagram</param>
        /// <returns>picture</returns>
        public async Task<Picture> getImageRecentPublishByUser(string userId)
        {
            string path = Configuaration.API_USER + userId + "/media/recent/?access_token=" + Global.TOKEN;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    List<string> list = new List<string>();
                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    Picture image = new Picture();
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic stuff = JObject.Parse(jsonString);
                        var v = stuff[Configuaration.KEY_API_DATA];
                        JArray v1 = JArray.Parse(v.ToString());
                        if (v1.Count > 0)
                        {
                            string id = v1[0][Configuaration.KEY_API_ID].ToString();
                            var standImage = v1[0][Configuaration.KEY_API_IMAGES][Configuaration.KEY_API_IMAGES_STANDARD_RESOLUTION];
                            image = JsonConvert.DeserializeObject<Picture>(standImage.ToString());
                            image.id = id;
                        }
                    }
                    else
                    {
                        ErrorRequest(response);
                    }
                    return image;
                }

            }

        }

        /// <summary>
        /// Post comment to image
        /// </summary>
        /// <param name="comment">content of comment</param>
        /// <param name="pictureId">id of image</param>
        /// <returns>true if post comment success else return false</returns>
        public async Task<bool> postComment(string comment, string pictureId)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>(Configuaration.ACCESS_TOKEN,Global.TOKEN),
                    new KeyValuePair<string, string>(Configuaration.KEY_API_TEXT, comment)
                });
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    response = await client.PostAsync(Configuaration.API_MEDIA + pictureId + "/comments", requestContent);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }


        }

        /// <summary>
        /// Load comment from image
        /// </summary>
        /// <param name="imageId">id of image</param>
        /// <returns>List comment</returns>
        public async Task<List<Comment>> loadComments(string imageId)
        {
            string path = Configuaration.API_MEDIA + imageId + "/comments?access_token=" + Global.TOKEN;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    List<Comment> listComment = new List<Comment>();
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic stuff = JObject.Parse(jsonString);
                        foreach (var item in stuff[Configuaration.KEY_API_DATA])
                        {
                            var comment = JsonConvert.DeserializeObject<Comment>(item.ToString());
                            listComment.Add(comment);
                        }
                    }
                    else
                    {
                        ErrorRequest(response);
                    }
                    return listComment;
                }
            }

        }
        
        /// <summary>
        /// Like image
        /// </summary>
        /// <param name="pictureId">id of image</param>
        public async void likeImage(string pictureId)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>(Configuaration.ACCESS_TOKEN,Global.TOKEN),
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

        /// <summary>
        /// process error response
        /// </summary>
        /// <param name="response">response from server</param>
        private void ErrorRequest(HttpResponseMessage response)
        {
            var jsonError = response.Content.ReadAsStringAsync().Result;
            dynamic error = JObject.Parse(jsonError);
            ErrorAPis errorApis = JsonConvert.DeserializeObject<ErrorAPis>(error[Configuaration.KEY_API_META].ToString());
            throw new Exception(errorApis.error_message);
        }
    }
}
