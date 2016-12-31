using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPassTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<Coin>().passed();
    }
}
