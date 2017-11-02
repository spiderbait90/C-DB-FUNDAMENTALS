using System;
using System.Data.SqlClient;

namespace _3.Minion_Names
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
                var id = int.Parse(Console.ReadLine());

                var findMinions = new SqlCommand
                    (
                    @"SELECT m.Name,m.Age FROM Minions AS m
                     LEFT JOIN MinionsVillains AS mv ON m.Id = mv.MinionId
                     LEFT JOIN Villains AS v ON mv.VillainId = v.Id
                     WHERE v.Id = @id
                     ORDER BY m.Name"
                    , connection);
                var findVillain = new SqlCommand
                    (
                    @"SELECT name FROM Villains
                     WHERE Id=@id"
                    , connection);

                findVillain.Parameters.AddWithValue("@id", id);
                var resultV = (string)findVillain.ExecuteScalar();

                findMinions.Parameters.AddWithValue("@id", id);
                var resultM = findMinions.ExecuteReader();                

                if (resultM.HasRows && resultV != null)
                {
                    Console.WriteLine($"Villain: {resultV}");
                    var count = 1;
                    while (resultM.Read())
                    {
                        Console.WriteLine($"{count}. {resultM["Name"]} {resultM["Age"]}");
                        count++;
                    }
                }
                else if (!resultM.HasRows && resultV == null)
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                }

                else
                {
                    Console.WriteLine($"Villain: {resultV}");
                    Console.WriteLine($"(no minions)");
                }

            }
        }
    }
}
