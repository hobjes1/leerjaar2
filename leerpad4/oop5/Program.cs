using System;

namespace YourNamespace
{
    public enum PokemonType
    {
        Fire,
        Water,
        Grass,
        Electric,
        //... (Add more types if needed)
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

    public class Trainer
    {
        private const int MaxPokeballs = 6;
        private readonly Pokemon[] _pokeballs = new Pokemon[MaxPokeballs];
        private int _currentPokeballCount = 0;

        public void AddPokemon(Pokemon pokemon)
        {
            if (_currentPokeballCount >= MaxPokeballs)
            {
                throw new Exception("Trainer can only have six pokeballs on the belt.");
            }

            _pokeballs[_currentPokeballCount++] = pokemon;
        }

        public Pokemon GetPokemon(int index)
        {
            if (index < 0 || index >= _currentPokeballCount)
            {
                throw new Exception("Invalid Pokeball index.");
            }
            return _pokeballs[index];
        }

        public int CurrentPokeballCount => _currentPokeballCount;
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

    class Program
    {
        static void Main(string[] args)
        {
            // Sample usage
            Pokemon charizard = new Pokemon("Charizard", PokemonType.Fire, PokemonType.Water);
            Pokeball pokeball1 = new Pokeball(charizard);

            Trainer ash = new Trainer();
            ash.AddPokemon(charizard);

            Console.WriteLine($"{charizard.Name}'s strength is {charizard.Strength}");
            Console.WriteLine($"Number of Pokemon with Ash: {ash.CurrentPokeballCount}");
        }
    }
}