using DemoInstagram.Model;
using System.Collections.Generic;
using System.Net.Http;

namespace DemoInstagram.Business.Interface
{
    public interface IBusiness
    {
        string GetPath(string typeAPI, string parameter);

        Profile ProcessProfile(string json);

        Picture ProcessPicture(string json);

        List<Profile> ProcessListProfile(string json);

        List<Comment> LoadComment(string json);

        string ProcessError(HttpResponseMessage response);
    }
}
