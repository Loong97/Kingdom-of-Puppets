using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Terrain : MonoBehaviour
{
	public const int MAPSIZE = 1024;
	public const float PLOTX = 0.6f;
	public const float PLOTZ = 0.52f;
	public const float JOYSTICKRATE = -0.0005f;

	protected Vector3[,] relatives = new Vector3[MAPSIZE, MAPSIZE];

	protected int shownumber = 63;
	protected int centerx = MAPSIZE / 2;
	protected int centerz = MAPSIZE / 2;

	void Start()
	{
		Read_Objects.joystick.OnDragging += RenewMeshPosition;
	}

	void Update()
	{
		RenewPoints();
	}

	protected void RenewMeshPosition(Vector2 vec)
	{
		transform.Translate(vec.x * JOYSTICKRATE, 0, vec.y * JOYSTICKRATE);
		int xn = (int)Mathf.Round(transform.position.x / PLOTX);
		int zn = (int)Mathf.Round(transform.position.z / PLOTZ);
		if (xn == 0 && zn == 0) return;
		transform.Translate(-xn * PLOTX, 0, -zn * PLOTZ);
		centerx = Tool_Math.Fit(centerx - xn, shownumber / 2, MAPSIZE - shownumber / 2);
		centerz = Tool_Math.Fit(centerz - zn, shownumber / 2, MAPSIZE - shownumber / 2);
		RenewMesh();
	}

	protected void RenewMesh()
	{
		Vector3[] vertices = new Vector3[shownumber * shownumber];
		for (int i = 0; i < shownumber; i++)
		{
			for (int j = 0; j < shownumber; j++)
			{
				Vector3 point_relative;
				float x, y, z;
				if (centerz % 2 == 0)
				{
					point_relative = relatives[centerx - shownumber / 2 + i, centerz - shownumber / 2 + j];
					x = (i - shownumber / 2 + (j % 2 == centerz % 2 ? 0.5f : 0)) * PLOTX + point_relative.x;
					z = (j - shownumber / 2) * PLOTZ + point_relative.z;
				}
				else
				{
					point_relative = relatives[centerx + shownumber / 2 - i, centerz + shownumber / 2 - j];
					x = (-i + shownumber / 2 + (j % 2 == centerz % 2 ? 0.5f : 0)) * PLOTX + point_relative.x;
					z = (-j + shownumber / 2) * PLOTZ + point_relative.z;
				}
				y = point_relative.y;
				vertices[i * shownumber + j] = new Vector3(x, y, z);
			}
		}

		if (GetComponent<MeshFilter>() != null)
		{
			GetComponent<MeshFilter>().mesh.vertices = vertices;
			GetComponent<MeshFilter>().mesh.RecalculateNormals();
			GetComponent<MeshFilter>().mesh.RecalculateBounds();
			GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
		}
		else
		{
			MeshInit(vertices);
		}
	}

	protected void ClosestPoint(Vector3 position, out int x, out int z)
	{
		x = (int)Mathf.Round(position.x / PLOTX) + centerx;
		z = (int)Mathf.Round(position.z / PLOTZ) + centerz;
	}

	protected virtual void RenewPoints() { }

	public virtual void NewGame() { }

	private void MeshInit(Vector3[] vertices)
	{
		Vector2[] uv = new Vector2[shownumber * shownumber];
		for (int i = 0; i < uv.Length; i++)
		{
			float u = (float)(i / shownumber) / (float)(shownumber - 1);
			float v = (float)(i % shownumber) / (float)(shownumber - 1);
			uv[i] = new Vector2(u, v);
		}
		int[] triangles = new int[6 * (shownumber - 1) * (shownumber - 1)];
		for (int i = 0; i < triangles.Length; i++)
		{
			int x = i / 6 / (shownumber - 1);
			int z = i / 6 % (shownumber - 1);
			bool flag = z % 2 != centerz % 2;
			int added = i % 6;
			triangles[i] = x * shownumber + z;
			switch (added)
			{
				case 0:
					break;
				case 1:
					triangles[i] += 1;
					break;
				case 2:
					triangles[i] += flag ? shownumber : shownumber + 1;
					break;
				case 3:
					triangles[i] += flag ? 1 : 0;
					break;
				case 4:
					triangles[i] += shownumber + 1;
					break;
				case 5:
					triangles[i] += shownumber;
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
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>();
		gameObject.AddComponent<MeshCollider>().sharedMesh = mesh;
	}


}
