using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip damage;
    public AudioClip attack;
    public AudioClip dead;
    public AudioClip stepOne;
    public AudioClip stepTwo;

    public void jumpPlayer() 
    {
        audioSource.PlayOneShot(jump);
    }

    public void damagePlayer()
    {
        audioSource.PlayOneShot(damage);
    }

    public void attackPlayer()
    {
        audioSource.PlayOneShot(attack);
    }

    public void deadPlayer()
    {
        audioSource.PlayOneShot(dead);
    }

    public void stepOnePlayer()
    {
        audioSource.PlayOneShot(stepOne);
    }

    public void stepTwoPlayer()
    {
        audioSource.PlayOneShot(stepTwo);
    }

}
