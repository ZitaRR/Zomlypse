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
            else if (Input.GetKeyDown(KeyCode.N))
            {
                manager.Notifications.Add(new Notification(
                    "Notification",
                    "This is a notification!"));
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                manager.Notifications.Add(new Prompt(
                    "Prompt",
                    "This is a prompt!",
                    (prompt) =>
                    {
                        string outcome = prompt.Success
                            ? "Prompt was accepted!"
                            : "Prompt was declined!";
                        manager.Notifications.Add(new Notification(
                            "Notification",
                            outcome));
                    }));
            }
        } 
    }
}