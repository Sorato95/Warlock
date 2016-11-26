using UnityEngine;
using System.Collections;
using System;

public class SpeedBoost : Spell
{
    public override float TimeToLive
    {
        get
        {
            return timeToLive + (0.33F * level);
        }
    }

    private int buffIntKey;

    public override void castSpell()
    {
        buffIntKey = caster.MovementManager.addInfluence(2 + (0.1F * level), "Multiply");
    }

    public void OnDestroy()
    {
        if (caster != null)
        {
            caster.MovementManager.removeInfluence(buffIntKey);
        }
    }
}
