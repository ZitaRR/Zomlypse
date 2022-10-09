using UnityEngine;

namespace Zomlypse.Interfaces
{
    public interface INotification
    {
        string Header { get; }
        string Message { get; }
        RectTransform Rect { get; set; }
        float Time { get; }

        void Update(float deltaTime);
        bool HasExpired();
    }
}
