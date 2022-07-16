using Zomlypse.IO;
using Zomlypse.IO.Collections;

namespace Zomlypse
{
    public class Entity
    {
        public static Names Names { get; }

        public Appearance Appearance { get; set; }
        public CharacterInfo Info { get; set; }

        static Entity()
        {
            Names = FileManager.Read<Names>(FileManager.MALE_NAMES);
        }

        public Entity(CharacterInfo info) : this(info, Appearance.Random())
        {
            
        }

        public Entity(CharacterInfo info, Appearance appearance)
        {
            Info = info;
            Appearance = appearance;
        }
    }
}
