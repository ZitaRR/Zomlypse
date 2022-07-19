using UnityEngine;
using Zomlypse.Extensions;

namespace Zomlypse.Helpers
{
    public class NameInput : EnsureInput
    {
        [SerializeField]
        private int amountOfSpacing = 1;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Ensure(string input)
        {
            if (string.IsNullOrEmpty(input) ||
                input.CountChar(' ') > amountOfSpacing)
            {
                button.interactable = false;
                return;
            }

            button.interactable = true;
        }
    }
}
