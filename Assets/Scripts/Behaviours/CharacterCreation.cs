using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zomlypse.Enums;
using Zomlypse.Extensions;
using Zomlypse.Singleton;

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
            => previous == CustomizationPart.Hair || (previous == CustomizationPart.Beard && ActiveAppearance.Gender is Gender.Male);

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

        private UI ui;
        private Appearance maleAppearance;
        private Appearance femaleAppearance;
        private Card card;
        private CharacterInfo info;
        private CustomizationPart current;
        private CustomizationPart previous;
        private Sprite[] sprites;

        private void Awake()
        {
            ui = GameManager.Instance.UserInterface;
            card = GetComponentInChildren<Card>();

            maleToggle.onValueChanged.AddListener((_) =>
            {
                SetCustomizationOption(current);
                card.Apply(ActiveAppearance);
            });
        }

        private void Start()
        {
            maleAppearance = Appearance.Random(Gender.Male);
            femaleAppearance = Appearance.Random(Gender.Female);
            maleAppearance.OnChange += UpdateAppearance;
            femaleAppearance.OnChange += UpdateAppearance;

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
            previous = current;
            current = part;

            switch (current)
            {
                case CustomizationPart.Head:
                    colorPicker.Attach(ActiveAppearance.Head);
                    PopulateCustomizationContent();
                    break;
                case CustomizationPart.Hair:
                    colorPicker.Attach(ActiveAppearance.Hair);
                    PopulateCustomizationContent();
                    break;
                case CustomizationPart.Eyes:
                    colorPicker.Attach(ActiveAppearance.Eyes);
                    PopulateCustomizationContent();
                    break;
                case CustomizationPart.Beard:
                    colorPicker.Attach(ActiveAppearance.Beard);
                    PopulateCustomizationContent();
                    break;
            }
        }

        private void PopulateCustomizationContent()
        {
            sprites = new Sprite[0];
            switch (current)
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

                ui.Sweep(customizationPanel, optionsPanel, Direction.Normal, action: ClearCustomizationContent);
                return;
            }

            Direction direction = customizationPanel.localPosition.DirectionTo(optionsPanel.localPosition);
            if (direction is Direction.Normal)
            {
                PopulateCustomizationContent(sprites);
                ui.Sweep(customizationPanel, optionsPanel, Direction.Right);
                return;
            }

            if (!ui.IsTweening(customizationPanel, out _) && IsDetailedCustomizationActive)
            {
                ui.SweepTransition(customizationPanel, optionsPanel, () => PopulateCustomizationContent(sprites));
                return;
            }
            else if (IsDetailedCustomizationActive)
            {
                PopulateCustomizationContent(sprites);
                ui.Sweep(customizationPanel, optionsPanel, Direction.Right);
                return;
            }

            PopulateCustomizationContent(sprites);
            ui.Sweep(customizationPanel, optionsPanel, Direction.Right);
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

            if (current is CustomizationPart.Hair)
            {
                image.color = ActiveAppearance.Hair.Color;
            }
            else if (current is CustomizationPart.Beard)
            {
                image.color = ActiveAppearance.Beard.Color;
            }

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

        private void UpdateAppearance(Appearance appearance, AppearancePart part)
        {
            if (sprites.Length > 0)
            {
                PopulateCustomizationContent(sprites);
            }

            card.Apply(appearance);
        }
    }
}
