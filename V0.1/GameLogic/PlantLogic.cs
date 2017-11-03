using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public static class PlantLogic
	{
		public static List<APlant> nature = new List<APlant>();

		public static void Clear()
		{
			nature.Clear();
		}

		public static void NewGame()
		{
			nature.Add(new APlant(0, 0, 12));
			nature[0].OntoStage(new Vector2(-3, 3));
		}
	}
}