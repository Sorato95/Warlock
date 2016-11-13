using UnityEngine;
using System.Collections;
using System;

public class Fireball : ProjectileSpell {

    public float timeToLive;            //assigned by Inspector
    public int shotSpeed;               //assigned by Inspector
    public float knockbackForce;        //assigned by Inspector

    private Vector3 moveDirection = Vector3.back;           //only assigned for testing

    public override float getKnockbackForce()
    {
        return knockbackForce;
    }

    public override void castSpell()
    {
        getRigidbody().AddForce(this.transform.forward * shotSpeed);
        Destroy(gameObject, timeToLive);                    //destroy bullet after <timeToLive> seconds
    }

    public override void affectPlayer(GameObject player)
    {
        Debug.Log(player.name + " sagt 'Aua!'");
    }

    public override bool isDestroyedOnCollision(bool isPlayerCollision) { return true; }
    public override void reactToCollision(Collider c) { return; }
}
