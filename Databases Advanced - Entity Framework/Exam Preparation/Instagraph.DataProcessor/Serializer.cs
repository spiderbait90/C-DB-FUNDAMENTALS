using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Instagraph.Data;
using Instagraph.DataProcessor.DtoModels;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var posts = context.Posts
                .Where(x => x.Comments.Count == 0)
                .Select(x => new
                {
                    Id = x.Id,
                    Picture = x.Picture.Path,
                    User = x.User.Username
                })
                .OrderBy(x => x.Id)
                .ToArray();

            var result = JsonConvert.SerializeObject(posts, Formatting.Indented);

            return result;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var users = context.Users
                .Include(x => x.Posts)
                .ThenInclude(x => x.Comments)
                .Include(x => x.Followers)
                .ToArray();

            var filteredUsers = new List<User>();

            foreach (var u in users)
            {
                foreach (var f in u.Followers)
                {
                    if (u.Posts.Any(x => x.Comments.Any(a => a.UserId == f.FollowerId)))
                    {
                        filteredUsers.Add(u);
                        break; ;
                    }
                }
            }

            var result = filteredUsers
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    Username = x.Username,
                    Followers = x.Followers.Count
                })
                .Skip(1)
            .ToArray();

            var parsed = JsonConvert.SerializeObject(result, Formatting.Indented);
            
            return parsed;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
                .Include(x => x.Posts)
                .ThenInclude(x => x.Comments)
                .Select(x => new CommentsPostsDto()
                {
                    Username = x.Username,
                    MostComments = x.Posts.Select(a => a.Comments.Count).Max()
                })
                .ToArray();

            var xml = new XDocument(new XElement("users"));
            var dto = new List<CommentsPostsDto>();

            //foreach (var u in users)
            //{
            //    var mostComments = 0;
            //    if (u.posts.Any())
            //    {
            //        mostComments = u.posts.OrderByDescending(x => x).First();
            //    }
            //    dto.Add(new CommentsPostsDto()
            //    {
            //        Username = u.Username,
            //        MostComments = mostComments
            //    });
                
            //}
            //dto = dto.OrderByDescending(x => x.MostComments)
            //    .ThenBy(x => x.Username).ToList();

            //foreach (var d in dto)
            //{
            //    xml.Root.Add(new XElement("user",
            //        new XElement("Username", d.Username),
            //        new XElement("MostComments", d.MostComments)));
            //}
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CommentsPostsDto[]), new XmlRootAttribute("Users"));
            serializer.Serialize(new StringWriter(sb), users, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
            Console.WriteLine(sb.ToString());
            return xml.ToString();
        }
    }
}
