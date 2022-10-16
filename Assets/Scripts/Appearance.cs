using System;
using UnityEngine;
using Zomlypse.Enums;
using Zomlypse.IO;
using Zomlypse.IO.Collections;

namespace Zomlypse
{
    public class Appearance
    {
        public const string DEFAULT_HEAD = "Sprites/default";
        public const string EYES = "Sprites/eyes";
        public const string EMPTY = "Sprites/empty";
        public const string MALE_HAIRS = "Sprites/MaleHairs/";
        public const string FEMALE_HAIRS = "Sprites/FemaleHairs/";
        public const string BEARDS = "Sprites/Beards/";

        public static int MaleHairCount { get; }
        public static int FemaleHairCount { get; }
        public static int BeardCount { get; }
        public static ColorPresets Colors { get; }

        public event Action<Appearance, AppearancePart> OnChange;

        public AppearancePart Head { get; set; }
        public AppearancePart Hair { get; set; }
        public AppearancePart Eyes { get; set; }
        public AppearancePart Beard { get; set; }
        public Gender Gender { get; }

        static Appearance()
        {
            MaleHairCount = Resources.LoadAll(MALE_HAIRS).Length / 2;
            FemaleHairCount = Resources.LoadAll(FEMALE_HAIRS).Length / 2;
            BeardCount = Resources.LoadAll(BEARDS).Length / 2;
            Colors = FileManager.Read<ColorPresets>(FileManager.COLOR_PRESETS);
        }

        public Appearance(Gender gender)
        {
            Gender = gender;

            Head = new AppearancePart(Load(DEFAULT_HEAD), CustomizationPart.Head);
            Hair = new AppearancePart(Load(EMPTY), CustomizationPart.Hair);
            Eyes = new AppearancePart(Load(EYES), CustomizationPart.Eyes);
            Beard = new AppearancePart(Load(EMPTY), CustomizationPart.Beard);

            Head.OnChange += (part) => OnChange?.Invoke(this, part);
            Hair.OnChange += (part) => OnChange?.Invoke(this, part);
            Eyes.OnChange += (part) => OnChange?.Invoke(this, part);
            Beard.OnChange += (part) => OnChange?.Invoke(this, part);
        }

        public void Load(CustomizationPart part, int index)
        {
            switch (part)
            {
                case CustomizationPart.Hair when Gender is Gender.Male:
                    Hair.Sprite = Load($"{MALE_HAIRS}hair_{index}");
                    return;
                case CustomizationPart.Hair when Gender is Gender.Female:
                    Hair.Sprite = Load($"{FEMALE_HAIRS}hair_{index}");
                    return;
                case CustomizationPart.Beard when Gender is Gender.Male:
                    Beard.Sprite = Load($"{BEARDS}beard_{index}");
                    return;
                case CustomizationPart.Beard when Gender is Gender.Female:
                    Beard.Sprite = Load(EMPTY);
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

        public Appearance RandomizeAppearance()
        {
            switch (Gender)
            {
                case Gender.Male:
                    int index = UnityEngine.Random.Range(0, MaleHairCount);
                    Hair.Sprite = Load($"{MALE_HAIRS}hair_{index}");
                    index = UnityEngine.Random.Range(0, BeardCount);
                    Beard.Sprite = Load($"{BEARDS}beard_{index}");
                    Beard.Color = Colors.RandomHair();
                    break;
                case Gender.Female:
                    index = UnityEngine.Random.Range(0, FemaleHairCount);
                    Hair.Sprite = Load($"{FEMALE_HAIRS}hair_{index}");
                    Beard.Sprite = Load(EMPTY);
                    break;
                default:
                    throw new InvalidOperationException($"Cannot randomize appearance for gender: {Gender.None}");
            }

            Head.Color = Colors.RandomSkin();
            Hair.Color = Colors.RandomHair();
            Eyes.Color = Colors.RandomEyes();
            return this;
        }

        public static Appearance Random(Gender gender)
        {
            return new Appearance(gender).RandomizeAppearance();
        }
    }
}
