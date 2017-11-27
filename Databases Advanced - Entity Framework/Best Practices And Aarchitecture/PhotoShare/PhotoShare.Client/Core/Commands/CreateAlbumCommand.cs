using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var username = data[1];
            var albumTitle = data[2];
            var bgColor = data[3].First().ToString().ToUpper() + new String(data[3].Skip(1).ToArray());
            var tags = data.Skip(4).Select(x => "#" + x).ToArray();

            using (var db = new PhotoShareContext())
            {
                var user = db.Users.SingleOrDefault(x => x.Username == username);

                var album = db.Albums.SingleOrDefault(x => x.Name == albumTitle);

                if (user == null)
                    throw new ArgumentException($"User {username} not found");

                if (album != null)
                    throw new ArgumentException($"Album {album.Name} does exists!");

                object parsedColor;

                if (!Enum.TryParse(typeof(Color), bgColor, out parsedColor))
                {
                    throw new ArgumentException($"Color {bgColor} not found!");
                }

                bool allTagsChecked = true;

                var tagsFromDb = db.Tags.Select(x => x.Name)
                    .ToList();

                foreach (var tag in tags)
                {
                    if (!tagsFromDb.Contains(tag))
                        allTagsChecked = false;
                }

                if (!allTagsChecked)
                {
                    throw new ArgumentException("invalid tags");
                }

                var albumToAdd = new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = (Color)parsedColor,
                    IsPublic = false,
                };

                var albumRole = new AlbumRole()
                {
                    User = user,
                    Album = albumToAdd,
                    Role = Role.Owner
                };

                albumToAdd.AlbumRoles.Add(albumRole);

                db.Albums.Add(albumToAdd);

                db.SaveChanges();

                var a = db.Albums.Single(x => x.Name == albumToAdd.Name);

                foreach (var tag in db.Tags)
                {
                    if (tags.Contains(tag.Name))
                    {
                        a.AlbumTags.Add(new AlbumTag()
                        {
                            Album = a,
                            Tag = tag
                        });
                    }
                }

                db.SaveChanges();
            }

            return $"Album {albumTitle} successfully created!";
        }
    }
}
