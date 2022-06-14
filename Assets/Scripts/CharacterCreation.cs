using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
    private int HairIndex
    {
        get => hairIndex;
        set
        {
            if (value < 0)
            {
                hairIndex = hair.Length - 1;
            }
            else if (value >= hair.Length)
            {
                hairIndex = 0;
            }
            else
            {
                hairIndex = value;
            }

            activeHair.sprite = hair[hairIndex];
        }
    }
    private int BeardIndex
    {
        get => beardIndex;
        set
        {
            if (value < 0)
            {
                beardIndex = beards.Length - 1;
            }
            else if (value >= beards.Length)
            {
                beardIndex = 0;
            }
            else
            {
                beardIndex = value;
            }

            activeBeard.sprite = beards[beardIndex];
        }
    }

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] hair;
    [SerializeField]
    private Sprite[] beards;

    [Header("Misc")]
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private ColorPicker colorPicker;
    [SerializeField]
    private TextMeshProUGUI customizationHeader;

    private Image activeHair;
    private Image activeBeard;
    private Image activeEyes;
    private Image activeHead;
    private int hairIndex;
    private int beardIndex;

    private void Awake()
    {
        activeHair = GameObject.FindGameObjectWithTag("Hair").GetComponent<Image>();
        activeBeard = GameObject.FindGameObjectWithTag("Beard").GetComponent<Image>();
        activeEyes = GameObject.FindGameObjectWithTag("Eyes").GetComponent<Image>();
        activeHead = GameObject.FindGameObjectWithTag("Head").GetComponent<Image>();
    }

    private void Start()
    {
        dropdown.onValueChanged.AddListener(SetCustomizationOption);

        HairIndex = 0;
        BeardIndex = 0;

        SetCustomizationOption(0);
    }

    public void SetCustomizationIndex(int index = 1)
    {
        switch (colorPicker.Image.name)
        {
            case "Hair":
                HairIndex += index;
                break;
            case "Beard":
                BeardIndex += index;
                break;
        }
    }

    public void SetCustomizationOption(int index)
    {
        switch (index)
        {
            case 0:
                colorPicker.Attach(activeHead);
                customizationHeader.transform.parent.gameObject.SetActive(false);
                break;
            case 1:
                colorPicker.Attach(activeHair);
                customizationHeader.transform.parent.gameObject.SetActive(true);
                break;
            case 2:
                colorPicker.Attach(activeEyes);
                customizationHeader.transform.parent.gameObject.SetActive(false);
                break;
            case 3:
                colorPicker.Attach(activeBeard);
                customizationHeader.transform.parent.gameObject.SetActive(true);
                break;
        }

        customizationHeader.text = colorPicker.Image.name.ToHeader();
    }
}
