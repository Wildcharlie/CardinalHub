using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CardinalHub.Models;
namespace CardinalHub.DAL
{
    public class CardHubInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CardHubContext>
    {
        protected override void Seed(CardHubContext context)
        {
            var users = new List<CardHubUser>
            {
            new CardHubUser{FirstName="Carson",LastName="Alexander", UserName="atjevt01@louisville.edu",CreateDate=DateTime.Parse("2005-09-01")},
            new CardHubUser{FirstName="Meredith",LastName="Alonso",UserName="qwe@qwe.qwe",CreateDate=DateTime.Parse("2002-09-01")},
            new CardHubUser{FirstName="Arturo",LastName="Anand",UserName="qwe1@qwe.qwe",CreateDate=DateTime.Parse("2003-09-01")},
            new CardHubUser{FirstName="Gytis",LastName="Barzdukas",UserName="qwe2@qwe.qwe",CreateDate=DateTime.Parse("2002-09-01")},
            new CardHubUser{FirstName="Yan",LastName="Li",UserName="qwe3@qwe.qwe",CreateDate=DateTime.Parse("2002-09-01")},
            new CardHubUser{FirstName="Peggy",LastName="Justice",UserName="qwe4@qwe.qwe",CreateDate=DateTime.Parse("2001-09-01")},
            new CardHubUser{FirstName="Laura",LastName="Norman",UserName="qwe5@qwe.qwe",CreateDate=DateTime.Parse("2003-09-01")},
            new CardHubUser{FirstName="Nino",LastName="Olivetto",UserName="qwe6@qwe.qwe",CreateDate=DateTime.Parse("2005-09-01")}
            };

            users.ForEach(s => context.CardHubUsers.Add(s));
            context.SaveChanges();
        }

    }
}