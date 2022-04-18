using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    }

    private void UpdateColor()
    {
        image.color = new Color(red.value, green.value, blue.value);
    }
}
