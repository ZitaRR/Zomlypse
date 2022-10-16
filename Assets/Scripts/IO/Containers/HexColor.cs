using System;

namespace Zomlypse.IO.Containers
{
    [Serializable]
    public struct HexColor
    {
        public byte RR { get => (byte)Convert.ToInt32(hex.Substring(0, 2), 16); }
        public byte GG { get => (byte)Convert.ToInt32(hex.Substring(2, 2), 16); }
        public byte BB { get => (byte)Convert.ToInt32(hex.Substring(4, 2), 16); }

        public string name;
        public string hex;

        public override string ToString()
        {
            return $"{name}: [{hex}]";
        }
    }
}
