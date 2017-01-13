using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBuilder : MonoBehaviour {

    public static ObstacleBuilder current;

    GameObject obstaclePrefab; //future: change to array

    private void Awake()
    {
        current = this;

        obstaclePrefab = Resources.Load("Prefabs/Obstacles/CubeObstacle") as GameObject;
    }

    void Start () {

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

    public void makeObstacleOnMesh(int meshIndex, ObstacleType obstacleType)
    {
        if(ObstacleType.Cube == obstacleType)
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
        else if (ObstacleType.Jump == obstacleType)
        {
            FloorMesh floorMesh = FloorBuilder.current.floorMeshes[meshIndex];

            floorMesh.prevPos1 = floorMesh.prevPos1 + floorMesh.dir * floorMesh.length * 5.0f;
            floorMesh.prevPos2 = floorMesh.prevPos2 + floorMesh.dir * floorMesh.length * 5.0f;
            floorMesh.prevPos1.y -= 5.0f;
            floorMesh.prevPos2.y -= 5.0f;
            floorMesh.makeMesh();

        } else if (ObstacleType.BeforeJump == obstacleType)
        {
            FloorMesh floorMesh = FloorBuilder.current.floorMeshes[meshIndex];
            Vector3 dir = floorMesh.dir;
            dir.y = -floorMesh.dir.y;
            floorMesh.changeNormalByDir(Vector3.Cross(dir, new Vector3(dir.z, 0, -dir.x)));
            //floorMesh.makeMesh();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
