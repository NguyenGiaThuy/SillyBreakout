using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public static Paddle instance;

    public bool isExpanded;
    public bool isShooting;

    [SerializeField]
    private GameObject shootingPoint1;
    [SerializeField]
    private GameObject shootingPoint2;
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float rateOfFire;
    [SerializeField]
    private float smoothTime;
    private float xVelocity = 0f;

    [SerializeField]
    private float minEdge;
    [SerializeField]
    private float maxEdge;
    [SerializeField]
    private float zOffset;

    private float rateOfFireCountdown;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FindObjectOfType<Ball>().transform.parent = transform;
        isExpanded = false;
        isShooting = false;
        rateOfFireCountdown = rateOfFire;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float newPositionX = Mathf.SmoothDamp(transform.position.x, worldPosition.x, ref xVelocity, smoothTime);
        newPositionX = Mathf.Clamp(newPositionX, minEdge, maxEdge);
        transform.position = new Vector3(newPositionX, transform.position.y, zOffset);

        if (isShooting)
        {
            rateOfFireCountdown -= Time.deltaTime;
            if (rateOfFireCountdown <= 0f)
            {
                Shoot();
                rateOfFireCountdown = rateOfFire;
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootingPoint1.transform.position, Quaternion.identity);
        Instantiate(bulletPrefab, shootingPoint2.transform.position, Quaternion.identity);
    }
}
