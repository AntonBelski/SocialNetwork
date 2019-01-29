using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class CurrentTime : ICurrentTime
    {
        public string GetTime
        {
            get { return DateTime.Now.ToString("hh:mm:ss"); }
        }
    }
}
