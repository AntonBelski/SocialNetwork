using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }
    }
}
