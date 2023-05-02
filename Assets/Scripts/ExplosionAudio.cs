using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAudio : MonoBehaviour
{
  [Header("Audio Effects")]
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip explosionSound;

  public void PlayExplosionAudio()
  {
    audioSource.clip = explosionSound;
    audioSource.Play();
  }
}
