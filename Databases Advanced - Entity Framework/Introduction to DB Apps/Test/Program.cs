using System;
using System.Data.SqlClient;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection
                (
                "Server=.;" +
                "Dataase=SoftUni;" +
                "Integrated Security=True"
                );

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand
                    (
                    "SELECT COUNT(*) FROM Employees",
                    connection
                    );

                var result = (int)command.ExecuteScalar();

                Console.WriteLine(result);
            }
        }
    }
}
