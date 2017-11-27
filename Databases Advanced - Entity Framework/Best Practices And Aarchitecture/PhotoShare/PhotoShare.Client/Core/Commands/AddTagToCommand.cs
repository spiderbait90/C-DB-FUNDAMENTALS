using System.Linq;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class AddTagToCommand
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {

            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albumName = data[1];
            var tagName = "#" + data[2];

            using (var db = new PhotoShareContext())
            {
                var tag = db.Tags.FirstOrDefault(x => x.Name == tagName);

                var album = db.Albums.FirstOrDefault(x => x.Name == albumName);

                if (tag == null || album == null)
                    throw new ArgumentException("Either tag or album do not exist!");

                var albumTags = new AlbumTag()
                {
                    Album = album,
                    Tag = tag
                };

                album.AlbumTags.Add(albumTags);
                db.SaveChanges();

            }
            return $"Tag {tagName} added to {albumName}!";
        }
    }
}
