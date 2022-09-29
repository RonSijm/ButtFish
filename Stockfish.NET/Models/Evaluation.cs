namespace Stockfish.NET.Models
{
    public class Evaluation
    {
        public string Type { get; set; }
        public int Value { get; set; }

        public Evaluation()
        {
        }

        public Evaluation(string type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}