using UnityEngine;

namespace Zomlypse.States
{
    public class PlayState : State
    {
        public override void Start()
        {
            base.Start();
            game.Play();
        }

        public override void Stop()
        {
            base.Stop();
            game.Pause();
        }

        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                game.TogglePlayPause();
            }
        }
    }
}