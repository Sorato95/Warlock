using UnityEngine;
using System.Collections;
using System;

public class SpeedBoost : Spell
{

    private bool isPlayerBoosted = false;
    private float secondsBoosted = 0;
    private float speedDifference;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerBoosted)
        {
            Destroy(this.gameObject, 2 + (0.5F * getSpellLevel()));
            isPlayerBoosted = false;
        }
    }

    public void OnDestroy()
    {
        if (getCaster() != null)
        {
            getCaster().movementManager.impact(speedDifference, "Add");
        }
    }

    public override void affectPlayer(PlayerController player)
    {

    }

    public override void castSpell()
    {
        speedDifference = getCaster().movementManager.impact(1 + getSpellLevel(), "Multiply");
        isPlayerBoosted = true;
    }

    public override float getKnockbackForce()
    {
        return 0;
    }

}
