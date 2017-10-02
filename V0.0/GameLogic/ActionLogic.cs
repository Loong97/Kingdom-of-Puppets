using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public static class ActionLogic {
		public static ACharacter selChr; 

		private static List<APlot> exploreList = new List<APlot> ();

		public static void DowncountMissions () {
			foreach (ACharacter character in CharacterLogic.family) {
				character.mission.Countdown ();
			}
		}

		public static void AddExploreList () {
			for (int i = 0; i < 9; i++) {
				int x = selChr.location [0] - 1 + i / 3;
				int z = selChr.location [1] - 1 + i % 3;
				if (x >= 0 && x < TerrainLogic.MAPSIZE && z >= 0 && z < TerrainLogic.MAPSIZE) {
					exploreList.Add (TerrainLogic.plots [x, z]);
				}
				foreach (APlot plot in exploreList) {
					plot.HalfCloud ();
				}
			}
		}

		public static void DoExploreList () {
			foreach (APlot plot in exploreList) {
				plot.vision = 2;
				plot.RenewCloud ();
			}
			exploreList.Clear ();
		}

		public static void SetOccupied (int x, int z, bool flag) {
			TerrainLogic.plots [x, z].occupied = flag;
		}
	}
}