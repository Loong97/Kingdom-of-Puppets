using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	public class APlot {
		public byte indx = 0;
		public byte indz = 0;
		public byte height = 0;
		public byte vision = 0;
		public byte type = 0;
		public byte cover = 0;
		public GameObject plot;
		public GameObject cloud;
		public GameObject[] covers;
		public bool occupied = false;

		private float plotsizef;
		private float centerXf;
		private float centerZf;

		public APlot (int x=0, int z=0, byte h=0, byte v=0, byte t=0, byte c=0) {
			indx = (byte)x;
			indz = (byte)z;
			height = h;
			vision = v;
			type = t;
			cover = c;
			occupied = false;

			int mapsize = TerrainLogic.MAPSIZE;
			int plotsize = TerrainLogic.PLOTSIZE;
			plotsizef = (float)plotsize;
			int centerX = -mapsize / 2 * plotsize + plotsize / 2 + indx * plotsize;
			centerXf = (float)centerX;
			int centerZ = -mapsize / 2 * plotsize + plotsize / 2 + indz * plotsize;
			centerZf = (float)centerZ;
		}

		public void RenewPlot () {
			if (plot != null) {
				GameObject.Destroy (plot);
				plot = null;
			}

			int[,] xs = new int[3, 3];
			int[,] zs = new int[3, 3];
			GetSurroundIndex (out xs, out zs);

			int[,] hs = new int[3, 3];
			for (int i = 0; i < 9; i++) {
				hs [i / 3, i % 3] = TerrainLogic.plots [xs [i / 3, i % 3], zs [i / 3, i % 3]].height;
			}

			float[] ra = new float[2]{ 0.35f, 0.45f };

			float[] _x = new float[4] {
				- plotsizef / 2,
				- plotsizef * ra[type],
				plotsizef * ra[type],
				plotsizef / 2
			};
			float[] _z = new float[4] {
				- plotsizef / 2,
				- plotsizef * ra[type],
				plotsizef * ra[type],
				plotsizef / 2
			};

			Vector3[] vertices = new Vector3[16];

			vertices [0] = new Vector3 (_x [0], (hs [0, 0] + hs [0, 1] + hs [1, 0] + hs [1, 1]) / 4, _z [0]);
			vertices [1] = new Vector3 (_x [0], (hs [0, 1] + hs [1, 1]) / 2, _z [1]);
			vertices [2] = new Vector3 (_x [0], (hs [0, 1] + hs [1, 1]) / 2, _z [2]);
			vertices [3] = new Vector3 (_x [0], (hs [0, 1] + hs [0, 2] + hs [1, 1] + hs [1, 2]) / 4, _z [3]);

			vertices [4] = new Vector3 (_x [1], (hs [1, 0] + hs [1, 1]) / 2, _z [0]);
			vertices [5] = new Vector3 (_x [1], hs [1, 1], _z [1]);
			vertices [6] = new Vector3 (_x [1], hs [1, 1], _z [2]);
			vertices [7] = new Vector3 (_x [1], (hs [1, 1] + hs [1, 2]) / 2, _z [3]);

			vertices [8] = new Vector3 (_x [2], (hs [1, 0] + hs [1, 1]) / 2, _z [0]);
			vertices [9] = new Vector3 (_x [2], hs [1, 1], _z [1]);
			vertices [10] = new Vector3 (_x [2], hs [1, 1], _z [2]);
			vertices [11] = new Vector3 (_x [2], (hs [1, 1] + hs [1, 2]) / 2, _z [3]);

			vertices [12] = new Vector3 (_x [3], (hs [1, 0] + hs [1, 1] + hs [2, 0] + hs [2, 1]) / 4, _z [0]);
			vertices [13] = new Vector3 (_x [3], (hs [1, 1] + hs [2, 1]) / 2, _z [1]);
			vertices [14] = new Vector3 (_x [3], (hs [1, 1] + hs [2, 1]) / 2, _z [2]);
			vertices [15] = new Vector3 (_x [3], (hs [1, 1] + hs [1, 2] + hs [2, 1] + hs [2, 2]) / 4, _z [3]);

			float[] _u = new float[4] { 0, 0.5f - ra [type], 0.5f + ra [type], 1 };
			float[] _v = new float[4] { 0, 0.5f - ra [type], 0.5f + ra [type], 1 };

			Vector2[] uv = new Vector2[16];
			for (int i = 0; i < 16; i++) {
				uv [i] = new Vector2 (_u [i / 4], _v [i % 4]);
			}

			int[] triangles = new int[54];
			for (int i = 0; i < 9; i++) {
				int o = i + i / 3;
				triangles [i * 6] = o;
				triangles [i * 6 + 1] = o + 5;
				triangles [i * 6 + 2] = o + 4;
				triangles [i * 6 + 3] = o;
				triangles [i * 6 + 4] = o + 1;
				triangles [i * 6 + 5] = o + 5;
			}

			Mesh mesh = new Mesh ();
			mesh.Clear ();
			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
			mesh.RecalculateNormals ();
			mesh.RecalculateBounds ();

			plot = new GameObject ();
			plot.name = "Plot" + indx.ToString () + "-" + indz.ToString ();
			plot.transform.position = new Vector3 (centerXf, 0, centerZf);
			plot.AddComponent<MeshFilter> ().mesh = mesh;
			plot.AddComponent<MeshRenderer> ();
			plot.AddComponent<MeshCollider> ().sharedMesh = mesh;

			switch (type) {
			case 0:
				plot.GetComponent<MeshRenderer> ().material = ResourceReader.grassMat;
				break;
			case 1:
				plot.GetComponent<MeshRenderer> ().material = ResourceReader.poolMat;
				break;
			case 2:
				plot.GetComponent<MeshRenderer> ().material = ResourceReader.grassMat;
				break;
			default:
				plot.GetComponent<MeshRenderer> ().material = ResourceReader.grassMat;
				break;
			}
		}

		public void HalfCloud () {
			if (cloud != null) {
				cloud.GetComponent<MeshRenderer> ().material = ResourceReader.cloudMatHalf;
			}
		}

		public void RenewCloud () {
			if (cloud != null) {
				GameObject.Destroy (cloud);
				cloud = null;
			}

			switch (vision) {
			case 0:
				cloud = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cloud.transform.parent = plot.transform;
				cloud.name = "Cloud" + indx.ToString () + "-" + indz.ToString ();
				cloud.transform.localPosition = new Vector3 (0, height, 0);
				cloud.transform.localScale = new Vector3 (4, 4, 4);
				cloud.GetComponent<MeshRenderer> ().material = ResourceReader.cloudMat;
				break;
			case 1:
				break;
			case 2:
				break;
			default:
				break;
			}
		}

		private void GetSurroundIndex (out int[,] xs, out int[,] zs) {
			int mapsize = TerrainLogic.MAPSIZE;
			xs = new int[3, 3];
			zs = new int[3, 3];
			for (int i = 0; i < 9; i++) {
				int x = (int)indx - 1 + i / 3;
				if (x < 0) {
					x = 0;
				} else if (x >= mapsize) {
					x = mapsize - 1;
				}

				int z = (int)indz - 1 + i % 3;
				if (z < 0) {
					z = 0;
				} else if (z >= mapsize) {
					z = mapsize - 1;
				}

				xs [i / 3, i % 3] = x;
				zs [i / 3, i % 3] = z;
			}
		}
	}
}