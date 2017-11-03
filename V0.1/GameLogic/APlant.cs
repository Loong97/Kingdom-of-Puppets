using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public class APlant
	{
		public int type = 0;
		public int id = 0;
		public int day = 0;
		public GameObject entity;

		public APlant(int t, int i, int d)
		{
			type = t;
			id = i;
			day = d;
		}

		public void OntoStage(Vector2 position)
		{
			Object.Destroy(entity);
			entity = Object.Instantiate(ResourceReader.Plants[type]);

			entity.name = "Pla" + id.ToString();
			entity.transform.position = TerrainLogic.Project(position);
			entity.transform.Rotate(new Vector3(0, id % 6 * 60, 0));
			float rate = (float)day / (float)PlantLibrary.MatureDay[type];
			entity.transform.localScale = rate >= 1 ? new Vector3(1, 1, 1) : new Vector3(rate, rate, rate);
		}
	}
}