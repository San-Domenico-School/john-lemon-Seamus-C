using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
   [SerializeField] Transform player;
   [SerializeField] GameEnding gameEnding;

    bool isPlayerInRange;


    // Update is called once per frame
    void Update()
    {
        CanGargoyleSeePlayer();
    }

    private void CanGargoyleSeePlayer ()
    {
        if (isPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                    Debug.Log("Player Hit");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == player.gameObject)
        {
            Debug.Log("Entered Player");
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject == player.gameObject)
        {
            isPlayerInRange = false;
        }
    }
}
