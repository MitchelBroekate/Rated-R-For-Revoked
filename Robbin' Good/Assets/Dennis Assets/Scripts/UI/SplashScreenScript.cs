using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    public GameObject splashScreen;
    public AudioSource splashAudioSource;
    public float screenLoad = 7f;
    public float audioWait = 1.04f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SplashScreenLoad(screenLoad));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SplashScreenLoad (float screenLoad)
    {
        yield return new WaitForSeconds(audioWait);
        splashAudioSource.Play();

        yield return new WaitForSeconds(screenLoad);
        SceneManager.LoadScene("Dennis");
    }
}
