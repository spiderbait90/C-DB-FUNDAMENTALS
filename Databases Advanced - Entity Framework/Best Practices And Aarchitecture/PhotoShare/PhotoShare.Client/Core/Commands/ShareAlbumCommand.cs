using System.Linq;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albumId = int.Parse(data[1]);
            var userName = data[2];
            var permission = data[3];

            var db = new PhotoShareContext();

            var album = db.Albums.SingleOrDefault(x => x.Id == albumId);

            var user = db.Users.SingleOrDefault(x => x.Username == userName);

            if (album == null)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            if (user == null)
            {
                throw new ArgumentException($"User {userName} not found!");
            }

            if (permission != "owner" && permission != "viewer")
            {
                throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
            }

            var parsed = (Role)Enum.Parse(typeof(Role), permission, true);

            album.AlbumRoles.Add(new AlbumRole()
            {
                User = user,
                Album = album,
                Role = parsed
            });

            db.SaveChanges();
            db.Dispose();

            return $"Username {userName} added to album [{album.Name}] ([{permission}])";
        }
    }
}
