using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_Terrain : Base_Terrain
{
	public const float SEABOTTOM = -3f;
	public const float CONNECTED_RATIO = 0.3f;
	public const float HEIGHT_RATIO = 0.1f;
	public float[,] heights = new float[MAPSIZE, MAPSIZE];

	protected override void RenewPoints()
	{
		Vector3[,] npoints = new Vector3[shownumber, shownumber];
		for (int i = 0; i < shownumber; i++)
		{
			for (int j = 0; j < shownumber; j++)
			{
				int x = centerx - shownumber / 2 + i;
				int z = centerz - shownumber / 2 + j;
				float deltay = 0;
				Vector3 npoint = relatives[x, z];
				if (x == 0 || x == MAPSIZE - 1 || z == 0 || z == MAPSIZE - 1) continue;
				int[] connected_x;
				if (z % 2 == 0) connected_x = new int[6] { x - 1, x + 1, x, x - 1, x, x - 1 };
				else connected_x = new int[6] { x - 1, x + 1, x, x + 1, x, x + 1 };
				int[] connected_z = new int[6] { z, z, z + 1, z + 1, z - 1, z - 1 };
				for (int k = 0; k < 6; k++) deltay += (relatives[connected_x[k], connected_z[k]].y - npoint.y) * CONNECTED_RATIO;
				deltay += (heights[x, z] - npoint.y) * HEIGHT_RATIO;
				deltay = deltay > 0 ? deltay : 0;
				npoint.y += deltay;
				npoints[i, j] = npoint;
			}
		}
		for (int i = 0; i < shownumber; i++)
		{
			for (int j = 0; j < shownumber; j++)
			{
				int x = centerx - shownumber / 2 + i;
				int z = centerz - shownumber / 2 + j;
				relatives[x, z] = npoints[i, j];
			}
		}
		RenewMesh();
	}

	public override void NewGame()
	{
		for (int i = 0; i < MAPSIZE; i++)
		{
			for (int j = 0; j < MAPSIZE; j++)
			{
				relatives[i, j] = new Vector3(0, SEABOTTOM, 0);
				heights[i, j] = SEABOTTOM;
			}
		}
		Lift(Vector2.zero, 5, 0.5f);
		Lift(new Vector2(-4, 4), 3, 1.5f);
		RenewMesh();
		GetComponent<MeshRenderer>().material = Read_Resources.landMat;
	}

	private void Lift(Vector2 center, float radius, float height)
	{
		for (int i = 0; i < MAPSIZE; i++)
		{
			for (int j = 0; j < MAPSIZE; j++)
			{
				float x = (i - MAPSIZE / 2 + (j % 2 == 0 ? 0 : PLOTX / 2)) * PLOTX - center.x;
				float z = (j - MAPSIZE / 2) * PLOTZ - center.y;
				if (x * x + z * z <= radius * radius) heights[i, j] = height;
			}
		}
	}
}