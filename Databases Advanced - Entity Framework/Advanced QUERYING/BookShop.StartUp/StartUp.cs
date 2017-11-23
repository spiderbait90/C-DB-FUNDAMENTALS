using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                Console.WriteLine(RemoveBooks(db) + " books were deleted");

            }
        }

        public static int RemoveBooks(BookShopContext db)
        {
            var books = db.Books
                .Where(x => x.Copies < 4200);

            var count = books.Count();
            
            db.Books.RemoveRange(books);
            db.SaveChanges();

            return count;
        }

        //                       14

        //public static void IncreasePrices(BookShopContext db)
        //{
        //    var books = db.Books
        //        .Where(x => x.ReleaseDate.Value.Year < 2010);

        //    foreach (var b in books)
        //    {
        //        b.Price += 5;
        //    }

        //    db.SaveChanges();
        //}


        //                             13

        //public static string GetMostRecentBooks(BookShopContext db)
        //{
        //    var categories = db.Categories
        //        .Include(x => x.CategoryBooks)
        //        .ThenInclude(x => x.Book)
        //        .OrderBy(x => x.Name);

        //    var result = new StringBuilder();

        //    foreach (var c in categories)
        //    {
        //        result.AppendLine($"--{c.Name}");

        //        foreach (var b in c.CategoryBooks)
        //        {
        //            result.AppendLine($"{b.Book.Title} ({b.Book.ReleaseDate.Value.Year})");
        //        }
        //    }

        //    return result.ToString();
        //}


        //                   12

        //public static string GetTotalProfitByCategory(BookShopContext db)
        //{
        //    var books = db.Categories
        //        .Select(x => new
        //        {
        //            name = x.Name,
        //            profit = x.CategoryBooks.Sum(a => a.Book.Price * a.Book.Copies)
        //        })
        //        .OrderByDescending(x => x.profit)
        //        .ThenBy(x => x.name)
        //        .ToList();

        //    return string.Join(Environment.NewLine, books.Select(x => $"{x.name} ${x.profit:f2}"));

        //}




        //                11

        //public static string CountCopiesByAuthor(BookShopContext db)
        //{
        //    var authors = db.Authors
        //        .Include(x => x.Books)
        //        .ToList();

        //    var result = new StringBuilder();

        //    foreach (var a in authors.OrderByDescending(x=>x.Books.Select(a=>a.Copies).Sum()))
        //    {
        //        result.AppendLine($"{a.FirstName} {a.LastName} - {a.Books.Select(x => x.Copies).Sum()}");
        //    }

        //    return result.ToString().Trim();
        //}



        //                10

        //public static int CountBooks(BookShopContext db, int lengthCheck)
        //{
        //    var bookCount = db.Books
        //        .Where(x => x.Title.Length > lengthCheck)
        //        .ToList()
        //        .Count;

        //    return bookCount;
        //}




        //                 9

        //public static string GetBooksByAuthor(BookShopContext db, string input)
        //{
        //    var str = input.ToLower();

        //    var books = db.Books
        //        .Include(x => x.Author)
        //        .Where(x => x.Author.LastName.ToLower().StartsWith(str))
        //        .OrderBy(x => x.BookId)
        //        .Select(x => $"{x.Title} ({x.Author.FirstName} {x.Author.LastName})")
        //        .ToList();

        //    return string.Join(Environment.NewLine, books);

        //}



        //                   8

        //public static string GetBookTitlesContaining(BookShopContext db, string input)
        //{
        //    var str = input.ToLower();

        //    var books = db.Books
        //        .Where(x => x.Title.ToLower().Contains(str))
        //        .OrderBy(x => x.Title)
        //        .Select(x => x.Title)
        //        .ToList();

        //    return string.Join(Environment.NewLine,books);
        //}


        //                     7

        //public static string GetAuthorNamesEndingIn(BookShopContext db, string input)
        //{
        //    var autors = db.Authors
        //        .Where(x => x.FirstName.EndsWith(input))
        //        .Select(x => $"{x.FirstName} {x.LastName}")
        //        .OrderBy(x=>x)
        //        .ToList();

        //    return string.Join(Environment.NewLine, autors);
        //}


        //                   6

        //public static String GetBooksReleasedBefore(BookShopContext db, string input)
        //{
        //    var date = DateTime.ParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        //    var books = db.Books
        //        .Where(x => x.ReleaseDate < date)
        //        .OrderByDescending(x=>x.ReleaseDate)
        //        .Select(x => new
        //        {
        //            x.Title,
        //            x.EditionType,
        //            x.Price
        //        }).ToList();

        //    var result = new StringBuilder();

        //    foreach (var b in books)
        //    {
        //        result.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:f2}");
        //    }

        //    return result.ToString();
        //}


        //                     5

        //public static string GetBooksByCategory(BookShopContext db, string input)
        //{

        //    string[] categories = input.Split().Select(c => c.ToLower()).ToArray();

        //    var books = db.Books
        //        .Where(b => b.BookCategories.Any(c => categories.Contains((c.Category.Name).ToLower())))
        //        .OrderBy(b => b.Title)
        //        .Select(b => b.Title)
        //        .ToList();

        //    return string.Join(Environment.NewLine, books);
        //}


        //                   4

        //public static string GetBooksNotRealeasedIn(BookShopContext db, int input)
        //{

        //    var books = db.Books
        //        .Where(x => x.ReleaseDate.Value.Year != input)
        //        .OrderBy(x => x.BookId)
        //        .Select(x => x.Title)
        //        .ToList();

        //    return string.Join(Environment.NewLine, books);
        //}



        //                    3

        //public static string GetBooksByPrice(BookShopContext db)
        //{
        //    var books = db.Books
        //        .Select(x => new
        //        {
        //            x.Title,
        //            x.Price
        //        })
        //        .Where(x => x.Price > 40)
        //        .OrderByDescending(x => x.Price)
        //        .ToList();

        //    var result = new StringBuilder();

        //    foreach (var book in books)
        //    {
        //        result.AppendLine($"{book.Title} - ${book.Price:f2}");
        //    }

        //    return result.ToString();
        //}



        //                    2

        //public static string GetGoldenBooks(BookShopContext db)
        //{
        //    var books = db.Books
        //        .Where(x => x.EditionType == EditionType.Gold)
        //        .Where(x => x.Copies < 5000)
        //        .OrderBy(x=>x.BookId)
        //        .Select(x => x.Title)
        //        .ToList();

        //    return string.Join(Environment.NewLine, books);
        //}




        //                    1

        //public static string GetBooksByAgeRestriction(BookShopContext db, string input)
        //{
        //    AgeRestriction parsed = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), input, true);

        //    var books = db.Books
        //        .Where(x => x.AgeRestriction == parsed)
        //        .OrderBy(x => x.Title)
        //        .Select(x => x.Title)
        //        .ToList();

        //    return string.Join(Environment.NewLine, books);
        //}
    }
}
