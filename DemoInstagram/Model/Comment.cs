using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInstagram.Model
{
    public class Comment
    {
        public string id { get; set; }
        public Profile from { get; set; }
        public string text { get; set; }
        public string created_time { get; set; }
    }
}
