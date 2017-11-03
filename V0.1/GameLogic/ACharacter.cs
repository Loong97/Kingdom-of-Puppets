using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public class ACharacter
	{
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
		public string profession = CharacterLibrary.Profession[0];
		public GameObject flesh;
		//public AMission mission = new AMission();

		public ACharacter(string ig, bool ia, int i, string fn, string gn, bool gd, int ls, int a, string ag, string pf)
		{
			immortalGroup = ig;
			isAlive = ia;
			id = i;
			familyName = fn;
			givenName = gn;
			gender = gd;
			birthday = TimeLogic.season * TimeLogic.dateCount + TimeLogic.date;
			lifespan = ls;
			age = a;
			ageGroup = ag;
			profession = pf;
		}

		public ACharacter(int i, string fn, string gn, bool gd)
		{
			immortalGroup = CharacterLibrary.ImmortalGroup[0];
			isAlive = true;
			id = i;
			familyName = fn;
			givenName = gn;
			gender = gd;
			birthday = TimeLogic.season * TimeLogic.dateCount + TimeLogic.date;
			lifespan = 75;
			age = 0;
			ageGroup = CharacterLibrary.AgeGroup[0];
			profession = CharacterLibrary.Profession[0];
		}

		public void OntoStage(Vector2 position)
		{
			Object.Destroy(flesh);

			if (gender)
			{
				flesh = Object.Instantiate(ResourceReader.Boy);
			}
			else {
				flesh = Object.Instantiate(ResourceReader.Girl);
			}

			flesh.name = "Chr" + id.ToString();
			flesh.transform.position = TerrainLogic.Project(position);
		}

		public void Await()
		{
			//mission.Await();
			flesh.SendMessage("CharacterAwait");
		}

		public void Goto(Vector3 target)
		{
			//mission = new AMission("移动至" + " " + location[0].ToString() + "," + location[1].ToString(), 0);
			flesh.SendMessage("CharacterGoto", target);
		}

		public void Aging()
		{
			if (birthday != TimeLogic.season * TimeLogic.dateCount + TimeLogic.date)
			{
				return;
			}
			age += 1;
			isAlive &= age <= lifespan;
			for (int i = 0; i < CharacterLibrary.AgeGroupFlag.Length; i++)
			{
				if (age >= CharacterLibrary.AgeGroupFlag[i])
				{
					ageGroup = CharacterLibrary.AgeGroup[i];
				}
			}
		}
	}
}