using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Songs Attributes")]
    public AudioSource audioSource;
    public AudioClip[] songs;
    public float volume;
    [SerializeField] public float trackTimer;
    private float songsPlayed;
    private bool[] beenPlayed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        beenPlayed = new bool[songs.Length];

        if (!audioSource.isPlaying)
        {
            ChangeSong(Random.Range(0, songs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = volume;

        if (audioSource.isPlaying)
        {
            trackTimer += 1 * Time.deltaTime;
        }

        if (!audioSource.isPlaying || trackTimer >=audioSource.clip.length)
        {
            ChangeSong(Random.Range(0, songs.Length));
        }
        ResetShuffle();
    }

    public void ChangeSong(int songPicked)
    {
        if (!beenPlayed[songPicked])
        {
            trackTimer = 0;
            songsPlayed++;
            beenPlayed[songPicked] = true;
            audioSource.clip = songs[songPicked];
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void ResetShuffle()
    {
        if (songsPlayed == songs.Length)
        {
            songsPlayed = 0;
            for (int i = 0; i < songs.Length; i++)
            {
                if (i == songs.Length)
                    break;
                else
                    beenPlayed[i] = false;
            }
        }
    }
}
