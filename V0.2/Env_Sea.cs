using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_Sea : Base_Terrain
{
	public const float K_ORIGIN = 1f;
	public const float K_CONNECTED = 0.1f;
	public const float SPEED_LIMIT = 0.3f;

	private Vector3[,] speeds = new Vector3[MAPSIZE, MAPSIZE];
	private static float rl1 = Tool_Math.Length(new Vector3(PLOTX / 2, 0, PLOTZ));
	private float[] restlengths = new float[6] { PLOTX, PLOTX, rl1, rl1, rl1, rl1 };

	public void Drop(Vector3 point)
	{
		int _x, _z;
		ClosestPoint(point, out _x, out _z);
		relatives[_x, _z].y -= SPEED_LIMIT;
	}

	protected override void RenewPoints()
	{
		Vector3[,] npoints = new Vector3[shownumber, shownumber];
		Vector3[,] nspeeds = new Vector3[shownumber, shownumber];
		for (int i = 0; i < shownumber; i++)
		{
			for (int j = 0; j < shownumber; j++)
			{
				int x = centerx - shownumber / 2 + i;
				int z = centerz - shownumber / 2 + j;
				Vector3 npoint = relatives[x, z];
				Vector3 nspeed = speeds[x, z];
				Vector3 force = -npoint * K_ORIGIN;
				if (x == 0 || x == MAPSIZE - 1 || z == 0 || z == MAPSIZE - 1) continue;
				int[] connected_x;
				if (z % 2 == 0) connected_x = new int[6] { x - 1, x + 1, x, x + 1, x, x + 1 };
				else connected_x = new int[6] { x - 1, x + 1, x, x - 1, x, x - 1 };
				int[] connected_z = new int[6] { z, z, z + 1, z + 1, z - 1, z - 1 };
				for (int k = 0; k < 6; k++)
				{
					Vector3 dir = relatives[connected_x[k], connected_z[k]] - npoint;
					force += dir * (Tool_Math.Length(dir) - restlengths[k]) * K_CONNECTED;
				}
				nspeed += force;
				float nspeedl = Tool_Math.Length(nspeed);
				if (nspeedl > SPEED_LIMIT) nspeed /= nspeedl / SPEED_LIMIT;
				npoint += nspeed;
				npoints[i, j] = npoint;
				nspeeds[i, j] = nspeed;
			}
		}
		for (int i = 0; i < shownumber; i++)
		{
			for (int j = 0; j < shownumber; j++)
			{
				int x = centerx - shownumber / 2 + i;
				int z = centerz - shownumber / 2 + j;
				relatives[x, z] = npoints[i, j];
				speeds[x, z] = nspeeds[i, j];
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
				relatives[i, j] = Vector3.zero;
				speeds[i, j] = Vector3.zero;
			}
		}
		RenewMesh();
		GetComponent<MeshRenderer>().material = Read_Resources.waterMat;
	}
}