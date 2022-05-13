using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private int hp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hp -= Bullet.damage;
            if(hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            hp -= Ball.damage;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
