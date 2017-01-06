using CardinalHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CardinalHub.Models
{
    public class CardHubUser
    {
        public int CardHubUserID { get; set; }
        public int PerkPoints { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Motto { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Location Location { get; set; }
    }

    public static class CardHubUserMaster
    {
        public static void CreateNewCardUser(ApplicationUser user, string firstName, string lastName, string avatar,string motto)
        {
            CardHubContext db;
            Location location;
            CardHubUser cardUser;
            MapIcon icon;

            db = new CardHubContext();

            icon = new MapIcon { basePoints = 0, IconType = IconType.postal_code, IconMarker = IconMarker.MAP_PIN, fillColor = "#FF0000" };
            location = new Location { };
            location.City = "Louisville";
            location.State = "KY";
            location.Latitude = 38.2157f;
            location.Longitude = -85.7590743f;
            location.MapIcon = icon;

            cardUser = new CardHubUser { UserName = user.Email, CreateDate = DateTime.Now, Location = location, FirstName = firstName, LastName = lastName};

            if (avatar != null && avatar.Trim() != String.Empty)
            {
                cardUser.Avatar = CardHubUserMaster.CreateMD5(avatar.ToLower());
            }

            db.CardHubUsers.Add(cardUser);
            db.Locations.Add(location);
            db.SaveChanges();
        }

        public static string CreateMD5(string input)
        {
            input = input.Trim().ToLower();
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static CardHubUser GetCurrentUser(CardinalHub.DAL.CardHubContext db)
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;

            CardHubUser user;

            //if (userName.Length > 0)
            //{
             //   CardHubUser u = db.CardHubUsers
             //                  .Where(user => user.UserName == userName) 
             //                  .Select(user => user).Single();            
              //  return u;
           // }
           // else
           // {
           //     return null;
            //}

            user = db.CardHubUsers.FirstOrDefault(CardHubUser => CardHubUser.UserName == userName);

            if (user == null)
            {

                user = new CardHubUser {  UserName = userName, CreateDate = DateTime.Now };
                db.CardHubUsers.Add(user);
                db.SaveChanges();
            }

            return user;
            
        }
    }
}