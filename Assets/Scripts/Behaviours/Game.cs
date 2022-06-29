using System;
using UnityEngine;
using TMPro;
using Zomlypse.Extensions;
using System.Globalization;

namespace Zomlypse.Behaviours
{
    public class Game : MonoBehaviour
    {
        public static readonly DateTime startDate = new DateTime(2028, 02, 28, 14, 00, 00);

        public DateTime Current
        {
            get => current;
            set
            {
                current = value;
                dateDisplay.text = current.FormatDate(format);
                timeDisplay.text = current.FormatTime(format);
            }
        }

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI dateDisplay;
        [SerializeField]
        private TextMeshProUGUI timeDisplay;

        [Header("Misc")]
        [SerializeField]
        private bool forwardTime;
        [SerializeField]
        private float speed;

        private DateTime current;
        private IFormatProvider format;

        private void Start()
        {
            Current = startDate;
            format = CultureInfo.CreateSpecificCulture("en-US");
        }

        private void FixedUpdate()
        {
            if (!forwardTime)
            {
                return;
            }

            Current = Current.AddMinutes(speed);
        }

        public void Play()
        {
            forwardTime = true;
        }

        public void Pause()
        {
            forwardTime = false;
        }

        public void TogglePlayPause()
        {
            forwardTime = !forwardTime;
        }
    }
}
