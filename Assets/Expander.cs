using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expander : PowerUp
{
    public static event OnEffectAppliedHandler OnEffectApplied;

    public static int? duration = null;

    private void Update()
    {
        if (duration <= 0 && Paddle.instance.isExpanded)
        {
            Paddle.instance.transform.localScale = new Vector3(Paddle.instance.transform.localScale.x,
            Paddle.instance.transform.localScale.y / 1.5f, Paddle.instance.transform.localScale.z);
            Paddle.instance.isExpanded = false;
        }
    }

    protected override void ApplyEffect(GameObject ballObject)
    {
        duration = 15;

        if (!Paddle.instance.isExpanded)
        {
            Paddle.instance.transform.localScale = new Vector3(Paddle.instance.transform.localScale.x, 
                Paddle.instance.transform.localScale.y * 1.5f, Paddle.instance.transform.localScale.z);
            Paddle.instance.isExpanded = true;
            OnEffectApplied?.Invoke(this);
        }
    }
}
