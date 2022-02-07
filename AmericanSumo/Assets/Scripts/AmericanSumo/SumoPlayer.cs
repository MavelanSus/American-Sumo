using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoPlayer : MonoBehaviour
{
    public bool blue;
    [SerializeField] WinManager win;

    private void Start()
    {
        if (blue)
        {
            win.bluePlayersAlive++;
        }
        else
        {
            win.redPlayersAlive++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
                if (blue)
                {
                win.bluePlayersAlive--;
                gameObject.SetActive(false);
                }
                else
                {
                win.redPlayersAlive--;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            if (blue)
            {
                win.Win("blue");
            }
            else
            {
                win.Win("red");
            }
        }
    }
}
