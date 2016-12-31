using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public int index;
    public bool isUp;
    public CoinModel model;
	// Use this for initialization
	void Start () {
        isUp = false;
	}

    void OnTriggerEnter(Collider other)
    {
        isUp = false;
        model.touched();
    }

    public void resetCoin()
    {
        model.resetCoin();
    }

    public void passed()
    {
        isUp = false;
        CoinGenerator.current.resetCoins(index);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
