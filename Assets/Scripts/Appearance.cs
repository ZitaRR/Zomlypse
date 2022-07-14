using System;
using UnityEngine;
using Zomlypse.Enums;
using Zomlypse.IO;
using Zomlypse.IO.Collections;

namespace Zomlypse
{
    public class Appearance
    {
        public const string DEFAULT_HEAD_PATH = "Sprites/default";
        public const string EYES_PATH = "Sprites/eyes";
        public const string EMPTY_PATH = "Sprites/empty";
        public const string HAIRS = "Sprites/Hairs/";
        public const string BEARDS = "Sprites/Beards/";

        public static int HairCount { get; }
        public static int BeardCount { get; }
        public static ColorPresets Colors { get; }

        public AppearancePart Head { get; set; }
        public AppearancePart Hair { get; set; }
        public AppearancePart Eyes { get; set; }
        public AppearancePart Beard { get; set; }

        public event Action<Appearance, AppearancePart> OnChange;

        static Appearance()
        {
            HairCount = Resources.LoadAll(HAIRS).Length / 2;
            BeardCount = Resources.LoadAll(BEARDS).Length / 2;
            Colors = FileManager.Read<ColorPresets>(FileManager.COLOR_PRESETS);
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

            Head.Color = Colors.RandomSkin();
            Hair.Color = Colors.RandomHair();
            Eyes.Color = Colors.RandomEyes();
            Beard.Color = Colors.RandomHair();
        }

        public static Appearance Random()
        {
            Appearance appearance = new Appearance();
            appearance.RandomizeAppearance();
            return appearance;
        }
    }
}
