using System;
using System.Data.SqlClient;
using System.IO;

namespace _1.Initial_Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection
                (
                "Server=.;Integrated Security=True"
                );

            connection.Open();

            using (connection)
            {
                var createDB = new SqlCommand("CREATE DATABASE MinionsDB",connection);
                createDB.ExecuteNonQuery();
                Console.WriteLine("DataBase Created");

                var query = File.ReadAllText(@"CreateTablesAndInsert.sql");
                var insertValues = new SqlCommand(query, connection);
                insertValues.ExecuteNonQuery();
                Console.WriteLine("Tables Created And Values Inserted");
            }
        }
    }
}
