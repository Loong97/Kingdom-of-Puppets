using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public static class TerrainLogic
	{
		public const int SIZE = 200;
		public const float INTERVAL = 0.25f;
		public const float SEABOTTOM = -2.0f;
		public static GameObject terrain;
		public static GameObject backterrain;

		public static void Clear()
		{
			Object.Destroy(terrain);
			Object.Destroy(backterrain);
		}

		public static void GoBack()
		{
			terrain = backterrain;
		}

		public static void Lift(float centerX, float centerZ, float radius, float targetH)
		{
			Vector3[] vertices = terrain.GetComponent<MeshFilter>().mesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				float x = vertices[i].x;
				float y = vertices[i].y;
				float z = vertices[i].z;
				float distance = ((x - centerX) * (x - centerX) + (z - centerZ) * (z - centerZ)) / (radius * radius);
				if (y >= targetH)
				{
					continue;
				}
				else if (distance <= 1)
				{
					vertices[i].y = targetH;
				}
				else if (distance <= targetH - y + 1)
				{
					vertices[i].y = targetH - distance + 1;
				}
			}
			terrain.GetComponent<MeshFilter>().mesh.vertices = vertices;
			terrain.GetComponent<MeshFilter>().mesh.RecalculateNormals();
			terrain.GetComponent<MeshFilter>().mesh.RecalculateBounds();
			terrain.GetComponent<MeshCollider>().sharedMesh = terrain.GetComponent<MeshFilter>().mesh;
		}

		public static void NewGame()
		{
			Clear();
			NewTerrain();
			SaveBack();
			Lift(0, 0, 3.5f, 0.5f);
			Lift(-3, 3, 2.5f, 0.5f);
		}

		public static Vector3 Project(Vector2 origin2)
		{
			Vector3 origin3 = new Vector3(origin2.x, 1000.0f, origin2.y);
			Ray ray = new Ray(origin3, Vector3.down);
			RaycastHit hit;
			if (terrain.GetComponent<MeshCollider>().Raycast(ray, out hit, 2000.0f))
			{
				return hit.point;
			}
			else return origin3;
		}

		private static float HALFMAP
		{
			get
			{
				return 0.5f * (float)SIZE * INTERVAL;
			}
		}

		private static void SaveBack()
		{
			backterrain = terrain;
		}

		private static void NewTerrain()
		{
			Vector3[] vertices = new Vector3[SIZE * SIZE];
			for (int i = 0; i < vertices.Length; i++)
			{
				float x = (float)(i / SIZE) * INTERVAL - HALFMAP;
				float z = (float)(i % SIZE) * INTERVAL - HALFMAP;
				vertices[i] = new Vector3(x, SEABOTTOM, z);
			}

			Vector2[] uv = new Vector2[SIZE * SIZE];
			for (int i = 0; i < uv.Length; i++)
			{
				float u = (float)(i / SIZE) / (float)(SIZE - 1);
				float v = (float)(i % SIZE) / (float)(SIZE - 1);
				uv[i] = new Vector2(u, v);
			}

			int[] triangles = new int[6 * (SIZE - 1) * (SIZE - 1)];
			for (int i = 0; i < triangles.Length; i++)
			{
				int x = i / 6 / (SIZE - 1);
				int z = i / 6 % (SIZE - 1);
				int added = i % 6;
				triangles[i] = x * SIZE + z;
				switch (added)
				{
					case 0:
						break;
					case 1:
						triangles[i] += 1;
						break;
					case 2:
						triangles[i] += 1 + SIZE;
						break;
					case 3:
						break;
					case 4:
						triangles[i] += SIZE + 1;
						break;
					case 5:
						triangles[i] += SIZE;
						break;
				}
			}

			Mesh mesh = new Mesh();
			mesh.Clear();
			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			terrain = new GameObject();
			terrain.name = "Terrain";
			terrain.transform.position = new Vector3(0, 0, 0);
			terrain.AddComponent<MeshFilter>().mesh = mesh;
			terrain.AddComponent<MeshRenderer>();
			terrain.AddComponent<MeshCollider>().sharedMesh = mesh;
			terrain.GetComponent<MeshRenderer>().material = ResourceReader.landMat;
		}
	}
}