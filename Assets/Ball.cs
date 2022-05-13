using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static bool isLaunched = false;
    public static bool isDecelerated = false;
    public static float speed = 6f;
    public static int damage = 2;

    private Vector3 velocity;

    private void OnEnable()
    {
        velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        // Lauch ball
        if (Input.GetMouseButtonDown(0) && transform.parent != null && !isLaunched)
        {
            int x = Random.Range(-1, 2);
            while (x == 0)
            {
                x = Random.Range(-1, 2);
            }
            float y = 1f;
            SetLaunch(transform.position, x, y);
            isLaunched = true;
        }

        // Prevent getting stuck
        if (velocity == Vector3.up || velocity == Vector3.down)
        {
            int x = Random.Range(-1, 2);
            while (x == 0)
            {
                x = Random.Range(-1, 2);
            }
            float y = velocity.y;
            SetLaunch(transform.position, x, y);
        }
        if (velocity == Vector3.left || velocity == Vector3.right)
        {
            float x = velocity.x;
            int y = Random.Range(-1, 2);
            while (y == 0)
            {
                y = Random.Range(-1, 2);
            }
            SetLaunch(transform.position, x, y);
        }
    }

    private void FixedUpdate()
    {
        if(isLaunched)
        {
            transform.Translate(velocity * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameOver")
        {
            GameObjectsManager.instance.SetBallActive(this, false);
        }
    }

    public void SetLaunch(Vector3 launchPosition, float xVelocity, float yVelocity)
    {
        transform.parent = null;
        transform.position = launchPosition;
        velocity = new Vector3(xVelocity, yVelocity, 0f).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.GetContact(0);
        velocity = Vector3.Reflect(velocity, hit.normal);
    }
}
