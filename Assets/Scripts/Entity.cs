using Zomlypse.Behaviours;

namespace Zomlypse
{
    public class Entity
    {
        public Appearance Appearance { get; set; }
        public string Name { get; set; }

        public Entity(string name) : this(name, new Appearance())
        {
            
        }

        public Entity(string name, Appearance appearance)
        {
            Name = name;
            Appearance = appearance;
        }
    }
}
