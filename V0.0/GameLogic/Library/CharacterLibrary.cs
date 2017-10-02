using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public static class CharacterLibrary {
		public static string[] ImmortalGroup = new string[3] { "凡人", "仙", "神" };
		public static string[] AgeGroup = new string[5] { "婴儿", "儿童", "少年", "成年", "暮年" };
		public static int[] AgeGroupFlag = new int[5] { 0, 3, 9, 18, 65 };
		public static string[] Profession = new string[3] { "无", "文", "武" };
	}
}