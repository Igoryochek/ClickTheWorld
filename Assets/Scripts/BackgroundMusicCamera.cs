using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicCamera : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private AudioSource _audio;

    private void Start()
    {
        StartCoroutine(OnStartGame());
    }

    private IEnumerator OnStartGame()
    {
        while (true)
        {
            if (_audio.isPlaying == false)
            {
                int randomIndex = Random.Range(0, _clips.Count);
                _audio.clip = _clips[randomIndex];
                _audio.Play();
            }
            yield return null;
        }
    }
}
