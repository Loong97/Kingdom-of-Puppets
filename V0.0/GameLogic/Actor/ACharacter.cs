using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public class ACharacter {
		public string immortalGroup = CharacterLibrary.ImmortalGroup[0];
		public bool isAlive = true;
		public int id = 0;
		public string familyName = "";
		public string givenName = "";
		public bool gender = true;
		public int birthday = 0;
		public int lifespan = 75;
		public int age = 0;
		public string ageGroup = CharacterLibrary.AgeGroup[0];
		public string profession= CharacterLibrary.Profession[0];
		public GameObject flesh;
		public int[] location = new int[2];
		public AMission mission = new AMission ();

		public ACharacter (string ig , bool ia, int i, string fn, string gn, bool gd, int ls, int a, string ag, string pf) {
			immortalGroup = ig;
			isAlive = ia;
			id = i;
			familyName = fn;
			givenName = gn;
			gender = gd;
			birthday = TimeLogic.seasonIndex * 7 + TimeLogic.dateIndex;
			lifespan = ls;
			age = a;
			ageGroup = ag;
			profession = pf;
		}

		public ACharacter (int i, string fn, string gn, bool gd) {
			immortalGroup = CharacterLibrary.ImmortalGroup [0];
			isAlive = true;
			id = i;
			familyName = fn;
			givenName = gn;
			gender = gd;
			birthday = TimeLogic.seasonIndex * 7 + TimeLogic.dateIndex;
			lifespan = 75;
			age = 0;
			ageGroup = CharacterLibrary.AgeGroup[0];
			profession = CharacterLibrary.Profession[0];
		}

		public void OntoStage () {
			GameObject.Destroy (flesh);

			if (gender) {
				flesh = GameObject.Instantiate (ResourceReader.Man);
			}else {
				flesh = GameObject.Instantiate (ResourceReader.Wom);
			}

			RenewLocation (63 + id, 64);

			flesh.name = "Chr" + id.ToString ();
			flesh.transform.position = TerrainLogic.GetPosition (location[0], location[1]);
			flesh.AddComponent<SphereCollider> ().center = new Vector3 (0, 0.8f, 0);
			flesh.GetComponent<SphereCollider> ().radius = 0.8f;
			flesh.AddComponent<CharacterBehavior> ();
		}
		
		public void Await () {
			mission.Await ();
			flesh.SendMessage ("CharacterAwait");
		}

		public void Goto (string plotName) {
			APlot plot = TerrainLogic.Find (plotName);
			ActionLogic.SetOccupied (location [0], location [1], false);
			RenewLocation (plot.indx, plot.indz);

			mission = new AMission ("移动至" + " " + location [0].ToString () + "," + location [1].ToString (), 0);

			ActionLogic.SetOccupied (location [0], location [1], true);
			flesh.SendMessage ("CharacterGoto", location);
		}

		public void RenewLocation (int x, int z) {
			location [0] = x;
			location [1] = z;
		}

		public void Aging () {
			if (birthday != TimeLogic.seasonIndex * 7 + TimeLogic.dateIndex) {
				return;
			}
			age++;
			if (age > lifespan) {
				isAlive = false;
			}
			for (int i = 0; i < CharacterLibrary.AgeGroupFlag.Length; i++) {
				if (age >= CharacterLibrary.AgeGroupFlag [i]) {
					ageGroup = CharacterLibrary.AgeGroup [i];
				}
			}
		}
	}
}