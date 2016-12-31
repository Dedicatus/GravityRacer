using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public static CoinGenerator current;

    public int coinCount;
    public GameObject coinPrefab;

    int currentCoinIndex;
    public Coin[] coins;


	// Use this for initialization
	void Awake () {
        current = this;
        currentCoinIndex = 0;
        coins = new Coin[coinCount];
        for (int a = 0; a != coinCount; ++a)
        {
            GameObject coinObject = (GameObject)Instantiate<GameObject>(coinPrefab);
            coinObject.transform.position = Vector3.zero;
            coins[a] = coinObject.GetComponent<Coin>();
            coins[a].index = a;
            coinObject.transform.parent = transform;
        }
    }

    public void putCoin(int meshIndex)
    {
        if (coins[currentCoinIndex].isUp) return;
        coins[currentCoinIndex].isUp = true;
        coins[currentCoinIndex].resetCoin();
        FloorMesh floorMesh = FloorBuilder.current.floorMeshes[meshIndex];
        GameObject obj = coins[currentCoinIndex++].gameObject;
        Vector3 cross = Vector3.Cross(floorMesh.prevDir, floorMesh.dir);
        //print(dot);
        float posScale = cross.y < 0 ? 0.8f : 0.2f;
        Vector3 prevPosMid = floorMesh.prevPos1 + (floorMesh.prevPos2 - floorMesh.prevPos1) * posScale;
        prevPosMid += floorMesh.dir * floorMesh.length / 2.0f;
        prevPosMid.y += 1.0f;
        
        obj.transform.position = prevPosMid;
        obj.transform.forward = floorMesh.prevDir;
        if (currentCoinIndex >= coinCount)
            currentCoinIndex = 0;
    }
    
    public void resetCoins(int start)
    {
        int end = currentCoinIndex;
        if (start > end)
        {
            int t = start;
            start = end;
            end = t;
        }
        for(int a=start; a<=end;++a)
        {
            coins[a].isUp = false;
        }
    }

    void Update () {
		
	}
}
