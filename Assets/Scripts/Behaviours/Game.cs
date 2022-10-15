using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zomlypse.Enums;
using Zomlypse.Extensions;

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
        public GamePace Pace
        {
            get => pace;
            set
            {
                previous = pace;
                pace = value;
            }
        }

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI dateDisplay;
        [SerializeField]
        private TextMeshProUGUI timeDisplay;
        [SerializeField]
        private Button pauseButton;
        [SerializeField]
        private Button normalButton;
        [SerializeField]
        private Button fastButton;

        private DateTime current;
        private IFormatProvider format;
        private GamePace pace;
        private GamePace previous;

        private void Awake()
        {
            pauseButton.onClick.AddListener(Pause);
            normalButton.onClick.AddListener(() => Play());
            fastButton.onClick.AddListener(() => Play(GamePace.Fast));
        }

        private void Start()
        {
            Current = startDate;
            format = CultureInfo.CreateSpecificCulture("en-US");
        }

        private void FixedUpdate()
        {
            if (pace == GamePace.Pause)
            {
                return;
            }

            DateTime previous = Current;
            Current = Current.AddMinutes((int)pace);

            if (Current.Year > previous.Year)
            {
                GameManager.Instance.Notifications.Add(new Notification(
                    $"Year {Current.Year}",
                    $"Time has advanced, it is now year {Current.Year}."));
            }
            else if (Current.Day > previous.Day)
            {
                GameManager.Instance.Notifications.Add(new Prompt(
                    Current.DayOfWeek.ToString(),
                    "One person wants to join your group!",
                    (prompt) =>
                    {
                        if (!prompt.Success)
                        {
                            return;
                        }
                        Entity entity = new Entity();
                        GameManager.Instance.Deck.Add(entity);
                        GameManager.Instance.Notifications.Add(new Notification(
                            "A New Face",
                            $"<color=green><link=name>{entity.Info.Fullname}</link></color> have joined your group!"));
                    }));
            }
        }

        public void Play(GamePace pace = GamePace.Normal)
        {
            this.pace = pace;
        }

        public void Pause()
        {
            pace = GamePace.Pause;
        }

        public void TogglePlayPause()
        {
            Pace = previous;
        }
    }
}
