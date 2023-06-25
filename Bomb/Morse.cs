using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTANE_Bot
{
    public class Morse : KTANE_Module
    {
        private static readonly Dictionary<string, char> MorseCodeDict = new Dictionary<string, char>
        {
            { "01", 'a' },
            { "1000", 'b' },
            { "1010", 'c' },
            { "100", 'd' },
            { "0", 'e' },
            { "0010", 'f' },
            { "110", 'g' },
            { "0000", 'h' },
            { "00", 'i' },
            { "0111", 'j' },
            { "101", 'k' },
            { "0100", 'l' },
            { "11", 'm' },
            { "10", 'n' },
            { "111", 'o' },
            { "0110", 'p' },
            { "1101", 'q' },
            { "010", 'r' },
            { "000", 's' },
            { "1", 't' },
            { "001", 'u' },
            { "0001", 'v' },
            { "011", 'w' },
            { "1001", 'x' },
            { "1011", 'y' },
            { "1100", 'z' }
        };

        private static readonly Dictionary<string, float> WordsDict = new Dictionary<string, float>
        {
            { "shell", 3.505f },
            { "halls", 3.515f },
            { "slick", 3.522f },
            { "trick", 3.532f },
            { "boxes", 3.535f },
            { "leaks", 3.542f },
            { "strobe", 3.545f },
            { "bistro", 3.552f },
            { "flick", 3.555f },
            { "bombs", 3.565f },
            { "break", 3.572f },
            { "brick", 3.575f },
            { "steak", 3.582f },
            { "sting", 3.592f },
            { "vector", 3.595f },
            { "beats", 3.600f }
        };

        private StringBuilder _letters;

        public Morse(Bomb bomb) : base(bomb)
        {
            _letters = new StringBuilder();
        }

        public override string Solve()
        {
            var abbreviation = _letters.ToString();
            var wordsThatStartWithIt =
                (from word in WordsDict.Keys.ToList() where word.StartsWith(abbreviation) select word).ToList();

            var callerDict = new Dictionary<int, string>
            {
                { 1, "first" },
                { 2, "second" },
                { 3, "third" },
                { 4, "fourth" },
                { 5, "fifth" },
                { 6, "sixth" }
            };

            switch (wordsThatStartWithIt.Count)
            {
                case 16:
                case 0:
                    _letters = new StringBuilder();
                    return @"No words found; try again.";
                case 1:
                    return $"Word \"{wordsThatStartWithIt.Last()}\"; tunes in {WordsDict[wordsThatStartWithIt.Last()]} mega hertz.";
                default:
                    return $"{callerDict[_letters.Length + 1]} letter.";
            }
        }

        public bool AddLetters(params string[] sequence)
        {
            var interpretDict = new Dictionary<string, string>
            {
                { "dot", "0" },
                { "dash", "1" },
                { "zero", "0" },
                { "one", "1" },
                { ".", "0" },
                { "-", "1" },
                { "won", "1" },
                { "on", "1" }
            };

            var numbers = new StringBuilder();

            try
            {
                foreach (var s in sequence)
                    numbers.Append(interpretDict[s]);
                
                _letters.Append(MorseCodeDict[numbers.ToString()]);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public bool AddLetters(params char[] sequence)
        {
            var stringSequence = new string[sequence.Length];

            for (var i = 0; i < sequence.Length; i++)
                stringSequence[i] = sequence[i].ToString();

            return AddLetters(stringSequence);
        }
    }
}