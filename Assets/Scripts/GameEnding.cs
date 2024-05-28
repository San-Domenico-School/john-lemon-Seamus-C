using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
* This scripted ends the game when he reaches the level exit
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

    private float fadeDuration = 1f;
    private float displayImageDuration = 1f;
    private float timer;
    private bool isPlayerAtExit;
    private bool isPlayerCaught;


    // Update is called once per frame
    void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false);
        }
        else if(isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true);
        }

    }
    
    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    private void EndLevel(CanvasGroup image, bool restartGame)
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
    }   

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }
}
