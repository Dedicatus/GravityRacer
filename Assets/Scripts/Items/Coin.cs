using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public int index;
    public int meshIndex;

    public bool isShowing;

    public CoinModel model;
	// Use this for initialization
	void Start () {
        isShowing = false;
        disableCoin();
	}

    void OnTriggerEnter(Collider other)
    {
        model.touched();
        GameManager.current.coinCount++;
        //FloorBuilder.current.floorMeshes[meshIndex].coinIndex = -1;
    }

    public void resetCoin()
    {
        model.resetCoin();
        GetComponent<BoxCollider>().enabled = true;
    }

    public void disableCoin()
    {
        model.disabled();
        GetComponent<BoxCollider>().enabled = false;
    }

    public void passed()
    {
        //CoinGenerator.current.resetCoins(index);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
