using System;

namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string townName = data[1];
            string country = data[2];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                try
                {
                    context.Towns.Add(town);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Town {town.Name} was already added!");
                }

                return townName + " was added to database!";
            }
        }
    }
}
