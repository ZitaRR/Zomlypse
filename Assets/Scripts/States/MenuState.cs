namespace Zomlypse.States
{
    public class MenuState : State
    {
        public override void Start()
        {
            base.Start();
            UI.Enable("CharacterCreation");
        }

        public override void Stop()
        {
            base.Stop();
            UI.Disable("CharacterCreation");
        }

        protected override void Update()
        {

        }
    }
}