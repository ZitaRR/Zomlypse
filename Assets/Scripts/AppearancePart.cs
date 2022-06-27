using System;
using UnityEngine;

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

        public event Action<AppearancePart> OnChange;

        private Sprite sprite;
        private Color color;

        public AppearancePart(Sprite sprite) : this(sprite, Color.white)
        {

        }

        public AppearancePart(Sprite sprite, Color color)
        {
            Sprite = sprite;
            Color = color;
        }
    }
}