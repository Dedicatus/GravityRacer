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

    public int collidedCount;

    public enum FloorType
    {
        Straight,
        SmoothCurve,
        SteepCurve,
        UTurn,
        NarrowWidth,//Wait for more
        TotalRandom
    }

    public FloorType floorType;
    /* Floor Types:
     * 0: Straight
     * 1: Smooth Curve
     * 2: Steep Curve
     * 3: U Turn
     * 4: Width / 2
     * 5: Total Random
     * 
     */
    public int remainingFloorCount;
    public float floorTurningAngle;

    public static FloorBuilder current;

    public FloorMesh[] floorMeshes;

    int coinCount; //coin count for current map
    int coinRemain;


    // Use this for initialization
    void Start () {

        current = this;

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
        remainingFloorCount = 10;
        floorTurningAngle = 0;

        floorMeshes[0].prevPos1 = new Vector3(0, 0, 0);
        floorMeshes[0].prevPos2 = new Vector3(5, 0, 0);
        floorMeshes[0].dir = (new Vector3(0, -0.05f, 0.9f)).normalized;
        floorMeshes[0].prevDir = Vector3.forward;
        floorMeshes[0].makeMesh();
        for (int a = 1; a != floorMeshCount; ++a)
        {
            remainingFloorCount--;
            if (remainingFloorCount <= 0)
                refreshFloorType();
            floorMeshes[a].prevDir = floorMeshes[a - 1].dir;
            floorMeshes[a].dir = Quaternion.Euler(0, floorTurningAngle, 0) * floorMeshes[a].prevDir;
            floorMeshes[a].prevPos1 = floorMeshes[a - 1].endPos1;
            floorMeshes[a].prevPos2 = floorMeshes[a - 1].endPos2;
            floorMeshes[a].makeMesh();

            coinRemain--;
            if(coinRemain>=0 && coinRemain < coinCount)
                CoinGenerator.current.putCoin(a);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.current.state != GameManager.GameState.Running) return;
        collidedTime += Time.deltaTime;
        if(collidedTime >= 2.0f)
        {
            Player.current.Die();
        }
        while( (collidingIndex > startIndex && collidingIndex - startIndex > floorMeshCount / 2) || (collidingIndex < startIndex && collidingIndex + floorMeshCount - startIndex > floorMeshCount / 2)  )
        {
            makeFloorByType();
        }
    }

    public void meshCollided(int index)
    {
        collidedCount += (index - collidingIndex)>0? (index - collidingIndex): collidingIndex - index;
        GameManager.current.gameScore = collidedCount;
        collidingIndex = index;
        collidedTime = 0;
    }

    void makeFloorByType()
    {
        if(remainingFloorCount <= 0)
        {
            refreshFloorType();
        }
        remainingFloorCount--;

        floorMeshes[startIndex].prevPos1 = floorMeshes[endIndex].endPos1;
        floorMeshes[startIndex].prevPos2 = floorMeshes[endIndex].endPos2;
        floorMeshes[startIndex].prevDir = floorMeshes[endIndex].dir;
        floorMeshes[startIndex].dir = Quaternion.Euler(0, floorTurningAngle, 0) * floorMeshes[startIndex].prevDir;
        floorMeshes[startIndex].makeMesh();

        coinRemain--;
        if (coinRemain >= 0 && coinRemain < coinCount)
            CoinGenerator.current.putCoin(startIndex);
        endIndex = startIndex;
        startIndex = (startIndex + 1) % floorMeshCount;

    }

    void refreshFloorType()
    {
        floorType = (FloorType)Random.Range(0, 3);
        //print(floorType);
        switch(floorType)
        {
            case FloorType.Straight:
                remainingFloorCount = Random.Range(3,6);
                coinCount = Random.Range(0,remainingFloorCount);
                coinRemain = Random.Range(coinCount, remainingFloorCount);
                floorTurningAngle = 0;
                break;
            case FloorType.SmoothCurve:
                remainingFloorCount = Random.Range(5, 20);
                coinCount = Random.Range(0, remainingFloorCount);
                coinRemain = Random.Range(coinCount, remainingFloorCount);
                floorTurningAngle = (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(4.0f,10.0f);
                break;
            case FloorType.SteepCurve:
                remainingFloorCount = Random.Range(5, 10);
                coinCount = Random.Range(0, remainingFloorCount);
                coinRemain = Random.Range(coinCount, remainingFloorCount);
                floorTurningAngle = (Random.Range(0.0f,1.0f)>0.5f?1:-1) *  Random.Range(10.0f, 15.0f);
                break;
            case FloorType.UTurn:
                remainingFloorCount = 10;
                coinCount = Random.Range(0, remainingFloorCount);
                coinRemain = Random.Range(coinCount, remainingFloorCount);
                floorTurningAngle = (Random.Range(0, 1) > 0.5f ? 1 : -1) * Random.Range(5, 15);
                break;
            case FloorType.NarrowWidth:
                remainingFloorCount = 10;
                break;
            case FloorType.TotalRandom:
                remainingFloorCount = 10;
                break;

            default:
                break;
        }
    }

}
