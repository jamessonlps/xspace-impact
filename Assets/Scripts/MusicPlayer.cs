using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
  public List<AudioClip> songs; // lista de músicas definidas no Unity Editor

  private List<AudioClip> currentSongs; // lista de músicas atualmente em reprodução
  private AudioClip currentSong; // música atual

  private AudioSource audioSource;

  void Start()
  {
    currentSongs = new List<AudioClip>(songs); // copia a lista de músicas
    audioSource = GetComponent<AudioSource>();
    PlayRandomSong(); // começa a reproduzir uma música aleatória
  }

  void Update()
  {
    if (!audioSource.isPlaying) // se a música atual terminou de tocar
    {
      PlayRandomSong(); // escolhe a próxima música aleatoriamente e reproduz
    }
  }

  void PlayRandomSong()
  {
    int songIndex = Random.Range(0, currentSongs.Count); // escolhe um índice aleatório na lista
    currentSong = currentSongs[songIndex]; // armazena a música atual
    currentSongs.RemoveAt(songIndex); // remove a música atual da lista para evitar repetição
    if (currentSongs.Count == 0) // se a lista estiver vazia
    {
      currentSongs = new List<AudioClip>(songs); // reinicia a lista
    }
    audioSource.clip = currentSong; // define a música atual no AudioSource
    audioSource.Play(); // reproduz a música
  }
}
