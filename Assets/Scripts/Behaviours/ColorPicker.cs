using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zomlypse.Enums;
using Zomlypse.Extensions;

namespace Zomlypse.Behaviours
{
    public class ColorPicker : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI header;
        [SerializeField]
        private Slider red;
        [SerializeField]
        private Slider green;
        [SerializeField]
        private Slider blue;

        private AppearancePart part;
        private TextMeshProUGUI redText;
        private TextMeshProUGUI greenText;
        private TextMeshProUGUI blueText;

        private void Awake()
        {
            redText = red.GetComponentInChildren<TextMeshProUGUI>();
            greenText = green.GetComponentInChildren<TextMeshProUGUI>();
            blueText = blue.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            red.onValueChanged.AddListener(delegate { UpdateColor(); });
            green.onValueChanged.AddListener(delegate { UpdateColor(); });
            blue.onValueChanged.AddListener(delegate { UpdateColor(); });
        }

        public void Attach(AppearancePart part)
        {
            this.part = part;
            header.text = part.Sprite.name.ToHeader();

            red.SetValueWithoutNotify(part.Color.r);
            green.SetValueWithoutNotify(part.Color.g);
            blue.SetValueWithoutNotify(part.Color.b);

            UpdateColorText();
        }

        private void UpdateColor()
        {
            part.Color = new Color(red.value, green.value, blue.value);
            UpdateColorText();
        }

        private void UpdateColorText()
        {
            redText.text = (part.Color.r * 255).ToString("F0");
            greenText.text = (part.Color.g * 255).ToString("F0");
            blueText.text = (part.Color.b * 255).ToString("F0");
        }
    }
}
