using System;

namespace Zomlypse.IO.Containers
{
    [Serializable]
    public struct Name
    {
        public string name;

        public override string ToString()
        {
            return name;
        }
    }
}
