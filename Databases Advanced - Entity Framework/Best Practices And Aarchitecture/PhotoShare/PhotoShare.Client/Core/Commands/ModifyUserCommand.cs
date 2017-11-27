using System.Linq;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var db = new PhotoShareContext();
            var userName = data[1];
            var property = data[2].ToLower();
            var value = data[3];

            var user = db.Users
                .FirstOrDefault(x => x.Username == userName);

            if (user == null)
                throw new ArgumentException($"User {userName} not found");

            if (property != "password" && property != "borntown" && property != "currenttown")
                throw new ArgumentException($"Property {property} not supported!");

            if (property == "password")
            {
                if (!value.Any(char.IsLower) ||
                    !value.Any(char.IsNumber))
                    throw new ArgumentException($"Invalid Password");

                user.Password = value;
                db.SaveChanges();
            }

            else if (property == "borntown")
            {
                var town = db.Towns.FirstOrDefault(x => x.Name.ToLower() == value.ToLower());

                if (town == null)
                    throw new ArgumentException($"Town {value} not found");

                user.BornTown = town;
                db.SaveChanges();
            }

            else
            {
                var town = db.Towns.FirstOrDefault(x => x.Name.ToLower() == value.ToLower());

                if (town == null)
                    throw new ArgumentException($"Town {value} not found");

                user.CurrentTown = town;
                db.SaveChanges();
            }
            db.Dispose();

            return $"User {userName} {property} is {value}";
        }
    }
}
