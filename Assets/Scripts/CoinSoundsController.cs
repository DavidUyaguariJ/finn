using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundsController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip coin;


    public void coinTouchedPlayer()
    {
        audioSource.PlayOneShot(coin);
    }

}
