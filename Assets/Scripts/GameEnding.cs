using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
* This scripted ends the game when he reaches the level exit and plays audio when loose
*
* component of GameEndingTrigger
*
* @author Seamus
*
* @version 5/21/24
**/


public class GameEnding : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CanvasGroup exitBackgroundImageCanvasGroup;
    [SerializeField] private CanvasGroup caughtBackgroundImageCanvasGroup;
    [SerializeField] private AudioSource exitAudio;
    [SerializeField] private AudioSource caughtAudio;


    private float fadeDuration = 1f;
    private float displayImageDuration = 1f;
    private float timer;
    private bool isPlayerAtExit;
    private bool isPlayerCaught;
    private bool hasAudioPlayed;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if(isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

    }
    
    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    private void EndLevel(CanvasGroup image, bool restartGame, AudioSource audioSource)
    {
       timer += Time.deltaTime;

        image.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayImageDuration)
        {
            if(restartGame)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
        
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

    }   

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }
}
