using System;
using UnityEngine;
using Zomlypse.Extensions;
using Zomlypse.IO.Containers;

namespace Zomlypse.IO.Collections
{
    [Serializable]
    public class ColorPresets
    {
        public HexColor[] skin;
        public HexColor[] eyes;
        public HexColor[] hair;

        public Color32 RandomSkin()
        {
            return skin[UnityEngine.Random.Range(0, skin.Length)].ToColor32();
        }

        public Color32 RandomEyes()
        {
            return eyes[UnityEngine.Random.Range(0, eyes.Length)].ToColor32();
        }

        public Color32 RandomHair()
        {
            return hair[UnityEngine.Random.Range(0, hair.Length)].ToColor32();
        }
    }
}
