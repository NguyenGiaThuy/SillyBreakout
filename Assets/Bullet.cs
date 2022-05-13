using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static int damage = 1;

    [SerializeField]
    private float speed;

    private Vector3 velocity;

    private void Start()
    {
        velocity = Vector3.up;
    }

    private void FixedUpdate()
    {
        transform.Translate(velocity * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Objective" || other.tag == "Wall")
        {
            Destroy(gameObject, 0.01f);
        }
    }
}
