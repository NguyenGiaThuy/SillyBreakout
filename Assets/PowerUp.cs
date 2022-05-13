using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public delegate void OnEffectAppliedHandler(PowerUp powerUp);

    protected abstract void ApplyEffect(GameObject ballObject);

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ApplyEffect(other.gameObject);
            GameObjectsManager.instance.SetPowerUpActive(this, false);
        }
    }
}
