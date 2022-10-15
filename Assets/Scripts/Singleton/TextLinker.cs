using System;
using TMPro;
using UnityEngine;
using Zomlypse.Behaviours;
using Zomlypse.Enums;
using Zomlypse.Extensions;

namespace Zomlypse.Singleton
{
    [Serializable]
    public class TextLinker : Singleton
    {
        [SerializeField]
        private TextColor characterColor;

        public override void Initialize(GameManager _)
        {

        }

        public string CharacterLink(CharacterInfo info)
        {
            return $"<color={characterColor.Format()}><link={info.Id}>{info.Fullname}</link></color>";
        }

        public string CharacterLink(Entity entity)
        {
            return CharacterLink(entity.Info);
        }

        public Entity GetLinkCharacter(TextMeshProUGUI text)
        {
            return null;
        }

        private TMP_LinkInfo? GetLink(TextMeshProUGUI text)
        {
            int index = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (index == -1)
            {
                return null;
            }

            return text.textInfo.linkInfo[index];
        }
    }
}
