using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;


namespace DataBaseService
{
    internal class DataBaseSeed
    {
        public List<Speler> GetSeedDataBaseSpelers()
        {
            return new List<Speler>() {
                new Speler ( "Natalie",  "Velazquez",  DateTime.ParseExact("15/04/1993", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString()),
                new Speler ( "Oren",  "Yates",  DateTime.ParseExact("29/12/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
                new Speler ( "Ivy",  "Stanton",  DateTime.ParseExact("12/11/1991", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString()),
                new Speler ( "Hope",  "Dickerson",  DateTime.ParseExact("30/07/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
                 new Speler ( "Sonya",  "Boyle",  DateTime.ParseExact("24/10/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
             new Speler ( "Warren",  "Stark",  DateTime.ParseExact("06/04/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
                new Speler ( "Quon",  "Freeman",  DateTime.ParseExact("15/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
                new Speler ( "Justina",  "Warner",  DateTime.ParseExact("17/11/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
                new Speler ( "Odessa",  "Jenkins",  DateTime.ParseExact("28/08/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Rosalyn",  "Leon",  DateTime.ParseExact("10/05/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Rhoda",  "Klein",  DateTime.ParseExact("04/11/1997", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Isadora",  "Combs",  DateTime.ParseExact("21/04/1998", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Macy",  "Zimmerman",  DateTime.ParseExact("19/07/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Ulysses",  "Thomas",  DateTime.ParseExact("31/08/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Roth",  "Morrow",  DateTime.ParseExact("27/06/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Rafael",  "Manning",  DateTime.ParseExact("15/03/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Aaron",  "Wiley",  DateTime.ParseExact("08/06/1997", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Kelsey",  "Flores",  DateTime.ParseExact("17/10/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Garrison",  "Prince",  DateTime.ParseExact("11/08/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Charity",  "Rosario",  DateTime.ParseExact("19/09/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Adria",  "Wood",  DateTime.ParseExact("21/02/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Declan",  "Howell",  DateTime.ParseExact("14/07/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Indigo",  "Maxwell",  DateTime.ParseExact("25/09/1991", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Dale",  "Lewis",  DateTime.ParseExact("30/09/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Darryl",  "Harrison",  DateTime.ParseExact("16/01/1991", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Rafael",  "Calhoun",  DateTime.ParseExact("28/12/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Adele",  "Suarez",  DateTime.ParseExact("22/03/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Caesar",  "Keith",  DateTime.ParseExact("03/01/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Benedict",  "Sykes",  DateTime.ParseExact("10/03/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Zachery",  "Mcconnell",  DateTime.ParseExact("01/09/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Patricia",  "Small",  DateTime.ParseExact("17/08/1998", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Orson",  "Mcfarland",  DateTime.ParseExact("02/03/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Brian",  "Kerr",  DateTime.ParseExact("04/12/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Russell",  "Baird",  DateTime.ParseExact("10/02/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Igor",  "Pollard",  DateTime.ParseExact("12/05/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Octavius",  "Fletcher",  DateTime.ParseExact("27/09/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Skyler",  "Walton",  DateTime.ParseExact("23/02/1998", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Ian",  "Mayer",  DateTime.ParseExact("04/12/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Kaye",  "Knapp",  DateTime.ParseExact("17/02/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Astra",  "Wilkinson",  DateTime.ParseExact("22/10/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Aladdin",  "Yates",  DateTime.ParseExact("04/07/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Sarah",  "Kennedy",  DateTime.ParseExact("29/12/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Kim",  "Hoover",  DateTime.ParseExact("30/06/1991", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Hadley",  "Graham",  DateTime.ParseExact("16/03/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Harper",  "Morrison",  DateTime.ParseExact("01/05/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Adena",  "Walls",  DateTime.ParseExact("22/12/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Ira",  "Mack",  DateTime.ParseExact("05/07/1993", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Harriet",  "House",  DateTime.ParseExact("31/08/1992", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Maite",  "Rice",  DateTime.ParseExact("06/02/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Jack",  "Navarro",  DateTime.ParseExact("22/08/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Man.ToString() ),
    new Speler ( "Devin",  "Davenport",  DateTime.ParseExact("17/01/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Isabella",  "Oneil",  DateTime.ParseExact("30/11/1997", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Jenna",  "Strong",  DateTime.ParseExact("15/07/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Kennan",  "Hutchinson",  DateTime.ParseExact("24/09/1997", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Anjolie",  "Slater",  DateTime.ParseExact("04/06/1992", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Baker",  "Jennings",  DateTime.ParseExact("31/01/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Ifeoma",  "Lynch",  DateTime.ParseExact("16/04/1991", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Eve",  "Roman",  DateTime.ParseExact("01/07/1993", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Jamal",  "Hancock",  DateTime.ParseExact("31/01/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Tatiana",  "Cervantes",  DateTime.ParseExact("01/04/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Violet",  "Munoz",  DateTime.ParseExact("09/09/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Anjolie",  "Conner",  DateTime.ParseExact("23/08/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Charlotte",  "Frederick",  DateTime.ParseExact("06/09/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Dennis",  "Yates",  DateTime.ParseExact("06/05/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Kyla",  "Suarez",  DateTime.ParseExact("10/05/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Andrew",  "Espinoza",  DateTime.ParseExact("22/08/1993", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Guinevere",  "Calhoun",  DateTime.ParseExact("28/04/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Arden",  "Rose",  DateTime.ParseExact("01/02/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Jacob",  "Randolph",  DateTime.ParseExact("27/07/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Nayda",  "Martinez",  DateTime.ParseExact("29/05/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Kellie",  "Valentine",  DateTime.ParseExact("25/03/1993", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Athena",  "Nunez",  DateTime.ParseExact("30/09/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Bryar",  "Brock",  DateTime.ParseExact("21/09/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Lynn",  "Whitley",  DateTime.ParseExact("02/01/1992", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Raven",  "England",  DateTime.ParseExact("18/06/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Thaddeus",  "Garza",  DateTime.ParseExact("13/01/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Sharon",  "Brooks",  DateTime.ParseExact("15/06/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Rylee",  "Mckee",  DateTime.ParseExact("13/10/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Tanner",  "House",  DateTime.ParseExact("29/07/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Aurelia",  "Lang",  DateTime.ParseExact("19/10/1998", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Chava",  "Nash",  DateTime.ParseExact("10/09/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Perry",  "Wilkins",  DateTime.ParseExact("15/05/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Flynn",  "Mcpherson",  DateTime.ParseExact("05/05/1997", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Yasir",  "Ortiz",  DateTime.ParseExact("14/03/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Aurelia",  "Humphrey",  DateTime.ParseExact("21/01/1993", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Maisie",  "Carr",  DateTime.ParseExact("22/03/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Ila",  "Tillman",  DateTime.ParseExact("23/01/1992", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Moses",  "England",  DateTime.ParseExact("03/09/1991", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Vielka",  "Mcclain",  DateTime.ParseExact("30/04/1991", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Dolan",  "Soto",  DateTime.ParseExact("05/04/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Lev",  "Holt",  DateTime.ParseExact("10/10/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Rafael",  "Owen",  DateTime.ParseExact("10/08/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Jemima",  "Perry",  DateTime.ParseExact("06/04/1998", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Diana",  "Landry",  DateTime.ParseExact("13/05/1995", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Nehru",  "Haynes",  DateTime.ParseExact("27/12/1993", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Vera",  "Atkinson",  DateTime.ParseExact("12/10/1997", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Germane",  "Sutton",  DateTime.ParseExact("19/05/1993", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Chava",  "Daniel",  DateTime.ParseExact("13/05/1997", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Cynthia",  "Anderson",  DateTime.ParseExact("28/04/1994", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() ),
    new Speler ( "Wynter",  "Pierce",  DateTime.ParseExact("25/07/1997", "dd/MM/yyyy", CultureInfo.InvariantCulture),  Misc.GeslachtEnum.Vrouw.ToString() )
};


        }



        public List<VoetbalTeam> GetSeedDataBaseTeams()
        {
            return new List<VoetbalTeam>() {
                new VoetbalTeam ("A4", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("A1", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("A2", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("A3", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("H4", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("D4", Misc.GeslachtEnum.Vrouw),
                new VoetbalTeam ("A1", Misc.GeslachtEnum.Vrouw),
                new VoetbalTeam ("A2", Misc.GeslachtEnum.Vrouw),
                new VoetbalTeam ("A3", Misc.GeslachtEnum.Vrouw),
                new VoetbalTeam ("A4", Misc.GeslachtEnum.Vrouw),
                new VoetbalTeam ("B1", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("B2", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("AC Milan", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("AZ", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("Ajax", Misc.GeslachtEnum.Man),
                new VoetbalTeam ("Nederland", Misc.GeslachtEnum.Vrouw),
                new VoetbalTeam ("Nederland", Misc.GeslachtEnum.Man)

            };
            // return teams;
        }
    }
}
