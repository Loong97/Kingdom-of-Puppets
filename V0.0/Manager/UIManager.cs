using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using GameLogic;

public class UIManager : MonoBehaviour {

	void Start () {
		ClearActive ();
		ReadTime ();
	}

	void Update () {
		
	}

	public void ClearActive () {
		transform.Find ("CharacterGroup").gameObject.SetActive (false);
	}

	public void MakeActive (string groupName) {
		transform.Find (groupName).gameObject.SetActive (true);
	}

	public void ReadCharacter (int id) {
		MakeActive ("CharacterGroup");
		ACharacter chr = GameLogic.CharacterLogic.family [id];

		GameObject photo = transform.Find ("CharacterGroup").Find ("CharacterPhoto").gameObject;
		if (chr.gender) {
			int index = 0;
			photo.SendMessage ("ChangePhoto", index);
		}
		else if (!chr.gender) {
			int index = 1;
			photo.SendMessage ("ChangePhoto", index);
		}

		string _text = "  " + chr.familyName + "  " + chr.givenName + "  " + chr.age + "岁" + "  " + chr.profession + "  " + chr.mission.name;
		transform.Find ("CharacterGroup").Find ("CharacterInfomation").Find ("Text").gameObject.GetComponent<Text> ().text = _text;
	}

	public void ReadTime () {
		string _text = TimeLogic.year.ToString () + "年" + System.Environment.NewLine + TimeLogic.season + "." + TimeLogic.date + System.Environment.NewLine + "下一回合";
		transform.Find ("NextTurn").Find ("Text").gameObject.GetComponent<Text> ().text = _text;
	}
}
