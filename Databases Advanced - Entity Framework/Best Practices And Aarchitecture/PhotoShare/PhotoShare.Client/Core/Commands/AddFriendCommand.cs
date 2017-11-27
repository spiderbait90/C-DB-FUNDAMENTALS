using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var name1 = data[1];
            var name2 = data[2];

            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            using (var db = new PhotoShareContext())
            {
                var username1 = db.Users
                    .Include(x => x.FriendsAdded)
                    .Include(x => x.AddedAsFriendBy)
                    .FirstOrDefault(x => x.Username == name1);


                var username2 = db.Users
                    .Include(x => x.FriendsAdded)
                    .Include(x => x.AddedAsFriendBy)
                    .FirstOrDefault(x => x.Username == name2);

                if (username1 == null)
                {
                    throw new ArgumentException($"{name1} not found!");
                }
                if (username2 == null)
                {
                    throw new ArgumentException($"{name2} not found!");
                }

                if (username2.FriendsAdded.Any(x => x.FriendId == username1.Id) || username1.FriendsAdded.Any(x => x.FriendId == username2.Id))
                {
                    throw new InvalidOperationException($"{name2} is already a friend to {name1}");
                }

                    username1.FriendsAdded.Add(new Friendship()
                    {
                        User = username1,
                        Friend = username2
                    });

                db.SaveChanges();
            }

            return $"Friend {name2} added to {name1}";
        }
    }
}
