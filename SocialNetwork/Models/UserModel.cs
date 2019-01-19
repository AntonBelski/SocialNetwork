using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }
        
        public string Password { get; set; }

        public string Sex { get; set; }
    
        public List<UserModel> Friends { get; set; }

        public List<DialogueModel> Dialogues { get; set; }
    }
}
