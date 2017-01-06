using System;
using System.Collections.Generic;
using CardinalHub.Models;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CardinalHub.DAL
{
    public class CardHubContext : DbContext
    {
        public CardHubContext() : base("CardinalHubConnection")
            {
            }
        public DbSet<CardHubUser> CardHubUsers { get; set; }
        public DbSet<SocialInteraction> SocialInteractions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CardEvent> CardEvents { get; set; }
        public DbSet<MapIcon> MapIcons { get; set; }



    }
}