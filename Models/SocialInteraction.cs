using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CardinalHub.Models
{
    public class SocialInteraction
    {
        public int ID { get; set; }
        [Index("SocialInteractionIndex", 1, IsUnique = true)]
        public int CardEventID { get; set; }
        [Index("SocialInteractionIndex", 2, IsUnique = true)]
        public int CardHubUserID { get; set; }
        public EventStatus EventStatus { get; set; }
        public virtual CardHubUser CardHubUser { get; set; }
    }

    public enum EventStatus
    {
        [Display(Name = "Invited")]
        Invited,
        [Display(Name = "Going")]
        Going,
        [Display(Name = "Not Going")]
        NotGoing,
        [Display(Name = "Maybe")]
        Maybe,
        [Display(Name = "Ignore")]
        Ignore,
        [Display(Name = "None")]
        None,
    }

}