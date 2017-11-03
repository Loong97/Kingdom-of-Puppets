using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using GameLogic;

public class RootManager : MonoBehaviour
{
	public GameObject mainCanvas;
	public GameObject sun;

	void Awake()
	{
		NewGame();
	}

	void Start()
	{

	}

	void Update()
	{
		if (!Input.GetMouseButton(0))
		{
			return;
		}
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (ActionLogic.selChr == null)
		{
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.name.Contains("Chr"))
				{
					ActionLogic.Pick(hit);
				}
				else {
					//DragCamera();
				}
			}
			else {
				//DragCamera();
			}
		}

		else {
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.name == ActionLogic.selChr.flesh.name)
				{
					return;
				}
				else if (hit.collider.gameObject.name.Contains("Chr"))
				{
					ActionLogic.Pick(hit);
				}
				else if (hit.collider.gameObject.name == "Terrain")
				{
					if (hit.point.y > 0)
					{
						ActionLogic.selChr.Goto(hit.point);
						ActionLogic.ClearSelection();
					}
					else {
						return;
					}
				}
			}
		}
	}

	private void NewGame()
	{
		TimeLogic.NewGame();
		TerrainLogic.NewGame();
		CharacterLogic.NewGame();
		PlantLogic.NewGame();
	}
}
