using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using GameLogic;

public class RootManager : MonoBehaviour {
	public GameObject mainCanvas;
	public GameObject sun;

	void Awake () {
		TimeLogic.SetTime (1, 0, 0);
		TerrainLogic.Clear ();
		TerrainLogic.ReadMap ();
		CharacterLogic.Clear ();
		CharacterLogic.ReadFamilyTree ();
	}
		
	void Start () {
		TerrainLogic.Initialize ();
		CharacterLogic.Initialize ();
	}
		
	void Update () {
		if (!Input.GetMouseButton (0)) {
			return;
		}
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (ActionLogic.selChr==null) {
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.name.Contains ("Chr")) {
					Pick (hit);
				} else {
					DragCamera ();
				}
			} else {
				DragCamera ();
			}
		}

		else {
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.name == ActionLogic.selChr.flesh.name) {
					return;
				} else if (hit.collider.gameObject.name.Contains ("Chr")) {
					Pick (hit);
				} else if (hit.collider.gameObject.name.Contains ("Cloud")) {
					return;
				} else if (hit.collider.gameObject.name.Contains ("Plot")) {
					if (TerrainLogic.Find (hit.collider.gameObject.name).occupied == false) {
						ActionLogic.selChr.Goto(hit.collider.name);
						ClearSelection ();;
					} else {
						return;
					}
				}
			}
		}
	}

	private void DragCamera () {
		float x = -Input.GetAxis ("Mouse X");
		float y = -Input.GetAxis ("Mouse Y");
		Vector2 drag = new Vector2 (x, y);
		Camera.main.SendMessage ("DragCamera", drag);
	}

	private void Pick (RaycastHit hit) {
		ClearSelection ();
		RenewSelection (hit.collider.gameObject);
		ActionLogic.selChr.Await();
		mainCanvas.SendMessage ("ReadCharacter", ActionLogic.selChr.id);
	}

	private void ClearSelection () {
		ActionLogic.selChr = null;
		mainCanvas.SendMessage ("ClearActive");
	}

	public static void RenewSelection (GameObject s) {
		ActionLogic.selChr = CharacterLogic.Find (s.name);
	}

	public void ActionExplore () {
		ActionLogic.selChr.mission = new AMission ("探索周边", 1);
		ActionLogic.AddExploreList ();
	}

	public void EndTurn () {
		ClearSelection ();
		TimeLogic.NextTurn ();
		sun.SendMessage ("Revolve");
	}

	public void StartTurn () {
		mainCanvas.SendMessage ("ReadTime");
		ActionLogic.DowncountMissions ();
		ActionLogic.DoExploreList ();
	}
}