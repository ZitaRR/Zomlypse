using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UI 
{
    public static void Move(RectTransform child, RectTransform parent, Direction direction)
    {
        Vector3 position = parent.localPosition;
        switch (direction)
        {
            case Direction.Normal:
                position = parent.localPosition;
                break;
            case Direction.Up:
                position.y = parent.rect.yMax + (child.rect.height / 2);
                break;
            case Direction.Right:
                position.x = parent.offsetMax.x + (child.rect.width / 2);
                break;
            case Direction.Down:
                position.y = parent.rect.yMin - (child.rect.height / 2);
                break;
            case Direction.Left:
                position.x = parent.offsetMin.x - (child.rect.width / 2);
                break;
        }

        LeanTween.move(child, position, 1f).setEase(LeanTweenType.easeInOutBack);
    }
}
