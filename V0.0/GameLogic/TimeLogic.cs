using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public static class TimeLogic {
		public static int year = 0;
		public static int seasonIndex = 0;
		public static string season = "";
		public static int dateIndex = 0;
		public static string date = "";

		public static void SetTime (int y, int s, int d) {
			s %= 4;
			d %= 7;
			year = y;
			seasonIndex = s;
			season = TimeLibrary.Season [s];
			dateIndex = d;
			date = TimeLibrary.Date [s, d];
		}

		public static void NextTurn () {
			if (dateIndex >= 6) {
				if (seasonIndex >= 3) {
					year++;
					seasonIndex = 0;
				} else {
					seasonIndex++;
				}
				dateIndex = 0;
			} else {
				dateIndex++;
			}
			season = TimeLibrary.Season [seasonIndex];
			date = TimeLibrary.Date [seasonIndex, dateIndex];
			CharacterLogic.AgingAll ();
		}
	}
}