using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act_Human : Base_Thing
{
	void Start()
	{
		TieJoystick();
		controlling = true;
	}

	void Update()
	{
		Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);
		RaycastHit hit;
		if (Read_Objects.terrain.gameObject.GetComponent<MeshCollider>().Raycast(ray, out hit, 1000))
		{
			if (hit.point.y >= 0)
			{
				transform.Translate(new Vector3(0, hit.point.y - transform.position.y, 0));
				if (Read_Objects.joystick.pointerDown == true) GetComponent<Animator>().SetInteger("State", 1);
				else GetComponent<Animator>().SetInteger("State", 0);
			}
			else
			{
				transform.Translate(new Vector3(0, -transform.position.y, 0));
				if (Read_Objects.joystick.pointerDown == true)
				{
					Read_Objects.sea.Drop(transform.position);
					GetComponent<Animator>().SetInteger("State", 3);
				}
				else GetComponent<Animator>().SetInteger("State", 2);
			}
		}
	}
}