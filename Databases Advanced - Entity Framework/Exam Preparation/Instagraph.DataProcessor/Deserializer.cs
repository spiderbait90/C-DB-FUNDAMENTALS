using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.DataProcessor.DtoModels;
using Instagraph.Models;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var desPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var result = new StringBuilder();
            var picturesToAdd = new List<Picture>();


            foreach (var p in desPictures)
            {
                bool isValid = p.Size > 0 && !string.IsNullOrWhiteSpace(p.Path);

                bool ifExists = picturesToAdd.Any(x => x.Path == p.Path) || context.Pictures.Any(x => x.Path == p.Path);

                if (isValid && !ifExists)
                {
                    picturesToAdd.Add(p);
                    result.AppendLine($"Successfully imported Picture {p.Path}.");
                }
                else
                {
                    result.AppendLine("Error: Invalid data.");
                }
            }
            context.AddRange(picturesToAdd);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var desUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var result = new StringBuilder();
            var usersToAdd = new List<User>();

            foreach (var u in desUsers)
            {
                if (string.IsNullOrWhiteSpace(u.Username) ||
                    string.IsNullOrWhiteSpace(u.Password) ||
                    string.IsNullOrWhiteSpace(u.ProfilePicture))
                {
                    result.AppendLine("Error: Invalid data.");
                }

                else if (u.Username.Length > 30 || u.Password.Length > 20 || usersToAdd.Any(x => x.Username == u.Username))
                {
                    result.AppendLine("Error: Invalid data.");
                }
                else if (!context.Pictures.Any(x => x.Path == u.ProfilePicture) || usersToAdd.Any(x => x.Username == u.Username))
                {
                    result.AppendLine("Error: Invalid data.");
                }
                else
                {
                    User user = new User()
                    {
                        Username = u.Username,
                        Password = u.Password,
                        ProfilePicture = context.Pictures.First(x => x.Path == u.ProfilePicture)
                    };

                    usersToAdd.Add(user);

                    result.AppendLine($"Successfully imported User {u.Username}.");
                }
            }

            context.Users.AddRange(usersToAdd);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var json = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var users = context.Users.AsNoTracking().ToList();

            var str = new StringBuilder();
            var userFollowers = new List<UserFollower>();

            foreach (var uf in json)
            {
                if (users.Any(x => x.Username == uf.User) &&
                    users.Any(x => x.Username == uf.Follower) &&
                    !userFollowers.Any(x => x.User.Username == uf.User && x.Follower.Username == uf.Follower))
                {
                    userFollowers.Add(new UserFollower()
                    {
                        User = users.Single(x => x.Username == uf.User),
                        Follower = users.Single(x => x.Username == uf.Follower)
                    });

                    str.AppendLine($"Successfully imported Follower {uf.Follower} to User {uf.User}.");
                }
                else
                {
                    str.AppendLine("Error: Invalid data.");
                }
            }
            context.UsersFollowers.AddRange(userFollowers);
            context.SaveChanges();

            return str.ToString();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xml = XDocument.Parse(xmlString);
            var str = new StringBuilder();
            var posts = new List<Post>();

            foreach (var e in xml.Root.Elements())
            {
                var caption = e.Element("caption")?.Value;
                var username = e.Element("user")?.Value;
                var pic = e.Element("picture")?.Value;

                if (string.IsNullOrWhiteSpace(caption) ||
                    string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(pic))
                {
                    str.AppendLine("Error: Invalid data.");
                    continue;
                }

                var user = context.Users.FirstOrDefault(x => x.Username == username)?.Id;

                var picture = context.Pictures.FirstOrDefault(x => x.Path == pic)?.Id;

                if (picture == null || user == null)
                {
                    str.AppendLine("Error: Invalid data.");
                    continue;
                }

                var post = new Post()
                {
                    Caption = caption,
                    UserId = user.Value,
                    PictureId = picture.Value
                };

                posts.Add(post);
                str.AppendLine($"Successfully imported Post {caption}.");
            }

            context.Posts.AddRange(posts);
            context.SaveChanges();

            return str.ToString();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xml = XDocument.Parse(xmlString);

            var str = new StringBuilder();

            var comments = new List<Comment>();

            foreach (var c in xml.Root.Elements())
            {
                var content = c.Element("content")?.Value;
                var user = c.Element("user")?.Value;
                var post = c.Element("post")?.Attribute("id")?.Value;

                if (string.IsNullOrWhiteSpace(content) ||
                    string.IsNullOrWhiteSpace(user) ||
                    string.IsNullOrWhiteSpace(post))
                {
                    str.AppendLine("Error: Invalid data.");
                    continue;
                }

                var userr = context.Users.FirstOrDefault(x => x.Username == user)?.Id;

                var postt = context.Posts.FirstOrDefault(x => x.Id == int.Parse(post))?.Id;

                if (userr == null || postt == null)
                {
                    str.AppendLine("Error: Invalid data.");
                    continue;
                }

                var comment = new Comment()
                {
                    Content = content,
                    UserId = userr.Value,
                    PostId = postt.Value
                };

                comments.Add(comment);
                str.AppendLine($"Successfully imported Comment {content}.");
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();

            return str.ToString();
        }
    }
}
