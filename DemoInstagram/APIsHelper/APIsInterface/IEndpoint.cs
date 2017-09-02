using DemoInstagram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInstagram.APIsHelper.APIsInterface
{
    public interface IEndpoint
    {
        Task<Profile> GetProfile();

        Task<Picture> GetRecentImage();

        Task<List<Profile>> SearchUser(string userName);

        Task<Picture> GetImageRecentPublishByUser(string userId);

        Task<bool> PostComment(string comment, string pictureId);

        Task<List<Comment>> LoadComments(string imageId);

        Task<bool> LikeImage(string pictureId);
    }
}
