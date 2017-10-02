using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameLogic;

public class CharacterBehavior : MonoBehaviour {
	private const float animationSpeed = 3.0f;
	private const float moveSpeed = 0.05f;
	private bool meArrived = true;
	private Vector3 meTarget;

	void Start () {
		
	}

	void Update () {
		if (!meArrived) {
			Vector3 _ori = transform.position;
			Vector3 _tar = meTarget;
			float distance = Vector3.Distance (_ori, _tar);
			Vector3 movement = new Vector3 (_tar.x - _ori.x, _tar.y - _ori.y, _tar.z - _ori.z);
			if (moveSpeed <= distance) {
				movement *= moveSpeed / distance;
				transform.position += movement;
			}
			else {
				transform.position += movement;
				meArrived = true;
				GetComponent<Animation> ().Stop ();
				LookAtHorizontal (Camera.main.transform.position);
			}
		}
	}

	public void CharacterAwait () {
		LookAtHorizontal (Camera.main.transform.position);
	}

	public void CharacterGoto (int[] target) {
		meTarget = TerrainLogic.GetPosition (target[0], target[1]);
		LookAtHorizontal (meTarget);
		GetComponent<Animation> () ["PtArma|PtWalk"].speed = animationSpeed;
		GetComponent<Animation> ().Play ("PtArma|PtWalk");
		meArrived = false;
	}

	private void LookAtHorizontal (Vector3 target) {
		Vector3 _target = new Vector3 (target.x, transform.position.y, target.z);
		transform.LookAt (_target, Vector3.up);
	}
}
