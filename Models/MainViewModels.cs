using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardinalHub.Models
{
    public class MainViewClass
    {
        public CardHubUser CardHubUser { get; set; }
        public IEnumerable<CardEvent> CardEvents { get; set; }
        public IEnumerable<SocialObject> SocialObject { get; set; }
        public IEnumerable<SocialInteraction> SocialInteraction { get; set; }

    }
}