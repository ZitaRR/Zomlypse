using Zomlypse.IO;
using Zomlypse.IO.Collections;

namespace Zomlypse
{
    public class Entity
    {
        public static Names Names { get; }

        public Appearance Appearance { get; set; }
        public string Name { get; set; }

        static Entity()
        {
            Names = FileManager.Read<Names>(FileManager.MALE_NAMES);
        }

        public Entity() : this(Names.RandomName())
        {
            
        }

        public Entity(string name) : this(name, Appearance.Random())
        {
            
        }

        public Entity(string name, Appearance appearance)
        {
            Name = name;
            Appearance = appearance;
        }
    }
}
