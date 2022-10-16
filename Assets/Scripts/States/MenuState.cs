namespace Zomlypse.States
{
    public class MenuState : State
    {
        public override void Start()
        {
            base.Start();
            ui.Enable("CharacterCreation");
        }

        public override void Stop()
        {
            base.Stop();
            ui.Disable("CharacterCreation");
        }

        protected override void Update()
        {

        }
    }
}