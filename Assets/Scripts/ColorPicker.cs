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
        char[] chars = image.name.ToUpper().ToCharArray();
        header.text = string.Join(" ", chars);

        red.SetValueWithoutNotify(this.image.color.r);
        green.SetValueWithoutNotify(this.image.color.g);
        blue.SetValueWithoutNotify(this.image.color.b);
    }

    private void UpdateColor()
    {
        image.color = new Color(red.value, green.value, blue.value);
    }
}
