using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_Root : MonoBehaviour
{
	void Start()
	{
		NewGame();
	}

	private void NewGame() {
		Read_Objects.terrain.NewGame();
		Read_Objects.sea.NewGame();
	}
}
