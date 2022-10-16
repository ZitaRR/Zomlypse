using UnityEngine;
using Zomlypse.Enums;

namespace Zomlypse
{
    public class Stat
    {
        public const int MAX = 10;

        public int Value { get; private set; }
        public StatType Type { get; }

        public Stat(StatType type)
        {
            Type = type;
        }

        public bool TryIncrease(int value = 1)
        {
            if (value > MAX - Value)
            {
                return false;
            }

            Value += value;
            return true;
        }

        public int Randomize(int max)
        {
            int clamp = max >= MAX
                ? MAX
                : max;
            int value = Random.Range(0, clamp);
            Value = value;
            return Value;
        }

        public int Randomize()
        {
            return Randomize(MAX + 1);
        }

        public override string ToString()
        {
            return $"[{Type}: {Value}]";
        }
    }
}
