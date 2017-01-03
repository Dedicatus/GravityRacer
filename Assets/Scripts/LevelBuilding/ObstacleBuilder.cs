using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBuilder : MonoBehaviour {

    public static ObstacleBuilder current;

    GameObject obstaclePrefab; //future: change to array

	void Start () {
        current = this;

        obstaclePrefab = Resources.Load("Prefabs/Obstacles/CubeObstacle") as GameObject;

    }
	
    public void makeObstacleOnMesh(int meshIndex)
    {
        GameObject obstacle = Instantiate<GameObject>(obstaclePrefab) as GameObject;
        FloorMesh floorMesh = FloorBuilder.current.floorMeshes[meshIndex];
        Vector3 cross = Vector3.Cross(floorMesh.prevDir, floorMesh.dir);
        float posScale = cross.y > 0 ? 0.8f : 0.2f;
        Vector3 prevPosMid = floorMesh.prevPos1 + (floorMesh.prevPos2 - floorMesh.prevPos1) * posScale;
        prevPosMid += floorMesh.dir * floorMesh.length / 2.0f;
        prevPosMid.y += 1.0f;
        obstacle.transform.position = prevPosMid;
        obstacle.transform.forward = floorMesh.prevDir;

        floorMesh.destroyOnRemake = obstacle;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
