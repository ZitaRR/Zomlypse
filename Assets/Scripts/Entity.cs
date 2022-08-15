using Zomlypse.Enums;
using Zomlypse.IO;
using Zomlypse.IO.Collections;

namespace Zomlypse
{
    public class Entity
    {
        public static Names MaleNames { get; }
        public static Names FemaleNames { get; }

        public Appearance Appearance { get; set; }
        public CharacterInfo Info { get; set; }

        static Entity()
        {
            MaleNames = FileManager.Read<Names>(FileManager.MALE_NAMES);
            FemaleNames = FileManager.Read<Names>(FileManager.FEMALE_NAMES);
        }

        public Entity()
        {
            Gender gender = UnityEngine.Random.Range(0, 2) == 0
                ? Gender.Male
                : Gender.Female;
            string name = gender is Gender.Male
                ? MaleNames.RandomName()
                : FemaleNames.RandomName();

            Info = new CharacterInfo(name, RandomAge(), gender);
            Appearance = Appearance.Random(Info.Gender);
            UnityEngine.Debug.Log($"Instantiated entity: {Info.Fullname}, {Info.Gender}");
        }

        public Entity(CharacterInfo info) : this(info, Appearance.Random(info.Gender))
        {
            
        }

        public Entity(CharacterInfo info, Appearance appearance)
        {
            Info = info;
            Appearance = appearance;
        }

        public static int RandomAge()
        {
            return UnityEngine.Random.Range(18, 65);
        }
    }
}
