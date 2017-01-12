using System;
using UnityEngine;
using System.Collections.Generic;

public class TouchController : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] uiRectTransforms;

    [SerializeField]
    private float holdDelay = 1f;

    [SerializeField]
    private float minimumMovementPx = 1f;

    private struct TouchInfo
    {
        public float startTime;
        public bool hasMoved;
        public Vector2 startPos;
    }

    private readonly Dictionary<int, TouchInfo> touchData = new Dictionary<int, TouchInfo>();

    public bool isTap { get; private set; }

    public bool isHold { get; private set; }

    public Action<Vector2> onMove { get; set; }

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

	/// <summary>
	/// Handlovanie touch inputu
	/// </summary>
	private void Update()
	{
		this.isTap = false;

		int touchCount = Input.touchCount;

		for (int i = 0; i < touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);

			switch (touch.phase)
			{
				// zaciatok dotyku
				case TouchPhase.Began:
					this.HandleBegin(ref touch);
					break;

				// pohyb prstom
				case TouchPhase.Moved:
					this.HandleMoved(ref touch);
					break;

				// koniec dotyku
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					this.HandleEnd(ref touch);
					break;

				// drzanie dotyku na mieste
				case TouchPhase.Stationary:
					this.HandleStationary(ref touch);
					break;
			}
		}
	}

	private void HandleBegin(ref Touch touch)
    {
        if (this.IntersectsWithUI(touch.position))
            return;

        // ulozim si cas dotyku
        this.touchData[touch.fingerId] = new TouchInfo
        {
            startTime = Time.time,
            startPos = touch.position
        };
    }

    private void HandleMoved(ref Touch touch)
    {
        TouchInfo touchInfo;

        if (!this.touchData.TryGetValue(touch.fingerId, out touchInfo))
            return;

        float delta = Time.deltaTime / touch.deltaTime;

        Vector2 deltaPosition = touch.deltaPosition;
        deltaPosition.x /= Screen.currentResolution.width * delta;
        deltaPosition.y /= Screen.currentResolution.height * delta;

        // ak je pohyb vecsi ako minimalny
        if (!this.isHold && Vector2.Distance(touchInfo.startPos, touch.position) > this.minimumMovementPx)
        {
            touchInfo.hasMoved = true;
            this.touchData[touch.fingerId] = touchInfo;

            if (this.onMove != null)
                this.onMove(deltaPosition);
        }
        else
        {
            this.HandleStationary(ref touch);
        }
    }

    private void HandleStationary(ref Touch touch)
    {
        TouchInfo touchInfo;

        if (this.isHold || !this.touchData.TryGetValue(touch.fingerId, out touchInfo))
            return;

        // ak presiel urcity cas, zaregistrujem drzanie
        if (!touchInfo.hasMoved && Time.time - touchInfo.startTime > this.holdDelay)
        {
            this.isHold = true;
        }
    }

    private void HandleEnd(ref Touch touch)
    {
        TouchInfo touchInfo;

        if (!this.touchData.TryGetValue(touch.fingerId, out touchInfo))
            return;

        if (!this.isHold && !touchInfo.hasMoved)
        {
            this.isTap = true;
        }

        this.isHold = false;

        // odstranim touch z listu
        this.touchData.Remove(touch.fingerId);
    }

	public void Reset()
	{
		isTap = false;
		isHold = false;
	}
}
