using System;
using UnityEngine;
using UnityEngine.UI;

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

        private void Awake()
        {
            card = GetComponent<Image>();
        }

        private void Start()
        {
            SetTransparent(IsTransparent);
        }

        public void Apply(Appearance appearance)
        {
            head.sprite = appearance.Head;
            hair.sprite = appearance.Hair;
            eyes.sprite = appearance.Eyes;
            beard.sprite = appearance.Beard;
        }

        public void SetTransparent(bool value)
        {
            card.enabled = !value;
        }
    }
}
