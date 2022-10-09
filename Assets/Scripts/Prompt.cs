using System;
using UnityEngine;
using Zomlypse.Behaviours;
using Zomlypse.Interfaces;

namespace Zomlypse
{
    public class Prompt : INotification, IPrompt
    {
        public string Header { get; }
        public string Message { get; }
        public RectTransform Rect { get; set; }
        public float Time { get; private set; }
        public bool Success { get; set; }
        public Action<IPrompt> OnInput { get; }

        public Prompt(string header, string message, Action<IPrompt> onInput = null)
        {
            Header = header;
            Message = message;
            OnInput = onInput;
        }

        public void Update(float deltaTime)
        {
            Time += deltaTime;
        }

        public bool HasExpired()
        {
            return Time >= GameManager.Instance.Notifications.PromptLifeTime;
        }

        public void Delete()
        {
            UnityEngine.Object.Destroy(Rect.gameObject);
            OnInput?.Invoke(this);
        }
    }
}
