using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zomlypse.Enums;

namespace Zomlypse.Behaviours
{
    public class CharacterCreation : MonoBehaviour
    {
        public bool IsDetailedCustomizationActive
            => current == CustomizationPart.Hair || current == CustomizationPart.Beard;

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
        [SerializeField]
        private Toggle femaleToggle;

        private Appearance appearance;
        private Card card;
        private CharacterInfo info;
        private CustomizationPart current;
        private Gender gender;

        private void Awake()
        {
            card = GetComponentInChildren<Card>();

            maleToggle.onValueChanged.AddListener((value) => gender = value ? Gender.Male : Gender.None);
            femaleToggle.onValueChanged.AddListener((value) => gender = value ? Gender.Female : Gender.None);
        }

        private void Start()
        {
            appearance = Appearance.Random();
            appearance.OnChange += (appearance, _) => card.Apply(appearance);
            card.Apply(appearance);

            SetCustomizationOption(0);
        }

        public void SetCustomizationIndex(int index = 0)
        {
            appearance.Load(current, index);
        }

        public void SetCustomizationOption(int index)
        {
            switch (index)
            {
                case 0:
                    colorPicker.Attach(appearance.Head);
                    PopulateCustomizationContent();
                    current = CustomizationPart.Head;
                    break;
                case 1:
                    colorPicker.Attach(appearance.Hair);
                    PopulateCustomizationContent(CustomizationPart.Hair);
                    current = CustomizationPart.Hair;
                    break;
                case 2:
                    colorPicker.Attach(appearance.Eyes);
                    PopulateCustomizationContent();
                    current = CustomizationPart.Eyes;
                    break;
                case 3:
                    colorPicker.Attach(appearance.Beard);
                    PopulateCustomizationContent(CustomizationPart.Beard);
                    current = CustomizationPart.Beard;
                    break;
            }
        }

        private void PopulateCustomizationContent(CustomizationPart part = CustomizationPart.None)
        {
            Sprite[] sprites = new Sprite[0];
            switch (part)
            {
                case CustomizationPart.Hair:
                    sprites = Resources.LoadAll<Sprite>(Appearance.HAIRS);
                    break;
                case CustomizationPart.Beard:
                    sprites = Resources.LoadAll<Sprite>(Appearance.BEARDS);
                    break;
            }

            if (sprites.Length <= 0)
            {
                if (!IsDetailedCustomizationActive)
                {
                    return;
                }

                UI.Sweep(customizationPanel, optionsPanel, Direction.Normal, action: ClearCustomizationContent);
                return;
            }

            if (!UI.IsTweening(customizationPanel, out LTDescr ltd) && IsDetailedCustomizationActive)
            {
                UI.SweepTransition(customizationPanel, optionsPanel, () => PopulateCustomizationContent(sprites));
                return;
            }
            else if (ltd == null && IsDetailedCustomizationActive)
            {
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
                int temp = i + 1;
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

            info = new CharacterInfo(input.text, 27, gender);
            GameManager.Instance.Player = new Entity(info, appearance);
            SceneLoader.LoadScene("Play_SampleScene");
        }
    }
}
