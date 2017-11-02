using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {

        var teams = new Dictionary<string, Team>();
        while (true)
        {
            try
            {
                var input = Console.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (input[0] == "END")
                    break;

                if (input[0] == "Team")
                {
                    var team = new Team(input[1]);
                    teams.Add(input[1], team);
                }
                else if (input[0] == "Add")
                {
                    if (!teams.ContainsKey(input[1]))
                        throw new ArgumentException($"Team {input[1]} does not exist.");

                    var player = new Player(input[2], int.Parse(input[3]),
                        int.Parse(input[4]), int.Parse(input[5]),
                        int.Parse(input[6]), int.Parse(input[7]));                   
                    
                        teams[input[1]].AddPlayer(player);
                }
                else if (input[0] == "Remove")
                {
                    if (!teams.ContainsKey(input[1]))
                        throw new ArgumentException($"Team {input[1]} does not exist.");
                    else
                        teams[input[1]].RemovePlayer(input[2]);
                }
                else if (input[0] == "Rating")
                {
                    if (!teams.ContainsKey(input[1]))
                        throw new ArgumentException($"Team {input[1]} does not exist.");
                    else
                        Console.WriteLine($"{teams[input[1]].Name} - {teams[input[1]].Raiting}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}

