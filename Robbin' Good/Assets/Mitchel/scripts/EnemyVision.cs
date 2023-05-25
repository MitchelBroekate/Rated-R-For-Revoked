using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    int songGenre = 0;
    public void Button()
    {
        songGenre += 1;
    }

    private void Update()
    {
        if(songGenre > 1)
        {
            songGenre = 0;
        }
    }
}
