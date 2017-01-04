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

    public void startAnim()
    {
        if (gameObject.GetComponent<MoveToDecSpeedWithoutRot>() == null)
            gameObject.AddComponent<MoveToDecSpeedWithoutRot>();
        MoveToDecSpeedWithoutRot anim = gameObject.GetComponent<MoveToDecSpeedWithoutRot>();
        anim.maxSpeed = 100;
        anim.minSpeed = 50;
        anim.to = transform.position;
        transform.position = transform.position + new Vector3(0, 100, 0);
        anim.from = transform.position;
        anim.resetAnim();
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
