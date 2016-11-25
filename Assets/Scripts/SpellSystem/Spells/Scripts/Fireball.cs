using UnityEngine;
using System.Collections;
using System;

public class Fireball : ProjectileSpell {

    public override int DamageDealt
    {
        get
        {
            return damageDealt + (int) Math.Round(0.05F * damageDealt * level);
        }
    }

    public override float KnockBackForce
    {
        get
        {
            return knockBackForce + (10 * level);
        }
    }

    public override float TimeToLive
    {
        get
        {
            return timeToLive + (1 * level);
        }
    }

    public override void castSpell() {
		rigidBody.velocity = this.transform.forward * this.SpellSpeed;
	}

    public override void affectPlayer(PlayerController player)
    {
        //add burn over time effect to player
    }

    public override bool isDestroyedOnCollision(bool isPlayerCollision)
    {
        return true;
    }

    public override void reactToCollision(Collider c)
    {
        return;
    }
}