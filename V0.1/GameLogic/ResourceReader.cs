using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public static class ResourceReader
	{
		public static Material landMat = Resources.Load("Materials/LandMat") as Material;

		public static GameObject Boy = Resources.Load("Prefabs/Girl") as GameObject;
		public static GameObject Girl = Resources.Load("Prefabs/Girl") as GameObject;

		public static GameObject Tree = Resources.Load("Prefabs/Tree") as GameObject;
		public static GameObject[] Plants = new GameObject[3] { Tree, Tree, Tree };
	}
}