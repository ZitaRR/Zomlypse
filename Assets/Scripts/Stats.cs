using Zomlypse.Enums;

namespace Zomlypse
{
    public class Stats
    {
        public const int INITIAL = 14;

        public Stat Fitness { get => stats[0]; }
        public Stat Nimble { get => stats[1]; }
        public Stat Technical { get => stats[2]; }
        public Stat Medical { get => stats[3]; }
        public int Length { get => stats.Length; }

        private Stat[] stats = new Stat[]
        {
            new Stat(StatType.Fitness),
            new Stat(StatType.Nimble),
            new Stat(StatType.Technical),
            new Stat(StatType.Medical)
        };

        public Stats Randomize()
        {
            int current = INITIAL;
            for (int i = 0; i < Length; i++)
            {
                current -= stats[i].Randomize(current + 1);
                if (current == 0)
                {
                    return this;
                }
            }

            return this;
        }

        public static Stats Random()
        {
            return new Stats().Randomize();
        }

        public Stat this[int index]
        {
            get => stats[index];
            set => stats[index] = value;
        }

        public Stat this[StatType type]
        {
            get => stats[(int)type];
            set => stats[(int)type] = value;
        }
    }
}
