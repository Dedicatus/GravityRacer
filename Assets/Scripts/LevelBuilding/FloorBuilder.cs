using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorType
{
    Straight,
    SmoothCurve,
    SteepCurve,
    Jump,
    NarrowWidth,//Wait for more
    TotalRandom
}

public enum ObstacleType
{
    Cube,
    Jump,
    BeforeJump
}

public class ObstacleData
{
    public ObstacleType obstacleType;
    public int obstacleCount;
    public int obstacleStartIndex;
}


public class FloorTypeData
{
    public FloorType floorType;
    public int floorCount;
    public float floorTurningAngle;
    public int coinCount;
    public int coinStartIndex;
    public ArrayList obstacles;
}

public class FloorBuilder : MonoBehaviour {

    public int floorMeshCount;
    public GameObject floorMeshPrefab;

    public float width;
    public float length;

    public int initialStraightLength;

    public int collidingIndex;

    public int startIndex;
    public int endIndex;
    
    public float collidedTime;

    public int collidedCount;

    

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
    int obstacleCount;
    int obstacleCountRemain;

    float lastMakeFloor;

    FloorTypeData floorTypeData;

    // Use this for initialization
    void Start () {
        
        current = this;

        GameManager.current.state = GameManager.GameState.AssembleTrack;

        floorMeshes = new FloorMesh[floorMeshCount];
        for(int a=0;a!=floorMeshCount;++a)
        {
            GameObject floorObject = (GameObject)Instantiate<GameObject>(floorMeshPrefab);
            floorObject.transform.position = Vector3.zero;
            floorMeshes[a] = floorObject.GetComponent<FloorMesh>();
            floorMeshes[a].width = width;
            floorMeshes[a].length = length;
            floorMeshes[a].index = a;
            floorMeshes[a].coinIndex = -1;
            floorObject.transform.parent = transform;
        }
        floorTypeData = new FloorTypeData();
        floorTypeData.floorCount = initialStraightLength;
        floorTypeData.floorTurningAngle = 0;
        obstacleCount = 0;
        obstacleCountRemain = 0;

        floorMeshes[0].prevPos1 = new Vector3(0, 0, 0);
        floorMeshes[0].prevPos2 = new Vector3(5, 0, 0);
        floorMeshes[0].dir = (new Vector3(0, -0.05f, 0.9f)).normalized;
        floorMeshes[0].prevDir = Vector3.forward;
        floorMeshes[0].makeMesh();

        startIndex = 1;
        endIndex = 0;
        for (int a = 1; a != floorMeshCount; ++a)
        {
            /*remainingFloorCount--;
            if (remainingFloorCount <= 0)
                getFloorTypeByChallengeManager();//refreshFloorType();
            floorMeshes[a].prevDir = floorMeshes[a - 1].dir;
            floorMeshes[a].dir = Quaternion.Euler(0, floorTurningAngle, 0) * floorMeshes[a].prevDir;
            floorMeshes[a].prevPos1 = floorMeshes[a - 1].endPos1;
            floorMeshes[a].prevPos2 = floorMeshes[a - 1].endPos2;
            floorMeshes[a].makeMesh();

            coinRemain--;
            if(coinRemain>=0 && coinRemain < coinCount)
                CoinGenerator.current.putCoin(a);*/
            makeFloorByType();
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
        if( (collidingIndex > startIndex && collidingIndex - startIndex > floorMeshCount / 2) || (collidingIndex < startIndex && collidingIndex + floorMeshCount - startIndex > floorMeshCount / 2)  )
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
        if(floorTypeData.floorCount <= 0)
        {
            getFloorTypeByChallengeManager();//refreshFloorType();
        }
        floorTypeData.floorCount--;

        floorMeshes[startIndex].width = width;
        floorMeshes[startIndex].length = length;
        floorMeshes[startIndex].prevPos1 = floorMeshes[endIndex].endPos1;
        floorMeshes[startIndex].prevPos2 = floorMeshes[endIndex].endPos2;
        floorMeshes[startIndex].prevDir = floorMeshes[endIndex].dir;
        floorMeshes[startIndex].dir = Quaternion.Euler(0, floorTypeData.floorTurningAngle, 0) * floorMeshes[startIndex].prevDir;
        floorMeshes[startIndex].makeMesh();
        
        if(floorTypeData.obstacles != null)
        {
            for (int a = 0; a != floorTypeData.obstacles.Count; ++a)
            {
                ObstacleData obsData = (ObstacleData)floorTypeData.obstacles[a];
                obsData.obstacleStartIndex--;
                if(obsData.obstacleStartIndex == 0 && obsData.obstacleType == ObstacleType.Jump)
                {
                    ObstacleBuilder.current.makeObstacleOnMesh(startIndex, ObstacleType.BeforeJump);
                }
                if (obsData.obstacleStartIndex < 0 && -obsData.obstacleStartIndex <= obsData.obstacleCount)
                {
                    ObstacleBuilder.current.makeObstacleOnMesh(startIndex, obsData.obstacleType);
                }
            }
        }

        floorTypeData.coinStartIndex--;
        if (floorTypeData.coinStartIndex < 0 && -floorTypeData.coinStartIndex <= floorTypeData.coinCount)
            CoinGenerator.current.putCoin(startIndex);
        endIndex = startIndex;
        startIndex = (startIndex + 1) % floorMeshCount;

    }

    void getFloorTypeByChallengeManager()
    {
        floorTypeData = ChallengeManager.current.refreshFloorType(); 
        /*FloorTypeData data = ChallengeManager.current.refreshFloorType();
        remainingFloorCount =   data.floorCount;
        coinCount = data.coinCount;
        coinRemain = Random.Range(coinCount, remainingFloorCount);
        floorTurningAngle = data.floorTurningAngle;
        obstacleCount = data.obstacleCount;
        obstacleCountRemain = Random.Range(obstacleCount, remainingFloorCount);*/
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
            case FloorType.Jump:
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
