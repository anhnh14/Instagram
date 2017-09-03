using System.Collections.Generic;
using DemoInstagram.Business.Interface;
using Newtonsoft.Json;
using DemoInstagram.APIsHelper;
using Newtonsoft.Json.Linq;
using DemoInstagram.Model;
using System;
using System.Net.Http;

namespace DemoInstagram.Business
{
    public class InstagramBussinessImpl : IBusiness
    {
        /// <summary>
        /// Create path to API
        /// </summary>
        /// <param name="typeAPI">Type of API</param>
        /// <param name="parameter">add some parameter need for API</param>
        /// <returns>API</returns>
        public string GetPath(string typeAPI, string parameter)
        {
            return typeAPI + parameter + Global.TOKEN;
        }

        /// <summary>
        /// Get list comment of image
        /// </summary>
        /// <param name="json">json string from server response</param>
        /// <returns>List comment</returns>
        public List<Comment> LoadComment(string json)
        {
            List<Comment> listComment = new List<Comment>();
            dynamic stuff = JObject.Parse(json);
            foreach (var item in stuff[Configuaration.KEY_API_DATA])
            {
                var comment = JsonConvert.DeserializeObject<Comment>(item.ToString());
                listComment.Add(comment);
            }
            return listComment;
        }

       

        /// <summary>
        /// Get list profile from json
        /// </summary>
        /// <param name="json">json from server response</param>
        /// <returns>List profile</returns>
        public List<Profile> ProcessListProfile(string json)
        {
            List<Profile> listProfile = new List<Profile>();
            
            dynamic stuff = JObject.Parse(json);
            foreach (var item in stuff[Configuaration.KEY_API_DATA])
            {
                Profile profile = JsonConvert.DeserializeObject<Profile>(item.ToString());
                listProfile.Add(profile);
            }
            return listProfile;
        }

        /// <summary>
        /// Process image from json
        /// </summary>
        /// <param name="json">json from server response</param>
        /// <returns>Picture</returns>
        public Picture ProcessPicture(string json)
        {
            Picture picture = new Picture();
            dynamic stuff = JObject.Parse(json);
            var v = stuff[Configuaration.KEY_API_DATA];
            JArray v1 = JArray.Parse(v.ToString());
            if (v1.Count > 0)
            {
                string id = v1[0][Configuaration.KEY_API_ID].ToString();
                var standImage = v1[0][Configuaration.KEY_API_IMAGES][Configuaration.KEY_API_IMAGES_STANDARD_RESOLUTION];
                picture = JsonConvert.DeserializeObject<Picture>(standImage.ToString());
                picture.id = id;
            }
            return picture;
        }

        /// <summary>
        /// Proces profile from json
        /// </summary>
        /// <param name="json">json string from server response</param>
        /// <returns>Profile</returns>
        public Profile ProcessProfile(string json)
        {
            dynamic stuff = JObject.Parse(json);
            Profile profile = JsonConvert.DeserializeObject<Profile>(stuff[Configuaration.KEY_API_DATA].ToString());
            return profile;
        }

        /// <summary>
        /// Parse json return error from server
        /// </summary>
        /// <param name="response">response from server</param>
        /// <returns>Message error</returns>
        public string ProcessError(HttpResponseMessage response)
        {
            var jsonError = response.Content.ReadAsStringAsync().Result;
            dynamic error = JObject.Parse(jsonError);
            ErrorAPis errorApis = JsonConvert.DeserializeObject<ErrorAPis>(error[Configuaration.KEY_API_META].ToString());
            return errorApis.error_message;

        }
    }
}
