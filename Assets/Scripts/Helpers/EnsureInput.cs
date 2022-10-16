using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zomlypse.Helpers
{
    public class EnsureInput : MonoBehaviour
    {
        [SerializeField]
        protected TMP_InputField input;
          
        protected Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }

        protected virtual void Start()
        {
            input.onValueChanged.AddListener((input) => Ensure(input));
            Ensure(input.text);
        }

        protected virtual void Ensure(string input)
        {
            button.interactable = !string.IsNullOrEmpty(input);
        }
    }
}
