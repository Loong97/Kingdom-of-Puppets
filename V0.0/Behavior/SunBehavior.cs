using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameLogic;

public class SunBehavior : MonoBehaviour {
	public GameObject root;

	private static float[] seasonAngle = new float[4] { 45, 60, 45, 30 };
	private static float[] dateAngle = new float[7] { -65, -40, -15, 15, 40, 65, -90 };
	private const int totalFrame = 60;
	private Quaternion backAngle;
	private Quaternion targetAngle;
	private int runFrame = 0;

	void Start () {
		
	}

	void Update () {
		if (runFrame <= 0) {
			return;
		}
		float quotient = (float)runFrame / (float)totalFrame;
		transform.rotation = Quaternion.Slerp (backAngle, targetAngle, 1 - quotient);
		runFrame--;

		if (runFrame == 1) {
			root.SendMessage ("StartTurn");
		}
	}

	public void Revolve () {
		if (TimeLogic.dateIndex == 6) {
			targetAngle = Quaternion.Euler (new Vector3 (-90, -90, 0));
		}
		else {
			targetAngle = Quaternion.Euler (new Vector3 (seasonAngle [TimeLogic.seasonIndex], dateAngle [TimeLogic.dateIndex], 0));
		}
		backAngle = transform.rotation;
		runFrame = totalFrame;
	}
}
