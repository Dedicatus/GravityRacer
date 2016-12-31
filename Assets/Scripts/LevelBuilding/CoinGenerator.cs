using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public static CoinGenerator current;

    public int coinCount;
    public GameObject coinPrefab;

    public Coin[] coins;

	// Use this for initialization
	void Start () {
        current = this;
        coins = new Coin[coinCount];
        for (int a = 0; a != coinCount; ++a)
        {
            GameObject coinObject = (GameObject)Instantiate<GameObject>(coinPrefab);
            coinObject.transform.position = Vector3.zero;
            coins[a] = coinObject.GetComponent<Coin>();
            coinObject.transform.parent = transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
