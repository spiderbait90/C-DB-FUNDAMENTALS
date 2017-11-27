using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using Utilities;

    public class AddTagCommand
    {
        // AddTag <tag>
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string tag = data[1].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Tags.Any(x => x.Name == tag))
                    throw new ArgumentException($"Tag {tag} exists!");

                context.Tags.Add(new Tag
                {
                    Name = tag
                });

                context.SaveChanges();
            }

            return tag + " was added successfully to database!";
        }
    }
}
