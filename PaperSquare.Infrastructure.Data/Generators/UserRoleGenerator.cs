using Bogus;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Data.Generators
{
    public class UserRoleGenerator
    {
        private static readonly UserRoleGenerator Instance = new UserRoleGenerator();

        public static UserRoleGenerator Generator { get { return Instance; } }

        public List<UserRole> UserRoles = new ();

        private UserRoleGenerator()
        {
            UserRoles = InitUserRolesData();
        }

        private static Faker<UserRole> GetUserRolesGenerator(string userId)
        {
            return new Faker<UserRole>()
                .RuleFor(ur => ur.UserId, _ => userId)
                .RuleFor(ur => ur.RoleId, (f, _) =>
                {
                    return f.PickRandom(RolesGenerator.Generator.Roles).Id;
                });
        }

        private List<UserRole> InitUserRolesData() 
        {
            foreach (var user in UsersGenerator.Generator.Users)
            {
                UserRoles.Add(GetUserRolesGenerator(user.Id).Generate());
            }            
                        
            return UserRoles;
        }
    }
}
