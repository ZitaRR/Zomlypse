using System;
using UnityEngine;
using Zomlypse.Extensions;

namespace Zomlypse.Behaviours
{
    public class Game : MonoBehaviour
    {
        public static readonly DateTime startDate = new DateTime(2028, 02, 28, 14, 00, 00);

        [SerializeField]
        private bool forwardTime;
        [SerializeField]
        private float speed;

        private DateTime current;

        private void Start()
        {
            current = startDate;
            Debug.Log($"Game start: {current}");
        }

        private void FixedUpdate()
        {
            if (!forwardTime)
            {
                return;
            }

            current = current.AddMinutes(speed);
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
