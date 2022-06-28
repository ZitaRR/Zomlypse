using UnityEngine;

namespace Zomlypse.States
{
    public class PlayState : State
    {
        public override void Start()
        {
            base.Start();
            UI.Enable("Play");
            game.Play();
        }

        public override void Stop()
        {
            base.Stop();
            UI.Disable("Play");
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