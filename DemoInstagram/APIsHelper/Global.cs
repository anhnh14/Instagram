﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInstagram.APIsHelper
{
    public static class Global
    {
        private static string token = "";

        public static string TOKEN
        {
            get { return token; }
            set { token = value; }
        }

    }
}
