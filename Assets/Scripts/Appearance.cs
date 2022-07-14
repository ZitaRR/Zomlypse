using System;
using UnityEngine;
using Zomlypse.Enums;

namespace Zomlypse
{
    public class Appearance
    {
        public const string DEFAULT_HEAD_PATH = "Sprites/default";
        public const string EYES_PATH = "Sprites/eyes";
        public const string EMPTY_PATH = "Sprites/empty";
        public const string HAIRS = "Sprites/Hairs/";
        public const string BEARDS = "Sprites/Beards/";

        public static readonly int HairCount;
        public static readonly int BeardCount;

        public AppearancePart Head { get; set; }
        public AppearancePart Hair { get; set; }
        public AppearancePart Eyes { get; set; }
        public AppearancePart Beard { get; set; }

        public event Action<Appearance, AppearancePart> OnChange;

        static Appearance()
        {
            HairCount = Resources.LoadAll(HAIRS).Length / 2;
            BeardCount = Resources.LoadAll(BEARDS).Length / 2;
        }

        public Appearance()
        {
            Head = new AppearancePart(Load(DEFAULT_HEAD_PATH), CustomizationPart.Head);
            Hair = new AppearancePart(Load(EMPTY_PATH), CustomizationPart.Hair);
            Eyes = new AppearancePart(Load(EYES_PATH), CustomizationPart.Eyes);
            Beard = new AppearancePart(Load(EMPTY_PATH), CustomizationPart.Beard);

            Head.OnChange += (part) => OnChange?.Invoke(this, part);
            Hair.OnChange += (part) => OnChange?.Invoke(this, part);
            Eyes.OnChange += (part) => OnChange?.Invoke(this, part);
            Beard.OnChange += (part) => OnChange?.Invoke(this, part);
        }

        public void Load(CustomizationPart part, int index)
        {
            switch (part)
            {
                case CustomizationPart.Hair:
                    Hair.Sprite = Load($"{HAIRS}hair_{index}");
                    return;
                case CustomizationPart.Beard:
                    Beard.Sprite = Load($"{BEARDS}beard_{index}");
                    return;
                default:
                    throw new ArgumentException($"{part} does not have any customization parts");
            }
        }

        private Sprite Load(string path)
        {
            Sprite sprite = Resources.Load<Sprite>(path);
            if (sprite == null)
            {
                throw new ArgumentException($"{path} could not be found");
            }
            return sprite;
        }

        public void RandomizeAppearance()
        {
            Hair.Randomize();
            Beard.Randomize();

            Head.Color = ColorPresets.RandomSkin();
            Hair.Color = ColorPresets.RandomHair();
            Eyes.Color = ColorPresets.RandomEyes();
            Beard.Color = ColorPresets.RandomHair();
        }

        public static Appearance Random()
        {
            Appearance appearance = new Appearance();
            appearance.RandomizeAppearance();
            return appearance;
        }

        public static class ColorPresets
        {
            public static Color32[] Skin { get; } = new Color32[]
            {
                new Color32(255, 219, 125, 255),
                new Color32(241, 194, 125, 255),
                new Color32(224, 172, 105, 255),
                new Color32(198, 134, 66, 255),
                new Color32(141, 85, 36, 255)
            };

            public static Color32[] Eyes { get; } = new Color32[]
            {
                new Color32(99, 78, 52, 255),
                new Color32(46, 83, 111, 255),
                new Color32(61, 103, 29, 255),
                new Color32(28, 120, 71, 255),
                new Color32(73, 118, 101, 255)
            };

            public static Color32[] Hair { get; } = new Color32[]
            {
                new Color32(9, 8, 6, 255),
                new Color32(44, 34, 43, 255),
                new Color32(106, 78, 66, 255),
                new Color32(167, 133, 106, 255),
                new Color32(220, 208, 186, 255),
                new Color32(222, 188, 153, 255),
                new Color32(165, 107, 70, 255),
                new Color32(145, 85, 61, 255),
                new Color32(183, 166, 158, 255),
                new Color32(214, 196, 194, 255),
                new Color32(255, 24, 225, 255),
                new Color32(181, 82, 57, 255)
            };

            public static Color32 RandomSkin()
            {
                return Skin[UnityEngine.Random.Range(0, Skin.Length)];
            }

            public static Color32 RandomEyes()
            {
                return Eyes[UnityEngine.Random.Range(0, Eyes.Length)];
            }

            public static Color32 RandomHair()
            {
                return Hair[UnityEngine.Random.Range(0, Hair.Length)];
            }
        }
    }
}
