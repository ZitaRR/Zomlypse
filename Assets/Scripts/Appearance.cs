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

        public Sprite Head { get; set; }
        public Sprite Hair { get; set; }
        public Sprite Eyes { get; set; }
        public Sprite Beard { get; set; }

        public event Action<Appearance> OnChange;

        public Appearance()
        {
            Head = Resources.Load<Sprite>(DEFAULT_HEAD_PATH);
            Hair = Resources.Load<Sprite>(EMPTY_PATH);
            Eyes = Resources.Load<Sprite>(EYES_PATH);
            Beard = Resources.Load<Sprite>(EMPTY_PATH);
        }

        public void Load(CustomizationPart part, int index)
        {
            switch (part)
            {
                case CustomizationPart.Hair:
                    Hair = Load($"{HAIRS}hair_{index}");
                    break;
                case CustomizationPart.Beard:
                    Beard = Load($"{BEARDS}beard_{index}");
                    break;
                default:
                    Debug.Log($"{part} does not have any customization parts.");
                    return;
            }

            OnChange?.Invoke(this);
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
