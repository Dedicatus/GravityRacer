using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMesh : MonoBehaviour {

    public float width, length;
    public int index;

    public int coinIndex = -1;

    public GameObject leftRim;
    public GameObject rightRim;

    public GameObject destroyOnRemake;
     
    public Vector3 prevPos1, prevPos2; //1 left, 2 right
    public Vector3 dir, prevDir;
    public Vector3 endPos1, endPos2;

    bool animating;

    Vector3[] vertices;
    Vector2[] uvs;
    Vector3[] normals;
    int[] triangles;

    Mesh mesh;

    public void makeMesh()
    {
        Vector3 prevPosMid = (prevPos1 + prevPos2) / 2.0f;
        prevPosMid += dir * length;
        endPos1 = prevPosMid + (width / 2.0f * Vector3.Cross(dir, Vector3.up));
        endPos2 = prevPosMid - (width / 2.0f * Vector3.Cross(dir, Vector3.up));

        CoinGenerator.current.disableCoin(coinIndex);
        coinIndex = -1;

        if (destroyOnRemake != null)
            Destroy(destroyOnRemake);

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
        uvs[1] = new Vector2(1.0f, 0.0f);
        uvs[2] = new Vector2(0.0f, 1.0f);
        uvs[3] = new Vector2(1.0f, 1.0f);

       // float dot = Vector3.Dot(prevDir, dir);
        //float angle = Vector3.Angle(prevDir, dir);
        //if (dot < 0) angle = -angle;
        //print(angle);
        for(int a=0;a!=4;++a)
        {
            //Quaternion randomOffset = Quaternion.Euler( (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(0.0f, 5.0f), (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(0.0f, 5.0f), (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(0.0f, 5.0f));
            //normals[a] = randomOffset * Vector3.Cross(dir, (prevPos2 - prevPos1).normalized);  //Vector3.up;
            normals[a] = Quaternion.Euler(dir.x, 0, 0) * Vector3.up;
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

        leftRim.transform.position = (endPos1 + prevPos1) / 2.0f;
        leftRim.transform.localScale = new Vector3(0.3f, 0.3f, (endPos1 - prevPos1).magnitude);
        leftRim.transform.forward = (endPos1 - prevPos1).normalized;
        rightRim.transform.position = (endPos2 + prevPos2) / 2.0f;
        rightRim.transform.localScale = new Vector3(0.3f, 0.3f, (endPos2 - prevPos2).magnitude);
        rightRim.transform.forward = (endPos2 - prevPos2).normalized;

        animateToDest();
        //dir.y += 0.01f;
        // dir.Normalize();
    }

    void animateToDest()
    {
        transform.position = new Vector3(0, -100.0f, 0);
        GetComponent<MoveToDecreasingSpeed>().from = transform.position;
        GetComponent<MoveToDecreasingSpeed>().to = Vector3.zero;
        GetComponent<MoveToDecreasingSpeed>().resetAnim();
        animating = true;
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(animating)
        {
            //GetComponent<MoveToDecreasingSpeed>().moveToword();
            if (GetComponent<MoveToDecreasingSpeed>().reached == true) animating = false;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.other.tag == "Player")
        {
            Vector3 rot = Player.current.transform.eulerAngles;
            rot.x = -Mathf.Asin(dir.y) * Mathf.Rad2Deg + 14;
            Player.current.transform.eulerAngles = rot;//.up = Vector3.Cross(endPos1 - prevPos1, prevPos2 - prevPos1).normalized;
            transform.parent.GetComponent<FloorBuilder>().meshCollided(index);
        }
        
        //transform.parent.GetComponent<FloorBuilder>().collidedTime = 0;
    }

    /*void OnCollisionExit(Collision collision)
    {
        if (transform.parent.GetComponent<FloorBuilder>().collidingIndex == index)
        {
            transform.parent.GetComponent<FloorBuilder>().collidingIndex = -1;
        } 
    }*/

}
