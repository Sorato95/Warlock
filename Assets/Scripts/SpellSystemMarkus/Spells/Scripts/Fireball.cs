using UnityEngine;
using System.Collections;
using System;

public class Fireball : ProjectileSpellScript {

	public override void castSpell() {
		rigidBody.velocity = this.transform.forward * this.spellSpeed;
		Destroy(gameObject, this.timeToLive);
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