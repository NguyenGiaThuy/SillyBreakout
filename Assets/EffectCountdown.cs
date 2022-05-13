using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCountdown : MonoBehaviour
{
    public static EffectCountdown instance;

    private void Awake()
    {
        instance = this;
        Expander.OnEffectApplied += Expander_OnEffectApplied;
        Decelerator.OnEffectApplied += Decelerator_OnEffectApplied;
        Shooter.OnEffectApplied += Shooter_OnEffectApplied;
    }

    private void Shooter_OnEffectApplied(PowerUp shooterPowerUp)
    {
        StartCoroutine(ShooterExpire());
    }
    private IEnumerator ShooterExpire()
    {
        while(Shooter.duration > 0)
        {
            Shooter.duration--;
            yield return new WaitForSeconds(1f);
        }
    }

    private void Decelerator_OnEffectApplied(PowerUp deceleratorPowerUp)
    {
        StartCoroutine(DeceleratorExpire());
    }
    private IEnumerator DeceleratorExpire()
    {
        while (Decelerator.duration > 0)
        {
            Decelerator.duration--;
            yield return new WaitForSeconds(1f);
        }
    }


    private void Expander_OnEffectApplied(PowerUp expanderPowerUp)
    {
        StartCoroutine(ExpanderExpire());
    }
    private IEnumerator ExpanderExpire()
    {
        while (Expander.duration > 0)
        {
            Expander.duration--;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDestroy()
    {
        Expander.OnEffectApplied -= Expander_OnEffectApplied;
        Decelerator.OnEffectApplied -= Decelerator_OnEffectApplied;
        Shooter.OnEffectApplied -= Shooter_OnEffectApplied;
    }
}
