using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PhotoBehavior : MonoBehaviour {
	public List<Texture> photos=new List<Texture>();

	void Start () {
		
	}

	void Update () {
		
	}

	public void ChangePhoto (int index) {
		GetComponent<RawImage> ().texture = photos [index];
	}
}
