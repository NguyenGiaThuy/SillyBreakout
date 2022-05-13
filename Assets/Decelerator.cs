using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decelerator : PowerUp
{
    public static event OnEffectAppliedHandler OnEffectApplied;

    public static int? duration = null;

    private void Update()
    {
        if (duration <= 0 && Ball.isDecelerated)
        {
            Ball.speed *= 2;
            Ball.isDecelerated = false;
        }
    }

    protected override void ApplyEffect(GameObject ballObject)
    {
        duration = 15;

        if (!Ball.isDecelerated)
        {
            Ball.speed /= 2;
            Ball.isDecelerated = true;
            OnEffectApplied?.Invoke(this);
        }
    }
}
