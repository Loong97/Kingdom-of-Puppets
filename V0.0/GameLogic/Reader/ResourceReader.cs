using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public static class ResourceReader {
		public static Texture2D hometownMap = Resources.Load ("Map/HometownMap") as Texture2D;

		public static Material grassMat = Resources.Load ("Material/GrassMat") as Material;
		public static Material poolMat = Resources.Load ("Material/PoolMat") as Material;
		public static Material treeMat = Resources.Load ("Material/TreeMat") as Material;
		public static Material oreMat = Resources.Load ("Material/OreMat") as Material;
		public static Material cloudMat = Resources.Load ("Material/CloudMat") as Material;
		public static Material cloudMatHalf = Resources.Load ("Material/CloudMatHalf") as Material;

		public static GameObject Man = Resources.Load ("Character/Man") as GameObject;
		public static GameObject Wom = Resources.Load ("Character/Wom") as GameObject;
	}
}