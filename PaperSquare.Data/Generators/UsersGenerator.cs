using Bogus;
using PaperSquare.Data.Data;
using PaperSquare.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Data.Generators
{
    public class UsersGenerator
    {
        public static readonly UsersGenerator Instance = new UsersGenerator();
        public static UsersGenerator Generator { get { return Instance; } }

        public List<User> Users = new();
        private const int NUMBER_OF_USERS = 20;

        // Generates password for all users with value Password!1
        private const string PASSWORD_HASH = "AQAAAAEAACcQAAAAECKfk8fF5yZ4plu8y1vPtzMs/u8dlOOq0zuPKb1uKKDKRuxUFhSb2HUaBFLUEYe0EA==";
        private const string SECURITY_STAMP = "VJWEG644FKWZHWEQSDTECNTWRMOX3YFN";

        private UsersGenerator()
        {
            Users = InitUsersData();
        }

        private static Faker<User> GetUserGenerator()
        {
            return new Faker<User>()
                .RuleFor(u => u.Id, _ => Guid.NewGuid().ToString())
                .RuleFor(u => u.Firstname, u => u.Name.FirstName())
                .RuleFor(u => u.Lastname, u => u.Name.LastName())
                .RuleFor(u => u.UserName, (_,u) => u.Firstname + "_" + u.Lastname)
                .RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Firstname, u.Lastname))
                .RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper())
                .RuleFor(u => u.EmailConfirmed, f => f.Random.Bool())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.PhoneNumberConfirmed, f => f.Random.Bool())
                .RuleFor(u => u.BirthDate, f => f.Date.Between(new DateTime(1960, 1, 1), new DateTime(2003, 1, 1)))
                .RuleFor(u => u.IsDeleted, f => f.Random.Bool())
                .RuleFor(u => u.CreatedOnUtc, f => f.Date.Between(new DateTime(2010, 1, 1), new DateTime(2020, 1, 1)))
                .RuleFor(u => u.CreatedBy, (_, u) => u.Id)
                .RuleFor(u => u.LastModifiedBy, (_, u) => null)
                .RuleFor(u => u.PasswordHash, _ => PASSWORD_HASH)
                .RuleFor(u => u.SecurityStamp, _ => SECURITY_STAMP)
                .RuleFor(u => u.ConcurrencyStamp, _ => Guid.NewGuid().ToString());
        }

        public List<User> InitUsersData()
        {
            Users = GetUserGenerator().Generate(NUMBER_OF_USERS);

            return Users;
        }
    }
}
