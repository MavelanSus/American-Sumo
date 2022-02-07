using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] int maxCollisions;
    int collisions;
    [SerializeField] float lifetime;
    [SerializeField] GameObject particles;
    [SerializeField] int playerLayer;
    void Start()
    {
        Invoke("DestroyObject", lifetime);
    }
    void Update()
    {
        if(collisions > maxCollisions)
        {
            DestroyObject();
        }
    }
    void DestroyObject()
    {
        GameObject particlesGO = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        if (collision.gameObject.GetComponent<SumoPlayerMovement>())
        {
            DestroyObject();
        }
    }
}
