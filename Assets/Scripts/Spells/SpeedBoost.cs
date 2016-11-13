using UnityEngine;
using System.Collections;
using System;

public class SpeedBoost : Spell
{

    private bool isPlayerBoosted = false;
    private float secondsBoosted = 0;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerBoosted)
        {
            Destroy(this.gameObject, 2);
            isPlayerBoosted = false;
        }
    }

    public void OnDestroy()
    {
        if (getCaster() != null)
        {
            getCaster().curMoveSpeed = getCaster().curMoveSpeed / (2 + getSpellLevel());
        }
    }

    public override void affectPlayer(PlayerController player)
    {

    }

    public override void castSpell()
    {
        getCaster().curMoveSpeed = getCaster().curMoveSpeed * (2 + getSpellLevel());
        isPlayerBoosted = true;
    }

    public override float getKnockbackForce()
    {
        return 0;
    }

}
