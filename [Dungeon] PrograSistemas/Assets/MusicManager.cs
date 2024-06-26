using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> musicClipList;
    private AudioSource myAudio;

    private int previousClipIndex = -1;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();

        if (myAudio == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject. Please add one.");
            return;
        }

        if (musicClipList.Count == 0)
        {
            Debug.LogError("No AudioClips assigned to the Jukebox. Please add some AudioClips.");
            return;
        }

        PlayRandomClip();
    }

    private void Update()
    {
        if (!myAudio.isPlaying)
        {
            PlayRandomClip();
        }
    }

    private void PlayRandomClip()
    {
        if (musicClipList.Count <= 1)
        {
            myAudio.clip = musicClipList[0];
        }
        else
        {
            int newClipIndex;
            do
            {
                newClipIndex = Random.Range(0, musicClipList.Count);
            } while (newClipIndex == previousClipIndex);

            previousClipIndex = newClipIndex;
            myAudio.clip = musicClipList[newClipIndex];
        }

        myAudio.Play();
    }
}

