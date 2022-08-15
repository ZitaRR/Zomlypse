using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zomlypse.Enums;
using Zomlypse.Extensions;

namespace Zomlypse.Behaviours
{
    public class CharacterCreation : MonoBehaviour
    {
        public Appearance ActiveAppearance
        {
            get => maleToggle.isOn
                ? maleAppearance
                : femaleAppearance;
        }
        public bool IsDetailedCustomizationActive
            => current == CustomizationPart.Hair || (current == CustomizationPart.Beard && ActiveAppearance.Gender is Gender.Male);

        [SerializeField]
        private RectTransform customizationPanel;
        [SerializeField]
        private GameObject customizationContent;
        [SerializeField]
        private GameObject customizationCard;
        [SerializeField]
        private ColorPicker colorPicker;
        [SerializeField]
        private RectTransform optionsPanel;
        [SerializeField]
        private Toggle maleToggle;

        private Appearance maleAppearance;
        private Appearance femaleAppearance;
        private Card card;
        private CharacterInfo info;
        private CustomizationPart current;

        private void Awake()
        {
            card = GetComponentInChildren<Card>();

            maleToggle.onValueChanged.AddListener((value) =>
            {
                SetCustomizationOption(current);
                card.Apply(ActiveAppearance);
            });
        }

        private void Start()
        {
            maleAppearance = Appearance.Random(Gender.Male);
            femaleAppearance = Appearance.Random(Gender.Female);
            maleAppearance.OnChange += (appearance, _) => card.Apply(appearance);
            femaleAppearance.OnChange += (appearance, _) => card.Apply(appearance);

            card.Apply(ActiveAppearance);
            SetCustomizationOption(CustomizationPart.Head);
        }

        public void SetCustomizationIndex(int index = 0)
        {
            ActiveAppearance.Load(current, index);
        }

        public void SetCustomizationOption(int index)
        {
            SetCustomizationOption((CustomizationPart)index);
        }

        public void SetCustomizationOption(CustomizationPart part)
        {
            switch (part)
            {
                case CustomizationPart.Head:
                    colorPicker.Attach(ActiveAppearance.Head);
                    PopulateCustomizationContent();
                    break;
                case CustomizationPart.Hair:
                    colorPicker.Attach(ActiveAppearance.Hair);
                    PopulateCustomizationContent(part);
                    break;
                case CustomizationPart.Eyes:
                    colorPicker.Attach(ActiveAppearance.Eyes);
                    PopulateCustomizationContent();
                    break;
                case CustomizationPart.Beard:
                    colorPicker.Attach(ActiveAppearance.Beard);
                    PopulateCustomizationContent(part);
                    break;
            }
            current = part;
        }

        private void PopulateCustomizationContent(CustomizationPart part = CustomizationPart.None)
        {
            Sprite[] sprites = new Sprite[0];
            switch (part)
            {
                case CustomizationPart.Hair when ActiveAppearance.Gender is Gender.Male:
                    sprites = Resources.LoadAll<Sprite>(Appearance.MALE_HAIRS);
                    break;
                case CustomizationPart.Hair when ActiveAppearance.Gender is Gender.Female:
                    sprites = Resources.LoadAll<Sprite>(Appearance.FEMALE_HAIRS);
                    break;
                case CustomizationPart.Beard when ActiveAppearance.Gender is Gender.Male:
                    sprites = Resources.LoadAll<Sprite>(Appearance.BEARDS);
                    break;
            }

            if (sprites.Length <= 0)
            {
                if (!IsDetailedCustomizationActive && current != CustomizationPart.Beard)
                {
                    return;
                }

                UI.Sweep(customizationPanel, optionsPanel, Direction.Normal, action: ClearCustomizationContent);
                return;
            }

            Direction direction = customizationPanel.localPosition.DirectionTo(optionsPanel.localPosition);
            if (direction is Direction.Normal)
            {
                PopulateCustomizationContent(sprites);
                UI.Sweep(customizationPanel, optionsPanel, Direction.Right);
                return;
            }

            if (!UI.IsTweening(customizationPanel, out _) && IsDetailedCustomizationActive)
            {
                UI.SweepTransition(customizationPanel, optionsPanel, () => PopulateCustomizationContent(sprites));
                return;
            }
            else if (IsDetailedCustomizationActive)
            {
                PopulateCustomizationContent(sprites);
                UI.Sweep(customizationPanel, optionsPanel, Direction.Right);
                return;
            }

            PopulateCustomizationContent(sprites);
            UI.Sweep(customizationPanel, optionsPanel, Direction.Right);
        }

        private void PopulateCustomizationContent(Sprite[] sprites)
        {
            ClearCustomizationContent();
            for (int i = 0; i < sprites.Length; i++)
            {
                GameObject card = CreateCustomizationCard(sprites[i]);
                Button button = card.GetComponent<Button>();
                int temp = i;
                button.onClick.AddListener(() => SetCustomizationIndex(temp));
            }
        }

        private GameObject CreateCustomizationCard(Sprite sprite)
        {
            GameObject card = Instantiate(customizationCard, customizationContent.transform);
            Image image = card.transform.GetChild(0).GetComponent<Image>();
            image.sprite = sprite;
            return card;
        }

        private void ClearCustomizationContent()
        {
            for (int i = 0; i < customizationContent.transform.childCount; i++)
            {
                Destroy(customizationContent.transform.GetChild(i).gameObject);
            }
        }

        public void Create(TMP_InputField input)
        {
            if (string.IsNullOrEmpty(input.text))
            {
                throw new ArgumentException($"{nameof(name)} cannot be empty or null");
            }

            info = new CharacterInfo(input.text, 27, ActiveAppearance.Gender, true);
            GameManager.Instance.Player = new Entity(info, ActiveAppearance);
            SceneLoader.LoadScene("Play_SampleScene");
        }
    }
}
