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

        public AppearancePart Head { get; set; }
        public AppearancePart Hair { get; set; }
        public AppearancePart Eyes { get; set; }
        public AppearancePart Beard { get; set; }

        public event Action<Appearance, AppearancePart> OnChange;

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
    }
}
