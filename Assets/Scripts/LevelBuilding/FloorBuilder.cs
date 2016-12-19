using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBuilder : MonoBehaviour {

    public int floorMeshCount;
    public GameObject floorMeshPrefab;
    
    public int collidingIndex;

    public int startIndex;
    public int endIndex;

    public float collidedTime;

    public FloorMesh[] floorMeshes;

    // Use this for initialization
    void Start () {
        floorMeshes = new FloorMesh[floorMeshCount];
        startIndex = 0;
        endIndex = floorMeshCount-1;
        for(int a=0;a!=floorMeshCount;++a)
        {
            GameObject floorObject = (GameObject)Instantiate<GameObject>(floorMeshPrefab);
            floorObject.transform.position = Vector3.zero;
            floorMeshes[a] = floorObject.GetComponent<FloorMesh>();
            floorMeshes[a].width = 15;
            floorMeshes[a].length = 3;
            floorMeshes[a].index = a;
            floorObject.transform.parent = transform;
        }
        floorMeshes[0].prevPos1 = new Vector3(0, 0, 0);
        floorMeshes[0].prevPos2 = new Vector3(5, 0, 0);
        floorMeshes[0].dir = (new Vector3(0, -0.02f, 0.9f)).normalized;
        floorMeshes[0].prevDir = Vector3.forward;
        floorMeshes[0].makeMesh();
        for (int a = 1; a != floorMeshCount; ++a)
        {
            floorMeshes[a].prevDir = floorMeshes[a - 1].dir;
            floorMeshes[a].dir = Quaternion.Euler(0, Random.Range(-20,20), 0) * floorMeshes[a].prevDir;
            floorMeshes[a].prevPos1 = floorMeshes[a - 1].endPos1;
            floorMeshes[a].prevPos2 = floorMeshes[a - 1].endPos2;
            floorMeshes[a].makeMesh();
        }
    }
	
	// Update is called once per frame
	void Update () {
        collidedTime += Time.deltaTime;
        if(collidedTime >= 0.9f)
        {
            Player.current.Die();
        }
        while( (collidingIndex > startIndex && collidingIndex - startIndex > floorMeshCount / 2) || (collidingIndex < startIndex && collidingIndex + floorMeshCount - startIndex > floorMeshCount / 2)  )
        {
            floorMeshes[startIndex].prevPos1 = floorMeshes[endIndex].endPos1;
            floorMeshes[startIndex].prevPos2 = floorMeshes[endIndex].endPos2;
            floorMeshes[startIndex].prevDir = floorMeshes[endIndex].dir;
            floorMeshes[startIndex].dir = Quaternion.Euler(0, Random.Range(-20, 20), 0) * floorMeshes[startIndex].prevDir;
            floorMeshes[startIndex].makeMesh();
            endIndex = startIndex;
            startIndex = (startIndex+1)% floorMeshCount;
        }
    }
}
