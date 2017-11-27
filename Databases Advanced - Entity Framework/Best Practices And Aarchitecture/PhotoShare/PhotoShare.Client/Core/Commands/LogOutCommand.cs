using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class LogOutCommand
    {
        public static string Execute(string[] data)
        {
            if (Session.User == null)
                throw new InvalidOperationException("You should log in first in order to logout.");

            var name = Session.User.Username;

            Session.User = null;

            return $"User {name} successfully logged out!";
        }
    }
}
