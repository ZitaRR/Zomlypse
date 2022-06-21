using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zomlypse.Extensions;

namespace Zomlypse.Behaviours
{
    public class ColorPicker : MonoBehaviour
    {
        public Image Image => image;

        [SerializeField]
        private TextMeshProUGUI header;
        [SerializeField]
        private Slider red;
        [SerializeField]
        private Slider green;
        [SerializeField]
        private Slider blue;

        private Image image;
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

        public void Attach(Image image)
        {
            this.image = image;
            header.text = Image.name.ToHeader();

            red.SetValueWithoutNotify(Image.color.r);
            green.SetValueWithoutNotify(Image.color.g);
            blue.SetValueWithoutNotify(Image.color.b);

            UpdateColorText();
        }

        private void UpdateColor()
        {
            image.color = new Color(red.value, green.value, blue.value);
            UpdateColorText();
        }

        private void UpdateColorText()
        {
            redText.text = (image.color.r * 255).ToString("F0");
            greenText.text = (image.color.g * 255).ToString("F0");
            blueText.text = (image.color.b * 255).ToString("F0");
        }
    }
}
