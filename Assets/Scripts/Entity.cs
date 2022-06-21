using Zomlypse.Behaviours;

namespace Zomlypse
{
    public class Entity
    {
        public Appearance Appearance { get; set; }

        public Entity() : this(new Appearance())
        {
            
        }

        public Entity(Appearance appearance)
        {
            Appearance = appearance;
        }
    }
}
