using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    private int HairIndex
    {
        get => hairIndex;
        set
        {
            if (value < 0 && value >= hair.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            hairIndex = value;
            activeHair.sprite = hair[hairIndex];
        }
    }
    private int BeardIndex
    {
        get => beardIndex;
        set
        {
            if (value < 0 && value >= beards.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            beardIndex = value;
            activeBeard.sprite = beards[beardIndex];
        }
    }
    public bool IsDetailedCustomizationActive
        => current == CustomizationPart.Hair || current == CustomizationPart.Beard;

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] hair;
    [SerializeField]
    private Sprite[] beards;

    [Header("Misc")]
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

    private Image activeHair;
    private Image activeBeard;
    private Image activeEyes;
    private Image activeHead;
    private int hairIndex;
    private int beardIndex;
    private CustomizationPart current;

    private void Awake()
    {
        activeHair = GameObject.FindGameObjectWithTag("Hair").GetComponent<Image>();
        activeBeard = GameObject.FindGameObjectWithTag("Beard").GetComponent<Image>();
        activeEyes = GameObject.FindGameObjectWithTag("Eyes").GetComponent<Image>();
        activeHead = GameObject.FindGameObjectWithTag("Head").GetComponent<Image>();
    }

    private void Start()
    {
        HairIndex = 0;
        BeardIndex = 0;

        SetCustomizationOption(0);
    }

    public void SetCustomizationIndex(int index = 0)
    {
        switch (colorPicker.Image.name)
        {
            case "Hair":
                HairIndex = index;
                break;
            case "Beard":
                BeardIndex = index;
                break;
        }
    }

    public void SetCustomizationOption(int index)
    {
        switch (index)
        {
            case 0:
                colorPicker.Attach(activeHead);
                PopulateCustomizationContent();
                break;
            case 1:
                colorPicker.Attach(activeHair);
                PopulateCustomizationContent(CustomizationPart.Hair);
                break;
            case 2:
                colorPicker.Attach(activeEyes);
                PopulateCustomizationContent();
                break;
            case 3:
                colorPicker.Attach(activeBeard);
                PopulateCustomizationContent(CustomizationPart.Beard);
                break;
        }
    }

    private void PopulateCustomizationContent(CustomizationPart part = CustomizationPart.None)
    {
        switch (part)
        {
            case CustomizationPart.Hair:
                if (!UI.IsTweening(customizationPanel, out LTDescr ltd) && IsDetailedCustomizationActive)
                {
                    UI.SweepTransition(customizationPanel, optionsPanel, () => PopulateCustomizationContent(hair));
                    break;
                }
                else if (ltd == null && IsDetailedCustomizationActive)
                {
                    UI.Sweep(customizationPanel, optionsPanel, Direction.Right);
                    break;
                }

                PopulateCustomizationContent(hair);
                UI.Sweep(customizationPanel, optionsPanel, Direction.Right);
                break;
            case CustomizationPart.Beard:
                if (!UI.IsTweening(customizationPanel, out ltd) && IsDetailedCustomizationActive)
                {
                    UI.SweepTransition(customizationPanel, optionsPanel, () => PopulateCustomizationContent(beards));
                    break;
                }
                else if (ltd == null && IsDetailedCustomizationActive)
                {
                    UI.Sweep(customizationPanel, optionsPanel, Direction.Right);
                    break;
                }

                PopulateCustomizationContent(beards);
                UI.Sweep(customizationPanel, optionsPanel, Direction.Right);
                break;
            default:
                if (!IsDetailedCustomizationActive)
                {
                    break;
                }

                UI.Sweep(customizationPanel, optionsPanel, Direction.Normal, action: ClearCustomizationContent);
                break;
        }

        current = part;
    }

    private void PopulateCustomizationContent(Sprite[] content)
    {
        ClearCustomizationContent();
        for (int i = 0; i < content.Length; i++)
        {
            GameObject card = CreateCustomizationCard(content[i]);
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
        for(int i = 0; i < customizationContent.transform.childCount; i++)
        {
            Destroy(customizationContent.transform.GetChild(i).gameObject);
        }
    }
}
