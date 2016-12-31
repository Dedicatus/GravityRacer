using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void touched()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void resetCoin()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
