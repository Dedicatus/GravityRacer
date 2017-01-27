using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChallengeManager : MonoBehaviour {

    public static ChallengeManager current;

    public int challengesPerRest;
    public int remainingChallenges;

	public float[] challengeChances;

    public float startTime;
    public float getHardTimeRemain;

    // Use this for initialization
    void Start () {
        current = this;
        remainingChallenges = challengesPerRest;
    }

	public FloorType randomByPresetChance() {
		float randomRange = 100;
		float randomNumber = Random.Range(0,randomRange);
		float baseValue = 0;

		for(int i=0; i<challengeChances.Length;i++){
			float chance = baseValue + challengeChances [i] * randomRange;
			if(randomNumber <= chance) {
				return (FloorType)i;
			}
			baseValue = baseValue + (challengeChances [i] * randomRange);
		}
		//end

		int k=0;
		FloorType res = (FloorType)k;
		return res;
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
            //data.obstacleCount = 0;
            return data;
        }

        //Generate new floor type
		data.floorType = randomByPresetChance();//(FloorType)Random.Range(0, 4);
        data.obstacles = new ArrayList();
        ObstacleData obstacleData = new ObstacleData();
        //print(floorType);
        switch (data.floorType)
        {
            case FloorType.Straight:
                data.floorCount = Random.Range(0, 3);
                data.coinCount = Random.Range(0, data.floorCount);
                data.coinStartIndex = Random.Range(0, data.floorCount - data.coinCount);
                data.floorTurningAngle = 0;
                obstacleData.obstacleType = ObstacleType.Cube;
                obstacleData.obstacleCount = Random.Range(0, data.floorCount - 1);
                obstacleData.obstacleStartIndex = Random.Range(0, data.floorCount - obstacleData.obstacleCount);
                data.obstacles.Add(obstacleData);
                remainingChallenges -= obstacleData.obstacleCount/3;
                break;
            case FloorType.SmoothCurve:
                data.floorCount = Random.Range(5, 20);
                data.coinCount = Random.Range(0, data.floorCount);
                data.coinStartIndex = Random.Range(0, data.floorCount - data.coinCount);
                data.floorTurningAngle = (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(4.0f, 10.0f);
                obstacleData.obstacleType = ObstacleType.Cube;
                obstacleData.obstacleCount = Random.Range(0, data.floorCount / 3);
                obstacleData.obstacleStartIndex = Random.Range(0, data.floorCount - obstacleData.obstacleCount);
                data.obstacles.Add(obstacleData);
                remainingChallenges -= 2;
                break;
            case FloorType.SteepCurve:
                data.floorCount = Random.Range(5, 10);
                data.coinCount = Random.Range(0, data.floorCount);
                data.coinStartIndex = Random.Range(0, data.floorCount - data.coinCount);
                data.floorTurningAngle = (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(10.0f, 15.0f);
                obstacleData.obstacleType = ObstacleType.Cube;
                obstacleData.obstacleCount = Random.Range(0, data.floorCount / 3);
                obstacleData.obstacleStartIndex = Random.Range(0, data.floorCount - obstacleData.obstacleCount);
                data.obstacles.Add(obstacleData);
                remainingChallenges -= 3;
                break;
            case FloorType.Jump:
                data.floorCount = 10;
                data.coinCount = Random.Range(0, data.floorCount);
                data.coinStartIndex = Random.Range(0, data.floorCount - data.coinCount);
                data.floorTurningAngle = 0;
                obstacleData.obstacleType = ObstacleType.Jump;
                obstacleData.obstacleCount = 1;
                obstacleData.obstacleStartIndex = Random.Range(0, data.floorCount - obstacleData.obstacleCount);
                data.obstacles.Add(obstacleData);
                remainingChallenges -= 2;
                break;
            case FloorType.NarrowWidth:
                data.floorCount = 10;
                data.coinCount = Random.Range(0, data.floorCount);
                data.coinStartIndex = Random.Range(0, data.floorCount - data.coinCount);
                data.floorTurningAngle = (Random.Range(0.0f, 1.0f) > 0.5f ? 1 : -1) * Random.Range(4.0f, 10.0f);
                remainingChallenges -= 2;
                break;
            case FloorType.TotalRandom:
                break;

            default:
                break;
        }

        return data;
    }



    void addDifficulty()
    {
        print(Time.deltaTime);
        if (FloorBuilder.current.width > 10.0f)
        {
            FloorBuilder.current.width -= 1.0f;
            Time.timeScale += 0.1f;
        }
    }
    // Update is called once per frame
    void Update () {

        getHardTimeRemain -= Time.deltaTime;
        if (getHardTimeRemain <= 0)
        {
            addDifficulty();
            getHardTimeRemain = 30.0f;
        }
    }
}
