using System;
using UnityEngine;
using UnityEngine.UI;
using Zomlypse.Helpers;

namespace Zomlypse.Behaviours
{
    public class Card : MonoBehaviour
    {
        public Image Head => head;
        public Image Hair => hair;
        public Image Eyes => eyes;
        public Image Beard => beard;
        public bool IsTransparent
        {
            get => isTransparent;
            set
            {
                isTransparent = value;
                SetTransparent(isTransparent);
            }
        }

        [SerializeField]
        private bool isTransparent;
        [SerializeField]
        private Image head;
        [SerializeField]
        private Image hair;
        [SerializeField]
        private Image eyes;
        [SerializeField]
        private Image beard;

        private Image card;
        private CardDeck deck;
        private HoverEffect hover;

        private void Awake()
        {
            card = GetComponent<Image>();
            hover = GetComponent<HoverEffect>();
            deck = GetComponentInParent<CardDeck>();

            if (deck == null)
            {
                hover.enabled = false;
            }
        }

        private void Start()
        {
            SetTransparent(IsTransparent);
        }

        public void Apply(Appearance appearance)
        {
            head.sprite = appearance.Head.Sprite;
            head.color = appearance.Head.Color;

            hair.sprite = appearance.Hair.Sprite;
            hair.color = appearance.Hair.Color;

            eyes.sprite = appearance.Eyes.Sprite;
            eyes.color = appearance.Eyes.Color;

            beard.sprite = appearance.Beard.Sprite;
            beard.color = appearance.Beard.Color;
        }

        public void SetTransparent(bool value)
        {
            card.enabled = !value;
        }
    }
}
