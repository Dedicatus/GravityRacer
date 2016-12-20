using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMesh : MonoBehaviour {

    public float width, length;
    public int index;

    public Vector3 prevPos1, prevPos2; //1 left, 2 right
    public Vector3 dir, prevDir;
    public Vector3 endPos1, endPos2;

    Vector3[] vertices;
    Vector2[] uvs;
    Vector3[] normals;
    int[] triangles;

    Mesh mesh;

    public void makeMesh()
    {
        if (prevPos1 == null || prevPos2 == null)
        {
            print("prev pos is null");
            return;
        }

        Vector3 prevPosMid = (prevPos1 + prevPos2) / 2.0f;
        prevPosMid += dir * length;
        endPos1 = prevPosMid + (width / 2.0f * Vector3.Cross(dir, Vector3.up));
        endPos2 = prevPosMid - (width / 2.0f * Vector3.Cross(dir, Vector3.up));

        mesh = new Mesh();

        int childCount = 1;
        vertices = new Vector3[childCount * 4];
        uvs = new Vector2[childCount * 4];
        normals = new Vector3[childCount * 4];
        triangles = new int[childCount * 2 * 3];

        vertices[0] = prevPos1;
        vertices[1] = prevPos2;
        vertices[2] = endPos1;
        vertices[3] = endPos2;

        uvs[0] = new Vector2(0.0f, 0.0f);
        uvs[1] = new Vector2(0.0f, 1.0f);
        uvs[2] = new Vector2(1.0f, 0.0f);
        uvs[3] = new Vector2(1.0f, 1.0f);

        for(int a=0;a!=4;++a)
        {
            Quaternion randomOffset = Quaternion.Euler( (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(0.0f, 5.0f), (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(0.0f, 5.0f), (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(0.0f, 5.0f));
            normals[a] = randomOffset * Vector3.Cross(dir, (prevPos2 - prevPos1).normalized);  //Vector3.up;
        }

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;
        
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;
        mesh.triangles = triangles;

        mesh.name = "Generated mesh";
        GetComponent<MeshFilter>().mesh = mesh;
        if (gameObject.GetComponent<MeshCollider>() == null)
            gameObject.AddComponent<MeshCollider>();
        else
        {
            DestroyImmediate(GetComponent<MeshCollider>());
            gameObject.AddComponent<MeshCollider>();
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        transform.parent.GetComponent<FloorBuilder>().collidingIndex = index;
        transform.parent.GetComponent<FloorBuilder>().collidedTime = 0;
    }

    /*void OnCollisionExit(Collision collision)
    {
        if (transform.parent.GetComponent<FloorBuilder>().collidingIndex == index)
        {
            transform.parent.GetComponent<FloorBuilder>().collidingIndex = -1;
        } 
    }*/

}
