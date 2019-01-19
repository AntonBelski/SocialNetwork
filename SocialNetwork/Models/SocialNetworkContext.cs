using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Models
{
    public class SocialNetworkContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<DialogueModel> Dialogues { get; set; }
        public DbSet<MessageModel> Messages { get; set; }

        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
