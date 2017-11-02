using System;
using System.Data.SqlClient;
using System.IO;

namespace _2.Villain_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection
                (
                "Server=.;Database=MinionsDB;Integrated Security=True"
                );

            connection.Open();

            using (connection)
            {
                var query = File.ReadAllText(@"Query.sql");
                var command = new SqlCommand(query, connection);
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    var name = (string)result["Name"];
                    var count = (int)result["Count"];
                    Console.WriteLine($"{name} - {count}");
                }
            }
        }
    }
}
