using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChallengeManager : MonoBehaviour {

    public static ChallengeManager current;

    public int challengesPerRest;
    public int remainingChallenges;

	// Use this for initialization
	void Start () {
        current = this;
        remainingChallenges = challengesPerRest;
    }

    public FloorTypeData refreshFloorType()
    {
        FloorTypeData data = new FloorTypeData();
        if (remainingChallenges <= 0)
        {
            //Rest by straight
            remainingChallenges = challengesPerRest;

            data.floorCount = 10;
            data.coinCount = Random.Range(0, data.floorCount);
            data.floorTurningAngle = 0;
            data.obstacleCount = 0;
            return data;
        }

        //Generate new floor type
        data.floorType = (FloorType)Random.Range(0, 3);
        //print(floorType);
        switch (data.floorType)
        {
            case FloorType.Straight:
                data.floorCount = Random.Range(0, 3);
                data.coinCount = Random.Range(0, data.floorCount);
                data.floorTurningAngle = 0;
                data.obstacleCount = Random.Range(0, data.floorCount-1);
                remainingChallenges -= data.obstacleCount;
                break;
            case FloorType.SmoothCurve:
                data.floorCount = Random.Range(5, 20);
                data.coinCount = Random.Range(0, data.floorCount);
                data.floorTurningAngle = (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(4.0f, 10.0f);
                data.obstacleCount = Random.Range(0, data.floorCount / 3);
                remainingChallenges -= 2;
                break;
            case FloorType.SteepCurve:
                data.floorCount = Random.Range(5, 10);
                data.coinCount = Random.Range(0, data.floorCount);
                data.floorTurningAngle = (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(10.0f, 15.0f);
                data.obstacleCount = Random.Range(0, data.floorCount / 3);
                remainingChallenges -= 3;
                break;
            case FloorType.UTurn:
                data.floorCount = 10;
                data.coinCount = Random.Range(0, data.floorCount);
                data.floorTurningAngle = (Random.Range(0, 1) > 0.5f ? 1 : -1) * Random.Range(5, 15);
                remainingChallenges -= data.obstacleCount;
                break;
            case FloorType.NarrowWidth:
                break;
            case FloorType.TotalRandom:
                break;

            default:
                break;
        }
        return data;
    } 

    // Update is called once per frame
    void Update () {
		
	}
}
