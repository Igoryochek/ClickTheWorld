using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicCamera : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private bool _isNeedPlay;

    private void Start()
    {
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        while (_isNeedPlay)
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
