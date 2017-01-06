using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CardinalHub.Models
{
    public enum EventType
    {
        [Display(Name = "Study Session")]
        StudySession,
        [Display(Name = "Calculus Study")]
        CalculusStudy,
        [Display(Name = "Finals Prep")]
        FinalsPrep,
        [Display(Name = "Info Lecture")]
        InfoLecture,
        [Display(Name = "Food Event")]
        FoodEvent,
        [Display(Name = "Movie Event")]
        MovieEvent,
        [Display(Name = "Other")]
        Other,
    }
    public class CardEvent
    {
        public int CardEventID { get; set; }
        public int LocationID { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string PhoneNo { get; set; }
        public string WebSiteURL { get; set; }
        public int CardHubUserID { get; set; }
        public DateTime? EventStartDateTime { get; set; }
        public DateTime? EventStartEndTime { get; set; }
        public EventType EventType { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<SocialInteraction> SocialInteractions { get; set; }
        [NotMapped]
        public int GoingCount { get; set; }
        [NotMapped]
        public int MaybeCount { get; set; }
        [NotMapped]
        public int InvitedCount { get; set; }
    }

    public class EventViewClass
    {
        public IEnumerable<CardEvent> CardEvents { get; set; }
        public CardHubUser CardHubUser { get; set; }
        public IconType IconType { get; set; }
        public IconMarker IconMarker { get; set; }
        public EventType EventType { get; set; }

        public string AvatarImg 
        {
            get
            {
                if(CardHubUser.Avatar == null || CardHubUser.Avatar.Trim() == String.Empty)
                {
                    return "/Content/Images/defaultUserImage.png";
                }
                else
                {
                    return "http://www.gravatar.com/avatar/" + CardHubUser.Avatar.ToLower();      
                }
            }
        }
    }

    public class EventPageViewClass
    {        
        public CardEvent CardEvent { get; set; }
        public int CurrentUserId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? EventDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public DateTime? EventStartTime { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public DateTime? EventEndTime { get; set; }
        public IconType IconType { get; set; }
        public IconMarker IconMarker { get; set; }
    }
}