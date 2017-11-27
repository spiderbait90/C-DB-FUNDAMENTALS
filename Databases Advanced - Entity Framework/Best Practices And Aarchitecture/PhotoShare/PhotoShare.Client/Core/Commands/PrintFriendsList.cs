using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class PrintFriendsListCommand
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data)
        {
            var str = new StringBuilder();

            using (var db = new PhotoShareContext())
            {
                var user = db.Users
                    .AsNoTracking()
                    .Include(x=>x.FriendsAdded)
                    .ThenInclude(x=>x.Friend)
                    .FirstOrDefault(x => x.Username == data[1]);

                if (user == null)
                {
                    throw new ArgumentException($"User {data[1]} not found !");
                }

                if (!user.FriendsAdded.Any())
                {
                    throw new ArgumentException($"No friends for this user. :(");
                }

                str.AppendLine("Friends:");

                foreach (var f in user.FriendsAdded)
                {
                    str.AppendLine($"-[{f.Friend.Username}]");
                }
            }

            return str.ToString();
        }
    }
}
