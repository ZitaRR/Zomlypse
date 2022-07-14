using System;
using Zomlypse.IO.Containers;

namespace Zomlypse.IO.Collections
{
    [Serializable]
    public class Names
    {
        public Name[] names;

        public string RandomName()
        {
            return names[UnityEngine.Random.Range(0, names.Length)].name;
        }
    }
}