using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public CoinSoundsController coinSoundsController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinSoundsController.coinTouchedPlayer();
            Destroy(gameObject);
        }
    }
}
