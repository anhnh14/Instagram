using DemoInstagram.Model;
using System.Collections.Generic;

namespace DemoInstagram.Business.Interface
{
    public interface IInstagramBusiness
    {
        string GetPath(string typeAPI, string parameter);

        Profile ProcessProfile(string json);

        Picture ProcessPicture(string json);

        List<Profile> ProcessListProfile(string json);

        List<Comment> LoadComment(string json);
    }
}
