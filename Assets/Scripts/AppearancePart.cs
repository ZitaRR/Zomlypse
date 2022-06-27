using System;
using UnityEngine;
using Zomlypse.Enums;

namespace Zomlypse
{
    public class AppearancePart
    {
        public Sprite Sprite
        {
            get => sprite;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException($"{nameof(value)} cannot be null");
                }

                sprite = value;
                OnChange?.Invoke(this);
            }
        }
        public Color Color
        {
            get => color;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException($"{nameof(value)} cannot be null");
                }

                color = value;
                OnChange?.Invoke(this);
            }
        }
        public CustomizationPart Part { get; private set; }

        public event Action<AppearancePart> OnChange;

        private Sprite sprite;
        private Color color;

        public AppearancePart(Sprite sprite, CustomizationPart part) : this(sprite, Color.white, part)
        {

        }

        public AppearancePart(Sprite sprite, Color color, CustomizationPart part)
        {
            Sprite = sprite;
            Color = color;
            Part = part;
        }
    }
}