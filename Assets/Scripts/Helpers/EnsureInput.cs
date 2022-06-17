using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zomlypse.Helpers
{
    public class EnsureInput : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField input;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            input.onValueChanged.AddListener((input) => Ensure(input));
            Ensure(input.text);
        }

        private void Ensure(string input)
        {
            button.interactable = !string.IsNullOrEmpty(input);
        }
    }
}
