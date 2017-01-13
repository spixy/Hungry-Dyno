using UnityEngine;
using System.Collections.Generic;

public class TouchController
{
    public readonly List<RectTransform> uiRectTransforms = new List<RectTransform>();

    private readonly HashSet<int> touchData = new HashSet<int>();

    /// <summary>
    /// Zisti, ci sa bod nachadza v nejakom UI objekte
    /// </summary>
    private bool IntersectsWithUI(Vector2 screenPosition)
    {
        foreach (RectTransform rectTransform in uiRectTransforms)
        {
            if (rectTransform.gameObject.activeInHierarchy && RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition))
                return true;
        }

        return false;
    }

	public bool IsTap()
	{
		int touchCount = Input.touchCount;
		bool tap = false;

		for (int i = 0; i < touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);

			switch (touch.phase)
			{
				// zaciatok dotyku
				case TouchPhase.Began:
					tap = this.HandleBegin(ref touch);
					break;

				// pohyb prstom
				case TouchPhase.Moved:
					tap = this.HandleMoved(ref touch);
					break;

				// koniec dotyku
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					this.HandleEnd(ref touch);
					break;

				// drzanie dotyku na mieste
				case TouchPhase.Stationary:
					tap = this.HandleStationary(ref touch);
					break;
			}
		}

		return tap;
	}

	private bool HandleBegin(ref Touch touch)
    {
        if (this.IntersectsWithUI(touch.position))
            return false;

        // ulozim si cas dotyku
		this.touchData.Add(touch.fingerId);

		return true;
	}

    private bool HandleMoved(ref Touch touch)
    {
        return this.touchData.Contains(touch.fingerId);
    }

    private bool HandleStationary(ref Touch touch)
    {
        return this.touchData.Contains(touch.fingerId);
    }

    private void HandleEnd(ref Touch touch)
    {
        if (!this.touchData.Contains(touch.fingerId))
            return;

        // odstranim touch z listu
        this.touchData.Remove(touch.fingerId);
    }
}
