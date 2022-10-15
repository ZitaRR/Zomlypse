using UnityEngine;

namespace Zomlypse.States
{
    public class StateMachine
    {
        public static State State { get; private set; }

        public static void SetState<T>() where T : State, new()
        {
            State state = new T();
            if (State == state)
            {
                State.Continue();
                return;
            }

            State?.Stop();
            Debug.Log($"Switched from state [{State?.Name ?? "None"}] to [{state.Name}]");
            State = state;
            State.Start();
        }
    }
}
