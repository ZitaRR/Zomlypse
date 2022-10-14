using System;
using Zomlypse.Behaviours;
using Zomlypse.Singletons;

namespace Zomlypse.States
{
    public abstract class State : IEquatable<State>
    {
        public State Previous { get; }
        public string Name { get; }
        public bool IsActive { get; private set; } = false;

        protected GameManager manager;
        protected Game game;

        public State()
        {
            Previous = StateMachine.State;
            Name = GetType().Name.Replace(nameof(State), "");

            manager = GameManager.Instance;
            game = manager.GetComponent<Game>();
        }

        public virtual void Start()
        {
            if (IsActive)
            {
                return;
            }

            GameManager.OnFrame += Update;
            IsActive = true;
        }

        public virtual void Continue()
        {
            if (IsActive)
            {
                return;
            }

            GameManager.OnFrame += Update;
            IsActive = true;
        }

        public virtual void Stop()
        {
            if (!IsActive)
            {
                return;
            }

            GameManager.OnFrame -= Update;
            IsActive = false;
        }

        protected abstract void Update();

        public bool Equals(State state)
        {
            return Name == state?.Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as State);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(State lhs, State rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return false;
            }
            else if (ReferenceEquals(rhs, null))
            {
                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(State lhs, State rhs)
        {
            if (ReferenceEquals(lhs, null))
            {
                return false;
            }
            else if (ReferenceEquals(rhs, null))
            {
                return false;
            }

            return !lhs.Equals(rhs);
        }
    }
}