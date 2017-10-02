using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public static class TimeLibrary {
		public static string[] Season = new string[4] { "春", "夏", "秋", "冬" };
		public static string[,] Date = new string[4, 7] {
			{ "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "春夜" },
			{ "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "夏夜" },
			{ "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "秋夜" },
			{ "立冬", "小雪", "大雪", "冬至", "小寒", "大寒", "冬夜" }
		};
	}
}