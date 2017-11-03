using UnityEngine;
using System.Collections;

public class JoyStick3D : MonoBehaviour
{

	private JoyStick js;

	void Start()
	{
		js = Object.FindObjectOfType<JoyStick>();
		js.OnJoyStickTouchBegin += OnJoyStickBegin;
		js.OnJoyStickTouchMove += OnJoyStickMove;
		js.OnJoyStickTouchEnd += OnJoyStickEnd;
	}


	void OnJoyStickBegin(Vector2 vec)
	{
		Debug.Log("开始触摸虚拟摇杆");
	}

	void OnJoyStickMove(Vector2 vec)
	{
		Debug.Log("正在移动虚拟摇杆");

		//设置角色朝向
		Quaternion q = Quaternion.LookRotation(new Vector3(vec.x, 0, vec.y));
		transform.rotation = q;

		//移动角色并播放奔跑动画
		transform.Translate(Vector3.forward * 0.75f * Time.deltaTime);
		GetComponent<Animator>().SetInteger("AniState", 1);
	}

	void OnJoyStickEnd()
	{
		Debug.Log("触摸移动摇杆结束");

		//播放默认待机动画
		GetComponent<Animator>().SetInteger("AniState", 0);
	}

	void OnGUI()
	{
		GUI.Label(new Rect(30, 30, 200, 30), "3D模式下的虚拟摇杆测试");
	}
}