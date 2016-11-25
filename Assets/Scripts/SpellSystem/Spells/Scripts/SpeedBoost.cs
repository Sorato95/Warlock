using UnityEngine;
using System.Collections;
using System;

public class SpeedBoost : Spell
{
    private int buffIntKey;

    public override void castSpell()
    {
        Debug.Log("SpeedBoost = cast");
        buffIntKey = caster.movementManager.addInfluence(2 , "Multiply");
        Destroy(this.gameObject, 2 + (0.5F ));
    }

    public void OnDestroy()
    {
        if (caster != null)
        {
            caster.movementManager.removeInfluence(buffIntKey);
        }
    }
}
