﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models
{
    public class DialogueModel
    {
        public int Id { get; set; }

        public int User1_Id { get; set; }

        public int User2_Id { get; set; }

        public List<MessageModel> Content { get; set; }
    }
}
