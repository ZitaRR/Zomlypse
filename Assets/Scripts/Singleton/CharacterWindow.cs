using System;
using TMPro;
using UnityEngine;
using Zomlypse.Behaviours;

namespace Zomlypse.Singleton
{
    [Serializable]
    public class CharacterWindow : Singleton
    {
        [SerializeField]
        private TextMeshProUGUI name;
        [SerializeField]
        private Card card;

        private UI ui;

        public override void Initialize(GameManager manager)
        {
            ui = manager.UserInterface;
        }

        public void Enable(Entity entity)
        {
            ui.Enable("CharacterWindow");
            name.text = entity.Info.Fullname;
            card.Apply(entity.Appearance);
        }

        public void Disable()
        {
            ui.Disable("CharacterWindow");
        }
    }
}
