using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public int musicGenre = 0;
    public GameObject synthMusic;
    public GameObject electroMusic;

    // Update is called once per frame
    void Update()
    {
        if (musicGenre > 1)
        {
            musicGenre = 0;
        }

        if (musicGenre == 1)
        {
            electroMusic.SetActive(true);
            synthMusic.SetActive(false);
        }
        else
        {
            synthMusic.SetActive(true);
            electroMusic.SetActive(false);
        }
    }

    public void ChangeGenre()
    {
        musicGenre += 1;
    }
}
