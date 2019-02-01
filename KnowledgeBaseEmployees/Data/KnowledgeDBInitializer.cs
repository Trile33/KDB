using KnowledgeBaseEmployees.Models;
using System.Linq;

namespace KnowledgeBaseEmployees.Data
{
    public static class KnowledgeDBInitializer
    {
        public static void Initialize(KnowledgeDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Projects.Any())
            {
                return;
            }

            var projects = new Project[]
            {
                new Project{Name="Dela", Code="DLA", Description="something"},
                new Project{Name="EVA", Code="EVA", Description="something else"},
                new Project{Name="NT2", Code="NT", Description="Dutch course"},
                new Project{Name="ONA", Code="ONA", Description="She."}
            };
            foreach (Project p in projects)
            {
                context.Projects.Add(p);
            }
            context.SaveChanges();

            var employees = new Employee[]
            {
                new Employee{FirstName="Mirko", LastName="Kopanja", Username="mirkorulet", Password="ruletRulet", IsTeamLead=false},
                new Employee{FirstName="Pego", LastName="Simpson", Username="pegokralj", Password="PegoKralj", IsTeamLead=true},
                new Employee{FirstName="Jon", LastName="Snow", Username="letitsnow", Password="targeryan", IsTeamLead=true},
                new Employee{FirstName="Simo", LastName="Crepulja", Username="bibercrep", Password="CrepuljaCrep", IsTeamLead=false}
            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();

            var technologies = new Technology[]
            {
                new Technology{Name=".NET Core 2", Information="info1"},
                new Technology{Name="Java", Information="info2"},
                new Technology{Name="Scala", Information="info3"},
                new Technology{Name="Angular 5", Information="info4"}
            };
            foreach (Technology t in technologies)
            {
                context.Technologies.Add(t);
            }
            context.SaveChanges();

            var technologyemployees = new TechnologyEmployee[]
            {
                new TechnologyEmployee{TechnologyId=1, EmployeeId=1},
                new TechnologyEmployee{TechnologyId=2, EmployeeId=2},
                new TechnologyEmployee{TechnologyId=3, EmployeeId=3},
                new TechnologyEmployee{TechnologyId=4, EmployeeId=4},
                new TechnologyEmployee{TechnologyId=3, EmployeeId=2},
                new TechnologyEmployee{TechnologyId=2, EmployeeId=3},
                new TechnologyEmployee{TechnologyId=1, EmployeeId=4}
            };
            foreach (TechnologyEmployee g in technologyemployees)
            {
                context.TechnologyEmployees.Add(g);
            }
            context.SaveChanges();

            var technologyprojects = new TechnologyProject[]
            {
                new TechnologyProject{ProjectId=1, TechnologyId=1},
                new TechnologyProject{ProjectId=2, TechnologyId=2},
                new TechnologyProject{ProjectId=3, TechnologyId=3},
                new TechnologyProject{ProjectId=4, TechnologyId=4},
                new TechnologyProject{ProjectId=3, TechnologyId=2},
                new TechnologyProject{ProjectId=2, TechnologyId=3},
                new TechnologyProject{ProjectId=1, TechnologyId=4}
            };
            foreach (TechnologyProject k in technologyprojects)
            {
                context.TechnolgyProjects.Add(k);
            }
            context.SaveChanges();

            var projcetemployees = new ProjectEmployee[]
            {
                new ProjectEmployee{ ProjectId=1, EmployeeId=1},
                new ProjectEmployee{ ProjectId=1, EmployeeId=2},
                new ProjectEmployee{ ProjectId=2, EmployeeId=3},
                new ProjectEmployee{ ProjectId=2, EmployeeId=4},
                new ProjectEmployee{ ProjectId=3, EmployeeId=1},
                new ProjectEmployee{ ProjectId=3, EmployeeId=2},
                new ProjectEmployee{ ProjectId=4, EmployeeId=3},
                new ProjectEmployee{ ProjectId=4, EmployeeId=4}
            };
            foreach (ProjectEmployee x in projcetemployees)
            {
                context.ProjectEmployees.Add(x);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User{ Id = 1, Password = "a", Username = "Admin", Token = "token" , Role = Role.Admin },

            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }
    }
}
