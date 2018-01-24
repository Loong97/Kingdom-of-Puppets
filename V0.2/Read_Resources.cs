using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Read_Resources : MonoBehaviour
{
	public static Material landMat = Resources.Load("Materials/LandMat") as Material;
	public static Material waterMat = Resources.Load("Materials/WaterMat") as Material;
	public static GameObject Boy = Resources.Load("Prefabs/Boy") as GameObject;
}
