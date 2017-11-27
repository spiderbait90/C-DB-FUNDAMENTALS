using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand
    {
        public static string Execute(string[] data)
        {
            var userName = data[1];
            var password = data[2];

            using (var db = new PhotoShareContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == userName && x.Password == password);

                if (user == null)
                    throw new ArgumentException("Invalid username or password!");

                if (Session.User != null)
                {
                    throw new ArgumentException("You should logout first!");
                }

                Session.User = user;

            }

            return $"User {userName} successfully logged in!";
        }
    }
}
