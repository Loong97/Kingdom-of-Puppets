using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public static class CharacterLogic
	{
		public static List<ACharacter> family = new List<ACharacter>();

		public static void Clear()
		{
			family.Clear();
		}

		public static void NewGame()
		{
			family.Add(new ACharacter(0, "百里", "忆瑾", false));
			family[0].OntoStage(new Vector2(0, 0));
		}

		public static ACharacter Find(string objName)
		{
			int id = int.Parse(objName.Split('r')[1]);
			foreach (ACharacter chr in family)
			{
				if (chr.id == id)
				{
					return chr;
				}
			}
			return null;
		}

		public static void AgingAll()
		{
			foreach (ACharacter chr in family)
			{
				chr.Aging();
			}
		}
	}
}