using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public static class TimeLogic
	{
		public static int year = 0;
		public static int season = 0;
		public static int date = 0;
		public static float time = 0;
		public const int seasonCount = 4;
		public const int dateCount = 6;
		public const float dayLength = 60;

		public static void NewGame()
		{
			year = 0;
			season = 0;
			date = 0;
			time = 0;
		}

		public static void TimePass()
		{
			time += Time.deltaTime;
			if (time >= dayLength)
			{
				time = 0;
				date += 1;
			}
			if (date >= dateCount)
			{
				date = 0;
				season += 1;
			}
			if (season >= seasonCount)
			{
				season = 0;
				year += 1;
			}
		}
	}
}