using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public static class TerrainLogic {
		public const int MAPSIZE = 128;
		public const int PLOTSIZE = 4;
		public static APlot[,] plots = new APlot[MAPSIZE, MAPSIZE];

		public static void Clear () {
			plots = new APlot[MAPSIZE, MAPSIZE];
		}

		public static void ReadMap () {
			for (int i = 0; i < MAPSIZE * MAPSIZE; i++) {
				int x = i / MAPSIZE;
				int z = i % MAPSIZE;

				Color c = ResourceReader.hometownMap.GetPixel (x, z);
				byte height = (byte)((c.r * 256 + 1) / 2);
				byte type = (byte)((c.g * 256 + 25) / 50);
				byte cover = (byte)((c.b * 256 + 10) / 20);

				plots [x, z] = new APlot (x, z, height, 0, type, cover);
				//临时代码：初始可见区域
				if (Mathf.Abs (x - MAPSIZE / 2) + Mathf.Abs (z - MAPSIZE / 2) <= 4) {
					plots [x, z].vision = 2;
				}
			}
		}

		public static void Initialize () {
			foreach (APlot plot in plots) {
				plot.RenewPlot ();
				plot.RenewCloud ();
			}
		}

		public static APlot Find (string objName) {
			int x = int.Parse (objName.Split ('t') [1].Split ('-') [0]);
			int z = int.Parse (objName.Split ('t') [1].Split ('-') [1]);
			return plots [x, z];
		}

		public static Vector3 GetPosition (int x, int z) {
			float _x = plots [x, z].plot.transform.position.x;
			float _y = plots [x, z].height;
			float _z = plots [x, z].plot.transform.position.z;
			return new Vector3 (_x, _y, _z);
		}

		public static int GetHeight (Vector3 position) {
			int x = (int)(position.x / (float)PLOTSIZE) + MAPSIZE / 2;
			int z = (int)(position.z / (float)PLOTSIZE) + MAPSIZE / 2;
			if (x >= 0 && x < MAPSIZE && z >= 0 && z < MAPSIZE) {
				return plots [x, z].height;
			} else {
				return 0;
			}
		}
	}
}