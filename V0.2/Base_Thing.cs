using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Thing : MonoBehaviour
{
	protected bool controlling = false;

	protected void TieJoystick()
	{
		Read_Objects.joystick.OnDragging += GoWithJoystick;
	}

	private void GoWithJoystick(Vector2 vec)
	{
		if (controlling == false)
			transform.Translate(new Vector3(vec.x * Base_Terrain.JOYSTICKRATE, 0, vec.y * Base_Terrain.JOYSTICKRATE));
		else if (controlling == true)
			transform.LookAt(new Vector3(transform.position.x + vec.x, transform.position.y, transform.position.z + vec.y));
	}
}
