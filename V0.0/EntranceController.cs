using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EntranceController : MonoBehaviour {

	public void OnStartButtonClick(){
		SceneManager.LoadSceneAsync ("Hometown", LoadSceneMode.Single);
	}
		
	void Start () {
		
	}

	void Update () {
		
	}
}
