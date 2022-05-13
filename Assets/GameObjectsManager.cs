using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsManager : MonoBehaviour
{
    public static GameObjectsManager instance;

    public Ball[] ballsPool;
    public bool[] ballsExisted;

    public PowerUp[] powerUpsPool;
    public bool[] powerUpsExisted;
    private int activePowerUpsCount;

    [SerializeField]
    float zOffset;
    [SerializeField]
    float powerUpsSpawnInterval;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ballsPool = FindObjectsOfType<Ball>();
        ballsExisted = new bool[ballsPool.Length];
        SetBallActive(0, true);
        for (int i = 1; i < ballsPool.Length; i++)
        {
            SetBallActive(i, false);
        }

        powerUpsPool = FindObjectsOfType<PowerUp>();
        powerUpsExisted = new bool[powerUpsPool.Length];
        activePowerUpsCount = 0;
        for (int i = 0; i < powerUpsPool.Length; i++)
        {
            SetPowerUpActive(i, false);
        }

        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while(true)
        {
            // Prevent instant spawn
            yield return new WaitWhile(() => { return activePowerUpsCount == powerUpsPool.Length; });

            yield return new WaitUntil(() => { return Ball.isLaunched; });
            yield return new WaitForSeconds(powerUpsSpawnInterval);

            // Prevent overlap
            float x;
            float y;
            Collider[] colliders;
            do
            {
                x = Random.Range(-7f, 7f);
                y = Random.Range(-1f, 3f);
                Vector3 position = new Vector3(x, y, zOffset);
                colliders = Physics.OverlapSphere(position, 2f);
            } while (colliders.Length > 0);

            // Spawn a random powerup
            int i = Random.Range(0, powerUpsExisted.Length);
            if (!powerUpsExisted[i])
            {
                SetPowerUpActive(i, true);
                powerUpsPool[i].transform.position = new Vector3(x, y, powerUpsPool[i].transform.position.z);
            }
        }
    }

    public void SetBallActive(Ball ball, bool value)
    {
        for (int i = 0; i < ballsPool.Length; i++)
        {
            if (ballsPool[i] == ball)
            {
                ballsPool[i].gameObject.SetActive(value);
                ballsExisted[i] = value;
                break;
            }
        }
    }
    public void SetBallActive(int index, bool value)
    {
        ballsPool[index].gameObject.SetActive(value);
        ballsExisted[index] = value; 
    }

    public void SetPowerUpActive(PowerUp powerUp, bool value)
    {
        for (int i = 0; i < powerUpsPool.Length; i++)
        {
            if (powerUpsPool[i] == powerUp)
            {
                powerUpsPool[i].gameObject.SetActive(value);
                powerUpsExisted[i] = value;
                activePowerUpsCount = (value == true) ? activePowerUpsCount + 1 : activePowerUpsCount - 1;
                break;
            }
        }
    }
    public void SetPowerUpActive(int index, bool value)
    {
        powerUpsPool[index].gameObject.SetActive(value);
        powerUpsExisted[index] = value;
    }
}
