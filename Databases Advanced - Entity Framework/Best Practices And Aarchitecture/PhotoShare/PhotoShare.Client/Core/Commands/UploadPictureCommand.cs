using System.Linq;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albumName = data[1];
            var pictureTitle = data[2];
            var pictureFilePath = data[3];

            var db = new PhotoShareContext();

            var album = db.Albums.FirstOrDefault(x => x.Name == albumName);

            if (album == null)
                throw new ArgumentException($"Album {albumName} not found!");

            album.Pictures.Add(new Picture()
            {
                Title = pictureTitle,
                Path = pictureFilePath
            });

            db.SaveChanges();
            db.Dispose();

            return $"Picture {pictureTitle} added to {albumName}";
        }
    }
}
