using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CardinalHub.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public int MapIconID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public string ContentString { get; set; }
        public string Annotation { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public virtual MapIcon MapIcon { get; set; }
    }

    public class MapIcon
    {
        public int MapIconID { get; set; }
        public string FileName { get; set; }
        public int basePoints { get; set; }
        public string IconName { get; set; }
        public string fillColor { get; set; }
        public int? fillOpacity { get; set; }
        public IconType IconType { get; set; }
        public IconMarker IconMarker { get; set; }
    }

    public enum IconType
    {
     art_gallery,
     bank,
     gym,
     point_of_interest,
     post_office,
     university,
     school,
     library,
     spa,
     route,
     postal_code,
     museum,
     finance,
     natural_feature,
     park,
     male,
     female,
     unisex,
     bakery,
     cafe,
     restaurant,
     food,
     pet_store,
     book_store,
     electronics_store,
     grocery_or_supermarket,
     climbing,
     baseball,
     tennis,
     walking,
     kayaking,
     swimming,
     snow,
     bus_station,
     parking,
     airport,
     health,
     movie_theater,
     bowling_alley,
     locksmith,
     lawyer,
     accounting,
     political,
     local_government,
     embassy,
     city_hall,
     fire_station,
     police,
     church,
     wheelchair,
    }
    public enum IconMarker
    {
        [Display(Name = "Map Pin")]
        MAP_PIN,
        [Display(Name = "Square Pin")]
        SQUARE_PIN,
        [Display(Name = "Shield")]
        SHIELD,
        [Display(Name = "Route")]
        ROUTE,
        [Display(Name = "Square")]
        SQUARE,
        [Display(Name = "Rounded Square")]
        SQUARE_ROUNDED
    }
}