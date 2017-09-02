using DemoInstagram.APIsHelper.APIsInterface;
using DemoInstagram.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DemoInstagram.Business.Interface;

namespace DemoInstagram.APIsHelper
{
    public class EndpointInstagram : IEndpoint
    {
        private readonly IInstagramBusiness _instagramBussiness;

        public EndpointInstagram(IInstagramBusiness instagramBussiness)
        {
            this._instagramBussiness = instagramBussiness;
        }

        /// <summary>
        /// Get profile about owner of the access_token. 
        /// </summary>
        /// <returns>Profile</returns>
        public async Task<Profile> GetProfile()
        {
            string path = _instagramBussiness.GetPath(Configuaration.API_USER, "self/?access_token=");
            //connect to API
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    Profile profile = new Profile();

                    //check response from server
                    if (response.IsSuccessStatusCode)
                    {
                        //get profile of owner token
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        profile = _instagramBussiness.ProcessProfile(jsonString);
                    }
                    else
                    {
                        //Process json error from server
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
        public async Task<Picture> GetRecentImage()
        {
            //Get path recent image
            string path = _instagramBussiness.GetPath(Configuaration.API_USER, "self/media/recent?access_token=");

            //Connect to API
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    List<string> list = new List<string>();
                    Picture picture = new Picture();
                    if (response.IsSuccessStatusCode)
                    {
                        //Get json from Server
                        string jsonString = response.Content.ReadAsStringAsync().Result;

                        //Process image from json
                        picture = _instagramBussiness.ProcessPicture(jsonString);
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
        public async Task<List<Profile>> SearchUser(string userName)
        {
            //Get path list user 
            string path = _instagramBussiness.GetPath(Configuaration.API_USER, "search?q=" + userName + "&&access_token=");

            //Connect to API 
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    List<Profile> listProfile = new List<Profile>();
                    if (response.IsSuccessStatusCode)
                    {
                        //Get json string from response
                        var jsonString = response.Content.ReadAsStringAsync().Result;

                        //process list profile from json
                        listProfile = _instagramBussiness.ProcessListProfile(jsonString);
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
        public async Task<Picture> GetImageRecentPublishByUser(string userId)
        {

            //Get path image recent published by user
            string path = _instagramBussiness.GetPath(Configuaration.API_USER, userId + "/media/recent/?access_token=");

            //Connect to API
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    List<string> list = new List<string>();
                    Picture picture = new Picture();
                    if (response.IsSuccessStatusCode)
                    {
                        //Get json from reponse
                        string jsonString = response.Content.ReadAsStringAsync().Result;

                        //Process image from json
                        picture = _instagramBussiness.ProcessPicture(jsonString);
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
        /// Post comment to image
        /// </summary>
        /// <param name="comment">content of comment</param>
        /// <param name="pictureId">id of image</param>
        /// <returns>true if post comment success else return false</returns>
        public async Task<bool> PostComment(string comment, string pictureId)
        {
            //Connect to API
            using (var client = new HttpClient())
            {
                //Prepare comment from user input
                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>(Configuaration.ACCESS_TOKEN,Global.TOKEN),
                    new KeyValuePair<string, string>(Configuaration.KEY_API_TEXT, comment)
                });
                HttpResponseMessage response = new HttpResponseMessage();

                //post comment
                response = await client.PostAsync(Configuaration.API_MEDIA + pictureId + "/comments", requestContent);

                //check status
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
        public async Task<List<Comment>> LoadComments(string imageId)
        {
            //Get path list comment
            string path = _instagramBussiness.GetPath(Configuaration.API_MEDIA, imageId + "/comments?access_token=");

            //Connect to API
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(path))
                {
                    List<Comment> listComment = new List<Comment>();
                    if (response.IsSuccessStatusCode)
                    {
                        //Get json from response
                        var jsonString = response.Content.ReadAsStringAsync().Result;

                        //Process list comment from json
                        listComment = _instagramBussiness.LoadComment(jsonString);
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
        public async Task<bool> LikeImage(string pictureId)
        {
            //Connect to API
            using (var client = new HttpClient())
            {
                //Prepare data to like
                //Add token to url
                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>(Configuaration.ACCESS_TOKEN,Global.TOKEN),
                });

                //Send like request
                HttpResponseMessage response = await client.PostAsync(Configuaration.API_MEDIA + pictureId + "/likes", requestContent);

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
