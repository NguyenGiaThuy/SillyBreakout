using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : PowerUp
{
    protected override void ApplyEffect(GameObject ballObject)
    {
        for (int i = 0; i < GameObjectsManager.instance.ballsPool.Length; i++)
        {
            if (!GameObjectsManager.instance.ballsExisted[i])
            {
                GameObjectsManager.instance.SetBallActive(i, true); 

                int x = Random.Range(-10, 11);
                while (x == 0) 
                { 
                    x = Random.Range(-10, 11); 
                } 
                int y = Random.Range(-10, 11);
                while (y == 0)
                {
                    y = Random.Range(-10, 11);
                }

                Vector3 positionOffset = new Vector3(0.3f, 0.3f, 0f);
                GameObjectsManager.instance.ballsPool[i].SetLaunch(ballObject.transform.position + positionOffset, x, y);
                break;
            }
        }
    }
}
