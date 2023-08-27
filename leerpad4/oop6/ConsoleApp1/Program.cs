using System;

namespace PokemonCenterDemo
{
    public enum PokemonType
    {
        Fire,
        Water,
        Grass,
        Electric,
        // Add more types if needed...
    }

    public sealed class Pokemon
    {
        private readonly string _name;
        private readonly PokemonType _strength;
        private readonly PokemonType _weakness;

        public Pokemon(string name, PokemonType strength, PokemonType weakness)
        {
            _name = name;
            _strength = strength;
            _weakness = weakness;
        }

        public string Name => _name;
        public PokemonType Strength => _strength;
        public PokemonType Weakness => _weakness;
    }

    public sealed class Pokeball
    {
        private readonly Pokemon _pokemon;

        public Pokeball(Pokemon pokemon)
        {
            _pokemon = pokemon ?? throw new ArgumentNullException(nameof(pokemon));
        }

        public Pokemon ContainedPokemon => _pokemon;
    }

    public interface IPlace
    {
        string Name { get; }
    }

    public class Arena : IPlace
    {
        public string Name => "Arena";
    }

    public abstract class Human
    {
        protected string Name { get; }

        protected Human(string name)
        {
            Name = name;
        }
    }

    public class Trainer : Human
    {
        private const int MaxPokeballs = 6;
        public Pokemon[] _pokeballs = new Pokemon[MaxPokeballs];
        private int _currentPokeballCount = 0;

        public Trainer(string name) : base(name) { }

        public void AddPokemon(Pokemon pokemon)
        {
            if (_currentPokeballCount >= MaxPokeballs)
            {
                throw new Exception("Trainer can only have six pokeballs on the belt.");
            }

            _pokeballs[_currentPokeballCount++] = pokemon;
        }

        public string GetTrainerName() => Name;
    }

    public class Nurse : Human
    {
        private readonly HealingStation _healingStation;

        public Nurse(string name, HealingStation healingStation) : base(name)
        {
            _healingStation = healingStation;
        }

        public void HealPokemonBelt(Trainer trainer)
        {
            Console.WriteLine($"{GetNurseName()} is healing the Pokemon for {trainer.GetTrainerName()}.");
            _healingStation.Heal(trainer._pokeballs);
            Console.WriteLine($"{GetNurseName()} has healed the Pokemon for {trainer.GetTrainerName()}.");
        }

        public string GetNurseName() => Name;
    }

    public class HealingStation
    {
        public void Heal(Pokemon[] pokemons)
        {
            foreach (var pokemon in pokemons)
            {
                if (pokemon == null) continue;
                Console.WriteLine($"Healing {pokemon.Name}...");
                // Add logic to heal the Pokemon if needed.
            }
        }
    }

    public class PokemonCenter : IPlace
    {
        private readonly Nurse _nurse;

        public PokemonCenter(Nurse nurse)
        {
            _nurse = nurse;
        }

        public string Name => "Pokemon Center";

        public void Visit(Trainer trainer)
        {
            Console.WriteLine($"{trainer.GetTrainerName()} visits the Pokemon Center.");
            _nurse.HealPokemonBelt(trainer);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Pokemon charizard = new Pokemon("Charizard", PokemonType.Fire, PokemonType.Water);
            Trainer ash = new Trainer("Ash");
            ash.AddPokemon(charizard);

            HealingStation healingStation = new HealingStation();
            Nurse joy = new Nurse("Nurse Joy", healingStation);
            PokemonCenter pokemonCenter = new PokemonCenter(joy);

            pokemonCenter.Visit(ash);
        }
    }
}
