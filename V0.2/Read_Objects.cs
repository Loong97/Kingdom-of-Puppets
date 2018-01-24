using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Read_Objects : MonoBehaviour
{
	public Env_Root Root;
	public Env_Terrain Terrain;
	public Env_Sea Sea;
	public UI_Joystick Joystick;

	public static Env_Root root;
	public static Env_Terrain terrain;
	public static Env_Sea sea;
	public static UI_Joystick joystick;

	void Awake()
	{
		root = Root;
		terrain = Terrain;
		sea = Sea;
		joystick = Joystick;
	}
}