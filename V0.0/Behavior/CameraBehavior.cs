using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameLogic;

public class CameraBehavior : MonoBehaviour {
	private float height = 10;
	private Vector3 focusBase = new Vector3 (0, 0, 0);
	private Vector3 focus = new Vector3 (0, 0, 0);

	void Start () {
		SetFocus ();
		transform.position = new Vector3 (focus.x, focus.y + height, focus.z - height);
	}

	void Update () {
		
	}

	public void DragCamera (Vector2 drag) {
		focusBase.x += drag.x;
		focusBase.z += drag.y;
		SetFocus ();
		transform.position = new Vector3 (focus.x, focus.y + height, focus.z - height);
	}

	private void SetFocus () {
		int y = TerrainLogic.GetHeight (focusBase);
		focus.x = focusBase.x;
		focus.y = y;
		focus.z = focusBase.z;
	}
}
