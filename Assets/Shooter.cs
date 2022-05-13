using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : PowerUp
{
    public static event OnEffectAppliedHandler OnEffectApplied;

    public static int? duration = null;

    private void Update()
    {
        if (duration <= 0 && Paddle.instance.isShooting)
        {
            Paddle.instance.isShooting = false;
        }
    }

    protected override void ApplyEffect(GameObject ballObject)
    {
        duration = 10;

        if (!Paddle.instance.isShooting)
        {
            Paddle.instance.isShooting = true;
            OnEffectApplied?.Invoke(this);
        }
    }
}
