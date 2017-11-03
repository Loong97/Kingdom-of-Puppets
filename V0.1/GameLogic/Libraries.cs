using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public static class TimeLibrary
	{
		public static string[] Season = new string[4] { "春", "夏", "秋", "冬" };
		public static string[,] Date = new string[4, 6] {
			{ "立春", "雨水", "惊蛰", "春分", "清明", "谷雨" },
			{ "立夏", "小满", "芒种", "夏至", "小暑", "大暑" },
			{ "立秋", "处暑", "白露", "秋分", "寒露", "霜降" },
			{ "立冬", "小雪", "大雪", "冬至", "小寒", "大寒" }
		};
	}

	public static class CharacterLibrary
	{
		public static string[] ImmortalGroup = new string[3] { "凡人", "仙", "神" };
		public static string[] AgeGroup = new string[5] { "婴儿", "儿童", "少年", "成年", "暮年" };
		public static int[] AgeGroupFlag = new int[5] { 0, 3, 9, 18, 65 };
		public static string[] Profession = new string[3] { "无", "文", "武" };
	}

	public static class PlantLibrary
	{
		public static string[] Name = new string[3] { "椰子树", "石头", "鱼" };
		public static int[] MatureDay = new int[3] { 12, 12, 6 };
	}
}