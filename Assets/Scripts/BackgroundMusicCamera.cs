using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicCamera : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private AudioSource _audio;


    private int _currentIndex=0;

    private void Start()
    {
        StartCoroutine(OnStartGame());
    }

    private IEnumerator OnStartGame()
    {
        while (true)
        {
            if (_audio.isPlaying!=false)
            {
                _audio.clip = _clips[_currentIndex];
                _audio.Play();
                _currentIndex++;
            }

        }
    }
}
