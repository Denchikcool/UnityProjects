using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Coin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            PlayerManager.numberOfCoins += 1;
            PlayerManager.totalCoins += 1;
            Destroy(gameObject);
        }
    }
}
