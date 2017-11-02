using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Team
{
    private Dictionary<string, Player> players;
    private string name;
    private int rating = 0;

    public Team(string name)
    {
        Name = name;
        Players = new Dictionary<string, Player>();        
    }
    public int Raiting
    {
        get { return rating; }
        set { rating = value;}
    }
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value)|| string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("A name should not be empty.");
            }
            name = value;
        }
    }
    private Dictionary<string, Player> Players
    {
        get { return players; }
        set { players = value;}
    }

    private int AvgSkillLevel()
    {
        if (Players.Count == 0)
            return 0;
        return (int)Math.Round(Players.Sum(x => x.Value.SkillLevel()) / (double)Players.Count());
    }
    public void AddPlayer(Player player)
    {
        this.Players.Add(player.Name, player);
        Raiting = this.AvgSkillLevel();
    }
    public void RemovePlayer(string player)
    {
        if (Players.ContainsKey(player))
        {
            this.Players.Remove(player);
            Raiting = this.AvgSkillLevel();
        }
        else
            throw new ArgumentException($"Player {player} is not in {Name} team.");
    }
}

