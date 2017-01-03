using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public static CoinGenerator current;

    public int coinCount;
    public GameObject coinPrefab;

    int currentCoinIndex;
    public Coin[] coins;
    public Queue<Coin> notShowingCoins;

	// Use this for initialization
	void Awake () {
        current = this;
        currentCoinIndex = 0;
        coins = new Coin[coinCount];
        notShowingCoins = new Queue<Coin>();
        for (int a = 0; a != coinCount; ++a)
        {
            GameObject coinObject = (GameObject)Instantiate<GameObject>(coinPrefab);
            coinObject.transform.position = Vector3.zero;
            coins[a] = coinObject.GetComponent<Coin>();
            coins[a].index = a;
            coins[a].disableCoin();
            notShowingCoins.Enqueue(coins[a]);
            coinObject.transform.parent = transform;
        }
    }

    public void putCoin(int meshIndex)
    {
        if (notShowingCoins.Count == 0) return;
        Coin coin = notShowingCoins.Dequeue();
        coin.resetCoin();
        FloorMesh floorMesh = FloorBuilder.current.floorMeshes[meshIndex];
        coin.meshIndex = meshIndex;
        floorMesh.coinIndex = coin.index;
        GameObject obj = coin.gameObject;
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

    IEnumerator resetCoinAfterSecond(float second, int index)
    {
        yield return new WaitForSeconds(second);
        //resetCoin(index);
    }

    public void disableCoin(int index)
    {
        //print(index);
        if (index < 0 || index >= coinCount) return;
        notShowingCoins.Enqueue(coins[index]);
        coins[index].disableCoin();
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
        }
    }

    void Update () {
		
	}
}
