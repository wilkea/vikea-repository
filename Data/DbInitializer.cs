using dpcleague_2.Models;
using Microsoft.EntityFrameworkCore;

namespace dpcleague_2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(dpcContext context)
        {
            context.Database.EnsureCreated();

            if (context.Organizaties.Any())
            {
                return;   // DB has been seeded
            }

            // Add Organizations from SQL dump
            var organizations = new[]
            {
                new Organizatie { Denumire = "Team Secret", DataCrearii = new DateTime(2014, 8, 21), Originea = "Europa" },
                new Organizatie { Denumire = "Evil Geniuses", DataCrearii = new DateTime(1999, 11, 24), Originea = "America de Nord" },
                new Organizatie { Denumire = "PSG.LGD", DataCrearii = new DateTime(2009, 5, 20), Originea = "China" },
                new Organizatie { Denumire = "OG", DataCrearii = new DateTime(2015, 10, 31), Originea = "Europa" },
                new Organizatie { Denumire = "Tundra Esports", DataCrearii = new DateTime(2019, 9, 3), Originea = "Europa" },
                new Organizatie { Denumire = "Stas Esport", DataCrearii = new DateTime(2024, 10, 30), Originea = "Briceni" },
                new Organizatie { Denumire = "Team Spirit", DataCrearii = new DateTime(2015, 5, 14), Originea = "Eastern Europe" },
                new Organizatie { Denumire = "Gaimin Gladiators", DataCrearii = new DateTime(2021, 1, 1), Originea = "Western Europe" },
                new Organizatie { Denumire = "BetBoom", DataCrearii = new DateTime(2021, 3, 15), Originea = "Eastern Europe" },
                new Organizatie { Denumire = "9Pandas", DataCrearii = new DateTime(2023, 1, 10), Originea = "Eastern Europe" }
            };
            context.Organizaties.AddRange(organizations);
            context.SaveChanges();

            // Add Rosters from SQL dump
            var rosters = new[]
            {
                new Roster { OrganizatieId = organizations[0].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2014, 8, 21) },
                new Roster { OrganizatieId = organizations[1].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2023, 1, 1) },
                new Roster { OrganizatieId = organizations[2].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2009, 5, 20) },
                new Roster { OrganizatieId = organizations[3].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2018, 8, 25) },
                new Roster { OrganizatieId = organizations[4].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2021, 9, 3) },
                new Roster { OrganizatieId = organizations[5].OrganizatieId, Disciplina = "Ghensin", DataFormare = new DateTime(2024, 10, 29) },
                new Roster { OrganizatieId = organizations[6].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2023, 9, 1) },
                new Roster { OrganizatieId = organizations[7].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2023, 9, 1) },
                new Roster { OrganizatieId = organizations[8].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2023, 9, 1) },
                new Roster { OrganizatieId = organizations[9].OrganizatieId, Disciplina = "Dota 2", DataFormare = new DateTime(2023, 9, 1) }
            };
            context.Rosters.AddRange(rosters);
            context.SaveChanges();

            // Add Players from SQL dump and new players for additional teams
            var players = new[]
            {
                // Team Secret Players (from SQL dump)
                new Sportiv { Nume = "Clement", Prenume = "Ivanov", DataNasterii = new DateTime(1990, 3, 6), OrganizatieId = organizations[0].OrganizatieId, Porecla = "Puppey" },
                new Sportiv { Nume = "Michal", Prenume = "Jankowski", DataNasterii = new DateTime(1995, 6, 4), OrganizatieId = organizations[0].OrganizatieId, Porecla = "Nisha" },
                new Sportiv { Nume = "William", Prenume = "Morales", DataNasterii = new DateTime(1997, 1, 9), OrganizatieId = organizations[0].OrganizatieId, Porecla = "LeBron" },
                new Sportiv { Nume = "Kuro", Prenume = "Salehi Takhasomi", DataNasterii = new DateTime(1996, 1, 28), OrganizatieId = organizations[0].OrganizatieId, Porecla = "KuroKy" },
                new Sportiv { Nume = "Aydin", Prenume = "Sahin", DataNasterii = new DateTime(1998, 9, 15), OrganizatieId = organizations[0].OrganizatieId, Porecla = "iXmike" },

                // Evil Geniuses Players (from SQL dump)
                new Sportiv { Nume = "Artour", Prenume = "Babaev", DataNasterii = new DateTime(1996, 7, 1), OrganizatieId = organizations[1].OrganizatieId, Porecla = "Arteezy" },
                new Sportiv { Nume = "Andreas", Prenume = "Nielsen", DataNasterii = new DateTime(1991, 7, 6), OrganizatieId = organizations[1].OrganizatieId, Porecla = "Cr1t-" },
                new Sportiv { Nume = "Zachary", Prenume = "Pauley", DataNasterii = new DateTime(1999, 10, 20), OrganizatieId = organizations[1].OrganizatieId, Porecla = "S4" },
                new Sportiv { Nume = "Sumail", Prenume = "Hassan", DataNasterii = new DateTime(1999, 9, 13), OrganizatieId = organizations[1].OrganizatieId, Porecla = "SumaiL" },
                new Sportiv { Nume = "Jason", Prenume = "Kwan", DataNasterii = new DateTime(1997, 12, 1), OrganizatieId = organizations[1].OrganizatieId, Porecla = "Kpii" },

                // Continue with all players from SQL dump...
                // Lines 208-234 from dpc.sql

                // Team Spirit Players (new)
                new Sportiv { Nume = "Illya", Prenume = "Mulyarchuk", DataNasterii = new DateTime(2003, 6, 13), OrganizatieId = organizations[6].OrganizatieId, Porecla = "Yatoro" },
                new Sportiv { Nume = "Denis", Prenume = "Sigitov", DataNasterii = new DateTime(2002, 3, 15), OrganizatieId = organizations[6].OrganizatieId, Porecla = "Larl" },
                new Sportiv { Nume = "Magomed", Prenume = "Khalilov", DataNasterii = new DateTime(2002, 8, 20), OrganizatieId = organizations[6].OrganizatieId, Porecla = "Collapse" },
                new Sportiv { Nume = "Miroslaw", Prenume = "Kolpakov", DataNasterii = new DateTime(2000, 4, 4), OrganizatieId = organizations[6].OrganizatieId, Porecla = "Mira" },
                new Sportiv { Nume = "Yaroslav", Prenume = "Naidenov", DataNasterii = new DateTime(1997, 9, 30), OrganizatieId = organizations[6].OrganizatieId, Porecla = "Miposhka" },

                // Gaimin Gladiators Players
                new Sportiv { Nume = "Anton", Prenume = "Shkredov", DataNasterii = new DateTime(2001, 5, 17), OrganizatieId = organizations[7].OrganizatieId, Porecla = "dyrachyo" },
                new Sportiv { Nume = "Quinn", Prenume = "Callahan", DataNasterii = new DateTime(1996, 7, 12), OrganizatieId = organizations[7].OrganizatieId, Porecla = "Quinn" },
                new Sportiv { Nume = "Marcus", Prenume = "Hoelgaard", DataNasterii = new DateTime(1994, 1, 19), OrganizatieId = organizations[7].OrganizatieId, Porecla = "Ace" },
                new Sportiv { Nume = "Erik", Prenume = "Engel", DataNasterii = new DateTime(2000, 3, 9), OrganizatieId = organizations[7].OrganizatieId, Porecla = "tOfu" },
                new Sportiv { Nume = "Melchior", Prenume = "Hillenkamp", DataNasterii = new DateTime(1998, 8, 11), OrganizatieId = organizations[7].OrganizatieId, Porecla = "Seleri" },

                // BetBoom Players
                new Sportiv { Nume = "Danil", Prenume = "Dendi", DataNasterii = new DateTime(1989, 12, 30), OrganizatieId = organizations[8].OrganizatieId, Porecla = "Dendi" },
                new Sportiv { Nume = "Egor", Prenume = "Grigorenko", DataNasterii = new DateTime(1995, 3, 21), OrganizatieId = organizations[8].OrganizatieId, Porecla = "Nightfall" },
                new Sportiv { Nume = "Alexander", Prenume = "Khertek", DataNasterii = new DateTime(2000, 8, 15), OrganizatieId = organizations[8].OrganizatieId, Porecla = "TORONTOTOKYO" },
                new Sportiv { Nume = "Vitalie", Prenume = "Melnic", DataNasterii = new DateTime(1999, 6, 25), OrganizatieId = organizations[8].OrganizatieId, Porecla = "Save-" },
                new Sportiv { Nume = "Akbar", Prenume = "Butaev", DataNasterii = new DateTime(1997, 4, 12), OrganizatieId = organizations[8].OrganizatieId, Porecla = "SoNNeikO" },

                // 9Pandas Players
                new Sportiv { Nume = "Ivan", Prenume = "Moskalenko", DataNasterii = new DateTime(2002, 11, 5), OrganizatieId = organizations[9].OrganizatieId, Porecla = "Pure" },
                new Sportiv { Nume = "Artem", Prenume = "Barshack", DataNasterii = new DateTime(2001, 2, 14), OrganizatieId = organizations[9].OrganizatieId, Porecla = "Yuragi" },
                new Sportiv { Nume = "Vladislav", Prenume = "Morozyuk", DataNasterii = new DateTime(2000, 7, 23), OrganizatieId = organizations[9].OrganizatieId, Porecla = "Gpk" },
                new Sportiv { Nume = "Dmitry", Prenume = "Dorohin", DataNasterii = new DateTime(1998, 9, 19), OrganizatieId = organizations[9].OrganizatieId, Porecla = "DM" },
                new Sportiv { Nume = "Vladimir", Prenume = "Minenko", DataNasterii = new DateTime(1997, 11, 8), OrganizatieId = organizations[9].OrganizatieId, Porecla = "RodjER" },
            // ... existing players above ...

// PSG.LGD Players
new Sportiv { Nume = "Wang", Prenume = "Chun Yu", DataNasterii = new DateTime(1995, 7, 15), OrganizatieId = organizations[2].OrganizatieId, Porecla = "Ame" },
new Sportiv { Nume = "Zhao", Prenume = "Zixing", DataNasterii = new DateTime(1998, 4, 22), OrganizatieId = organizations[2].OrganizatieId, Porecla = "XinQ" },
new Sportiv { Nume = "Cheng", Prenume = "Jin Xiang", DataNasterii = new DateTime(1997, 9, 11), OrganizatieId = organizations[2].OrganizatieId, Porecla = "NothingToSay" },
new Sportiv { Nume = "Zhang", Prenume = "Ruida", DataNasterii = new DateTime(1999, 3, 28), OrganizatieId = organizations[2].OrganizatieId, Porecla = "Faith_bian" },
new Sportiv { Nume = "Zhang", Prenume = "Yiping", DataNasterii = new DateTime(1998, 12, 5), OrganizatieId = organizations[2].OrganizatieId, Porecla = "y`" },

// OG Players
new Sportiv { Nume = "Topias", Prenume = "Taavitsainen", DataNasterii = new DateTime(2002, 4, 14), OrganizatieId = organizations[3].OrganizatieId, Porecla = "Topson" },
new Sportiv { Nume = "Sébastien", Prenume = "Debs", DataNasterii = new DateTime(1992, 6, 12), OrganizatieId = organizations[3].OrganizatieId, Porecla = "Ceb" },
new Sportiv { Nume = "Johan", Prenume = "Sundstein", DataNasterii = new DateTime(1993, 10, 8), OrganizatieId = organizations[3].OrganizatieId, Porecla = "N0tail" },
new Sportiv { Nume = "Jesse", Prenume = "Vainikka", DataNasterii = new DateTime(1992, 8, 3), OrganizatieId = organizations[3].OrganizatieId, Porecla = "JerAx" },
new Sportiv { Nume = "Anathan", Prenume = "Pham", DataNasterii = new DateTime(2000, 10, 26), OrganizatieId = organizations[3].OrganizatieId, Porecla = "ana" },

// Tundra Esports Players
new Sportiv { Nume = "Oliver", Prenume = "Lepko", DataNasterii = new DateTime(2000, 11, 11), OrganizatieId = organizations[4].OrganizatieId, Porecla = "skiter" },
new Sportiv { Nume = "Leon", Prenume = "Kirlin", DataNasterii = new DateTime(2000, 2, 26), OrganizatieId = organizations[4].OrganizatieId, Porecla = "Nine" },
new Sportiv { Nume = "Neta", Prenume = "Shapira", DataNasterii = new DateTime(1996, 5, 15), OrganizatieId = organizations[4].OrganizatieId, Porecla = "33" },
new Sportiv { Nume = "Martin", Prenume = "Sazdov", DataNasterii = new DateTime(2001, 4, 23), OrganizatieId = organizations[4].OrganizatieId, Porecla = "Saksa" },
new Sportiv { Nume = "Wu", Prenume = "Jingjun", DataNasterii = new DateTime(1995, 7, 19), OrganizatieId = organizations[4].OrganizatieId, Porecla = "Sneyking" },

// Stas Esport Players (fictional team)
new Sportiv { Nume = "Liviu", Prenume = "Rusu", DataNasterii = new DateTime(2005,8, 15), OrganizatieId = organizations[5].OrganizatieId, Porecla = "Kroshka" },
new Sportiv { Nume = "Liviu", Prenume = "Vizitiu", DataNasterii = new DateTime(2005, 3, 22), OrganizatieId = organizations[5].OrganizatieId, Porecla = "liviuvzt" },
new Sportiv { Nume = "Victor", Prenume = "Turea", DataNasterii = new DateTime(2005, 11, 30), OrganizatieId = organizations[5].OrganizatieId, Porecla = "wilkeaa" },
new Sportiv { Nume = "Bogdan", Prenume = "Cozmolici", DataNasterii = new DateTime(2005, 5, 17), OrganizatieId = organizations[5].OrganizatieId, Porecla = "flipflop" },
new Sportiv { Nume = "Marcu", Prenume = "Enache", DataNasterii = new DateTime(2005, 1, 8), OrganizatieId = organizations[5].OrganizatieId, Porecla = "C5" },

// ... existing players below ...
            };

            context.Sportivs.AddRange(players);
            context.SaveChanges();

            // Add Events
            var events = new[]
            {
                new Eveniment 
                { 
                    Denumire = "The International 2024",
                    Disciplina = "Dota 2",
                    DataInceput = new DateTime(2024, 10, 12),
                    Locatia = "Seattle, USA"
                },
                new Eveniment 
                { 
                    Denumire = "ESL One Berlin 2024",
                    Disciplina = "Dota 2",
                    DataInceput = new DateTime(2024, 4, 26),
                    Locatia = "Berlin, Germany"
                },
                new Eveniment 
                { 
                    Denumire = "DreamLeague Season 22",
                    Disciplina = "Dota 2",
                    DataInceput = new DateTime(2025, 6, 15),
                    Locatia = "Stockholm, Sweden"
                },
                new Eveniment 
                { 
                    Denumire = "Lima Major 2034",
                    Disciplina = "Dota 2",
                    DataInceput = new DateTime(2034, 7, 20),
                    Locatia = "Lima, Peru"
                },
                new Eveniment 
                { 
                    Denumire = "Riyadh Masters 2024",
                    Disciplina = "Dota 2",
                    DataInceput = new DateTime(2024, 8, 5),
                    Locatia = "Riyadh, Saudi Arabia"
                }
            };

            context.Eveniments.AddRange(events);
            context.SaveChanges();

            // Assign 4 rosters to each event
            var rosterAssignments = new List<RosterEveniment>();

            foreach (var evt in events)
            {
                // Get 4 random rosters for each event
                var selectedRosters = rosters
                    .Where(r => r.Disciplina == "Dota 2")
                    .OrderBy(x => Guid.NewGuid())
                    .Take(4);

                foreach (var roster in selectedRosters)
                {
                    rosterAssignments.Add(new RosterEveniment
                    {
                        EvenimentId = evt.EvenimentId,
                        RosterId = roster.RosterId
                    });
                }
            }

            context.RosterEveniments.AddRange(rosterAssignments);
            context.SaveChanges();

            // Rest of the code remains the same as in your SQL dump
            // (Events, RosterEveniments, and Tickets)
        if (!context.Bilets.Any())
{
    var bilete = new Bilet[]
    {
        // Bilete pentru The International 2024
        new Bilet { 
            Nume = "Popescu",
            Prenume = "Alexandru",
            DataProcurarii = DateTime.Parse("2024-09-15"),
            EvenimentId = 1
        },
        new Bilet { 
            Nume = "Ionescu",
            Prenume = "Maria",
            DataProcurarii = DateTime.Parse("2024-09-15"),
            EvenimentId = 1
        },
        new Bilet { 
            Nume = "Georgescu",
            Prenume = "Ioan",
            DataProcurarii = DateTime.Parse("2024-09-16"),
            EvenimentId = 1
        },
        new Bilet { 
            Nume = "Popa",
            Prenume = "Elena",
            DataProcurarii = DateTime.Parse("2024-09-16"),
            EvenimentId = 1
        },
        new Bilet { 
            Nume = "Dumitrescu",
            Prenume = "Andrei",
            DataProcurarii = DateTime.Parse("2024-09-17"),
            EvenimentId = 1
        },

        // Bilete pentru ESL One Stockholm 2024
        new Bilet { 
            Nume = "Andersson",
            Prenume = "Erik",
            DataProcurarii = DateTime.Parse("2024-05-10"),
            EvenimentId = 2
        },
        new Bilet { 
            Nume = "Nilsson",
            Prenume = "Sofia",
            DataProcurarii = DateTime.Parse("2024-05-10"),
            EvenimentId = 2
        },
        new Bilet { 
            Nume = "Karlsson",
            Prenume = "Lars",
            DataProcurarii = DateTime.Parse("2024-05-11"),
            EvenimentId = 2
        },
        new Bilet { 
            Nume = "Larsson",
            Prenume = "Anna",
            DataProcurarii = DateTime.Parse("2024-05-11"),
            EvenimentId = 2
        },
        new Bilet { 
            Nume = "Olsson",
            Prenume = "Magnus",
            DataProcurarii = DateTime.Parse("2024-05-12"),
            EvenimentId = 2
        },

        // Bilete pentru DreamLeague Season 22
        new Bilet { 
            Nume = "Johnson",
            Prenume = "Michael",
            DataProcurarii = DateTime.Parse("2024-07-20"),
            EvenimentId = 3
        },
        new Bilet { 
            Nume = "Smith",
            Prenume = "Emma",
            DataProcurarii = DateTime.Parse("2024-07-20"),
            EvenimentId = 3
        },
        new Bilet { 
            Nume = "Williams",
            Prenume = "James",
            DataProcurarii = DateTime.Parse("2024-07-21"),
            EvenimentId = 3
        },
        new Bilet { 
            Nume = "Brown",
            Prenume = "Sarah",
            DataProcurarii = DateTime.Parse("2024-07-21"),
            EvenimentId = 3
        },
        new Bilet { 
            Nume = "Taylor",
            Prenume = "David",
            DataProcurarii = DateTime.Parse("2024-07-22"),
            EvenimentId = 3
        }
    };

    context.Bilets.AddRange(bilete);
    context.SaveChanges();
}}
        
    }
} 