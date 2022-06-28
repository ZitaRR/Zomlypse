using System.Collections;
using UnityEngine;

namespace Zomlypse.States
{
    public class LoadState : State
    {
        public override void Start()
        {
            base.Start();
            UI.DisableAll();
        }

        protected override void Update()
        {

        }
    }
}