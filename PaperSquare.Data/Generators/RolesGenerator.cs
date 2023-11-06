using Bogus;
using PaperSquare.Core.Permissions;
using PaperSquare.Core.Domain.Entities.Identity;

namespace PaperSquare.Data.Generators
{
    public class RolesGenerator
    {
        private static readonly RolesGenerator Instance = new RolesGenerator();
        public static RolesGenerator Generator { get { return Instance; } }

        public List<Role> Roles = new();

        private RolesGenerator()
        {
            Roles = InitRolesData();
        }

        private static Faker<Role> GetRoleGenerator(string name)
        {
            return new Faker<Role>()
                .RuleFor(r => r.Id, _ => Guid.NewGuid().ToString())
                .RuleFor(r => r.Name, _ => name)
                .RuleFor(r => r.NormalizedName, _ => name.ToUpper())
                .RuleFor(r => r.ConcurrencyStamp, _ => Guid.NewGuid().ToString());
        }

        private List<Role> InitRolesData()
        {
            Roles = new List<Role>()
            {
                GetRoleGenerator(AppRoles.ADMIN).Generate(),
                GetRoleGenerator(AppRoles.REGISTERED_USER).Generate(),
                GetRoleGenerator(AppRoles.EDITOR).Generate(),
                GetRoleGenerator(AppRoles.GUEST).Generate()
            };

            return Roles;
        }
    }
}
