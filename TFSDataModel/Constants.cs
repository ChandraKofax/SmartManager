using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Constants
    {
        public static readonly TimeSpan TFSServerTimeDifferenceConstant = new TimeSpan(-5, -30, 0); //Server time difference of 5:30:00 hr
        public static readonly TimeSpan ClientTimeConstant = new TimeSpan(9, 0, 0);
        public static readonly Site[] Locations = new Site[]{
            new Site{ Name = "Hyderabad"},
            new Site{ Name = "Derry"}
            };

        public static readonly Sprint[] AvailableSprints = new Sprint[] {
            new Sprint{ Number = 0, IterationPath = "KTA\\Release 7.2\\Sprint 0", Name = "7.2 Sprint 0", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 1, IterationPath = "KTA\\Release 7.2\\Sprint 1", Name = "7.2 Sprint 1", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 2, IterationPath = "KTA\\Release 7.2\\Sprint 2", Name = "7.2 Sprint 2", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 3, IterationPath = "KTA\\Release 7.2\\Sprint 3", Name = "7.2 Sprint 3", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 4,  IterationPath = "KTA\\Release 7.2\\Sprint 4", Name = "7.2 Sprint 4", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 5,  IterationPath = "KTA\\Release 7.2\\Sprint 5", Name = "7.2 Sprint 5", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 6,  IterationPath = "KTA\\Release 7.2\\Sprint 6", Name = "7.2 Sprint 6", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 7, IterationPath = "KTA\\Release 7.2\\Sprint 7", Name = "7.2 Sprint 7", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 8, IterationPath = "KTA\\Release 7.2.1\\Sprint 1", Name = "7.2.1 Sprint 1", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 9, IterationPath = "KTA\\Release 7.2.1\\Release Sprint - Regression (Plus Feature Completion)", Name = "7.2.1 Regression", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
            new Sprint{ Number = 10, IterationPath = "KTA\\Release 7.2.1\\Release Sprint - RC Testing", Name = "7.2.1 RC Testing", Duration = new Duration(){ From = DateTime.Now, To= DateTime.Now}},
        };

        public static readonly Resource[] AllResources = new Resource[]{
            new Resource{ Name = "Srinivas Gajula", Capacity = 6, ExpectedVelocity = 6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{ Name = "Raghuthama Vemparala", Capacity = 0, ExpectedVelocity = 0, Level = ResourceLevel.Manager, Location = GetSite("Hyderabad")},
            new Resource{ Name = "Shravan Joopally", Capacity = 6, ExpectedVelocity = 6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{ Name = "Srikanth Penmetsa", Capacity = 6, ExpectedVelocity = 6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{ Name = "Kiran Jyothi", Capacity = 6, ExpectedVelocity = 6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{ Name = "Sandhya Reddy Gari", Capacity = 6, ExpectedVelocity = 6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{ Name = "Padma Dighe", Capacity = 6, ExpectedVelocity = 6, Level = ResourceLevel.Lead, Location = GetSite("Hyderabad")},
            new Resource{ Name = "Servani Basavani", Capacity = 6, ExpectedVelocity = 6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Sai Subrahmanya Prasad Dhulip", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Manager, Location = GetSite("Hyderabad")},
            new Resource{Name = "Ranadheer Pingli", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Nagarjuna Borra", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Mounika Vallabhaneni", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Sunanda Varma", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Amit Potdar", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Lead, Location = GetSite("Hyderabad")},
            new Resource{Name = "Sreevani Appikatla", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Prashant Kumar", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Rajesh Rekapalli", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Manager, Location = GetSite("Hyderabad")},
            new Resource{Name = "Shalini Reddy", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Phani Sastry", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Sangameshwar Reddy", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Lead, Location = GetSite("Hyderabad")},
            new Resource{Name = "Chandrashekar Machipeddi", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Rahnamol Sandeep", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Satya Karuturi", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Kalesha Shaik", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Srinivas Velidanda", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Manager, Location = GetSite("Hyderabad")},
            new Resource{Name = "Rama Subbarao Taduri", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Kranthi Kumar Reddy", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Naresh Kokkula", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Anjaneyulu Bondugula", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Chiranjeevi Vabalareddi", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Pallavi Singh", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Srinivas Nallapaneni", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Kiran Karka", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Rajini Govardhan", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Manager, Location = GetSite("Hyderabad")},
            new Resource{Name = "Sudhir Gudimella", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Manager, Location = GetSite("Hyderabad")},
            new Resource{Name = "Manasa Reddy", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Suushma Patlola", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Srikanth Chincholi", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Raghuveer Chennavajhala", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Rammohan Konduri", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Vinod Yendamury", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Nageswara Rao", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Swetha Kondoju", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Raviteja Peetla", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Anusha Raju", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Senior, Location = GetSite("Hyderabad")},
            new Resource{Name = "Sangeeta Garg", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Manager, Location = GetSite("Hyderabad")},
            new Resource{Name = "Preeti Gurram", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Mary Sumalatha", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Engineer, Location = GetSite("Hyderabad")},
            new Resource{Name = "Karri Jagadeeswara Reddy", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Lead, Location = GetSite("Hyderabad")},
            new Resource{Name = "Niraj Srivastava", Capacity = 6, ExpectedVelocity=6, Level = ResourceLevel.Lead, Location = GetSite("Hyderabad")},
        };

        public static readonly Team[] Teams = new Team[] {
            new Team{ Name = "Tiger", Manager = GetResource("Raghuthama Vemparala"), 
                TechSpecialist = new List<Resource>(){
                    GetResource("Raghuthama Vemparala"), 
                    GetResource("Amit Potdar")},
                    Members = new List<Resource>(){
                            GetResource("Raghuthama Vemparala"),
                            GetResource("Amit Potdar"),
                            GetResource("Srikanth Penmetsa"),
                            GetResource("Kiran Jyothi"),
                            GetResource("Sandhya Reddy Gari"), 
                            GetResource("Servani Basavani")
                }
            },
            new Team{ Name = "Cheetah", Manager = GetResource("Sai Subrahmanya Prasad Dhulip"), 
                TechSpecialist = new List<Resource>(){
                    GetResource("Sai Subrahmanya Prasad Dhulip"), 
                    GetResource("Sunanda Varma"),},
                Members = new List<Resource>(){
                            GetResource("Ranadheer Pingli"), 
                            GetResource("Nagarjuna Borra"),
                            GetResource("Mounika Vallabhaneni"),
                            GetResource("Sai Subrahmanya Prasad Dhulip"),
                            GetResource("Sunanda Varma"),
                            GetResource("Sreevani Appikatla"),
                            GetResource("Prashant Kumar")}
            },
            new Team{ Name = "Lion", Manager = GetResource("Rajesh Rekapalli"), 
                TechSpecialist = new List<Resource>(){
                    GetResource("Rajesh Rekapalli"), 
                    GetResource("Sangameshwar Reddy"),},
                Members = new List<Resource>(){
                            GetResource("Rajesh Rekapalli"), 
                            GetResource("Phani Sastry"),
                            GetResource("Sangameshwar Reddy"),
                            GetResource("Chandrashekar Machipeddi"),
                            GetResource("Rahnamol Sandeep"),
                            GetResource("Satya Karuturi"),
                            GetResource("Kalesha Shaik"),}
            },
            new Team{ Name = "Leopard", Manager = GetResource("Srinivas Velidanda"), 
                TechSpecialist = new List<Resource>(){
                    GetResource("Srinivas Velidanda"), 
                    GetResource("Chiranjeevi Vabalareddi"),},
                Members = new List<Resource>(){
                            GetResource("Srinivas Velidanda"),
                            GetResource("Rama Subbarao Taduri"),
                            GetResource("Kranthi Kumar Reddy"),
                            GetResource("Naresh Kokkula"),
                            GetResource("Chiranjeevi Vabalareddi"),
                            GetResource("Srinivas Nallapaneni"),
                            GetResource("Kiran Karka"),
                }
            },
            new Team{ Name = "QA", Manager = GetResource("Sudhir Gudimella"), 
                TechSpecialist = new List<Resource>(){
                    GetResource("Sudhir Gudimella"), 
                    GetResource("Manasa Reddy"),},
                Members = new List<Resource>(){
                            GetResource("Manasa Reddy"),
                            GetResource("Suushma Patlola"),
                            GetResource("Sudhir Gudimella"),
                            GetResource("Rammohan Konduri"),
                            GetResource("Vinod Yendamury"),
                            GetResource("Swetha Kondoju"),
                            GetResource("Raviteja Peetla"),
                            GetResource("Nageswara Rao"),
                            GetResource("Anusha Raju")
                             }
            },
            new Team{ Name = "TW", Manager = GetResource("Sangeeta Garg"), 
                TechSpecialist = new List<Resource>(){
                    GetResource("Sangeeta Garg")},
                Members = new List<Resource>(){
                            GetResource("Sangeeta Garg"), 
                            GetResource("Preeti Gurram"),
                            GetResource("Mary Sumalatha")}
            },
            new Team{ Name = "Arch", Manager = GetResource("Karri Jagadeeswara Reddy"), 
                TechSpecialist = new List<Resource>(){
                    GetResource("Karri Jagadeeswara Reddy"), 
                    GetResource("Niraj Srivastava"),},
                Members = new List<Resource>(){
                            GetResource("Karri Jagadeeswara Reddy"), 
                            GetResource("Niraj Srivastava"),
                            GetResource("Padma Dighe"), 
                }
            },
            new Team{ Name = "All Hyd", Manager = GetResource("Srinivas Gajula"), 
                TechSpecialist = new List<Resource>(){
                    GetResource("Srinivas Gajula")},
                Members = new List<Resource>(){
                            GetResource("Srinivas Gajula")}
            },
        };

        static Constants()
        {
            Locations[0].Manager = GetResource("Srinivas Gajula");
            Locations[0].Teams.Add(GetTeam("Tiger"));
            Locations[0].Teams.Add(GetTeam("Lion"));
            Locations[0].Teams.Add(GetTeam("Cheetah"));
            Locations[0].Teams.Add(GetTeam("Leopard"));
            Locations[0].Teams.Add(GetTeam("QA"));
            Locations[0].Teams.Add(GetTeam("TW"));
            Locations[0].Teams.Add(GetTeam("Arch"));
            Locations[1].Manager = GetResource("Mark Alexandar");
             Locations[1].Teams.Add(GetTeam("Tiger"));
             Locations[1].Teams.Add(GetTeam("Arch"));
        }

        public static Resource GetResource(string resourceName)
        {
            Resource resource = AllResources.FirstOrDefault(r => r.Name == resourceName);
            if (resource == null) { resource = new Resource { Name = "Unknown resource" }; }
            return resource;
        }
        public static Team GetTeam(string teamName)
        {
            Team team = Teams.FirstOrDefault(t => t.Name == teamName);
            if (team == null) { team = new Team { Name = "Unknown team" }; }
            return team;
        }
        public static Site GetSite(string siteName)
        {
            Site site = Locations.FirstOrDefault(s => s.Name == siteName);
            if (site == null) { site = new Site { Name = "Unknown" }; }
            return site;
        }
        public static bool IsTeamResource(string teamName, string resourceName)
        {
            Team team = Teams.FirstOrDefault(t => t.Name == teamName);
            if(team != null && team.Name != "Unknown team")
            {
                return team.Members.Count(r => r.Name == resourceName) > 0;
            }

            return false;
        }
    }
}
