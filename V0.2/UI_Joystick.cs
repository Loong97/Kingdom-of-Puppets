using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Joystick : Base_UI, IPointerDownHandler, IPointerUpHandler
{
	public delegate void Dragging(Vector2 vec);
	public event Dragging OnDragging;
	public bool pointerDown = false;

	private RectTransform area;
	private RectTransform stick;

	void Start()
	{
		area = GetComponent<RectTransform>();
		stick = GameObject.Find("JoyStick").GetComponent<RectTransform>();
		OnDragging += DragSticker;
	}

	void Update()
	{
		if (pointerDown == false) return;
		float x = Input.mousePosition.x - area.position.x;
		float y = Input.mousePosition.y - area.position.y;
		Vector2 vec = new Vector2(x, y);
		if (vec.magnitude >= area.rect.height / 2)
		{
			vec.Normalize();
			vec *= area.rect.height / 2;
		}
		if (this.OnDragging != null) this.OnDragging(vec);
	}

	public void OnPointerDown(PointerEventData data)
	{
		pointerDown = true;
	}

	public void OnPointerUp(PointerEventData data)
	{
		pointerDown = false;
		stick.Translate(-stick.anchoredPosition);
	}

	private void DragSticker(Vector2 vec)
	{
		stick.Translate(new Vector3(vec.x - stick.anchoredPosition.x, vec.y - stick.anchoredPosition.y, 0));
	}
}