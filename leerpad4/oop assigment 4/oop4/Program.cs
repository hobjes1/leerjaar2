using System;
using System.Collections.Generic;

// Enum for Pokemon types
enum PokemonType { Fire, Grass, Water }

// Pokemon Base Class
class Pokemon
{
    public string Name { get; protected set; }
    public PokemonType Type { get; protected set; }
    public Pokemon(string name, PokemonType type)
    {
        Name = name;
        Type = type;
    }
}

class Charmander : Pokemon { public Charmander() : base("Charmander", PokemonType.Fire) { } }
class Bulbasaur : Pokemon { public Bulbasaur() : base("Bulbasaur", PokemonType.Grass) { } }
class Squirtle : Pokemon { public Squirtle() : base("Squirtle", PokemonType.Water) { } }

class Trainer
{
    public string Name { get; private set; }
    private List<Pokemon> belt = new List<Pokemon>();

    public Trainer(string name)
    {
        Name = name;
        belt.AddRange(new Pokemon[] { new Charmander(), new Bulbasaur(), new Squirtle() }); // Just one of each for simplicity
    }

    public Pokemon ThrowPokemon()
    {
        var rnd = new Random();
        var index = rnd.Next(belt.Count);
        var chosenPokemon = belt[index];
        belt.RemoveAt(index);
        return chosenPokemon;
    }

    public bool HasPokemonLeft() => belt.Count > 0;
}

class Battle
{
    private Trainer trainer1;
    private Trainer trainer2;
    private Pokemon winnerFromLastRound;

    public Battle(Trainer t1, Trainer t2)
    {
        trainer1 = t1;
        trainer2 = t2;
    }

    public string FightRound()
    {
        var pokemon1 = trainer1.ThrowPokemon();
        var pokemon2 = trainer2.ThrowPokemon();

        if (pokemon1.Type == pokemon2.Type) // Draw
        {
            return "Draw";
        }
        else if ((pokemon1.Type == PokemonType.Fire && pokemon2.Type == PokemonType.Grass) ||
                 (pokemon1.Type == PokemonType.Grass && pokemon2.Type == PokemonType.Water) ||
                 (pokemon1.Type == PokemonType.Water && pokemon2.Type == PokemonType.Fire))
        {
            winnerFromLastRound = pokemon1;
            return $"{trainer1.Name} wins this round";
        }
        else
        {
            winnerFromLastRound = pokemon2;
            return $"{trainer2.Name} wins this round";
        }
    }

    public string GetBattleResult()
    {
        int trainer1Wins = 0, trainer2Wins = 0;

        while (trainer1.HasPokemonLeft() && trainer2.HasPokemonLeft())
        {
            var result = FightRound();
            if (result.Contains(trainer1.Name)) trainer1Wins++;
            if (result.Contains(trainer2.Name)) trainer2Wins++;
        }

        if (trainer1Wins > trainer2Wins) return $"{trainer1.Name} wins the battle";
        else if (trainer1Wins < trainer2Wins) return $"{trainer2.Name} wins the battle";
        else return "The battle is a draw";
    }
}

class Arena
{
    public static int TotalRoundsFought { get; private set; }
    public static int TotalBattlesFought { get; private set; }

    private Battle currentBattle;

    public Arena(Trainer t1, Trainer t2)
    {
        currentBattle = new Battle(t1, t2);
    }

    public void ConductBattle()
    {
        Console.WriteLine(currentBattle.GetBattleResult());
        TotalBattlesFought++;
    }

    public static void ShowScoreboard()
    {
        Console.WriteLine($"Total Battles: {TotalBattlesFought}");
    }
}

class Program
{
    static void Main()
    {
        Trainer ash = new Trainer("Ash");
        Trainer misty = new Trainer("Misty");

        Arena battleArena = new Arena(ash, misty);
        battleArena.ConductBattle();

        Arena.ShowScoreboard();
    }
}

