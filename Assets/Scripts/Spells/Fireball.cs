using UnityEngine;
using System.Collections;
using System;

public class Fireball : Spell
{

    public float timeToLive;
    public int shotSpeed = 0;

    private Vector3 moveDirection = Vector3.back;           //only assigned for testing

    // Update is called once per frame
    void Update()
    {

    }

    public override float getKnockbackForce()
    {
        return 150;
    }

    public override void castSpell()
    {
        this.gameObject.transform.position = this.getCaster().gameObject.transform.position - Vector3.back;
        getRigidbody().AddForce(moveDirection * shotSpeed);
        Destroy(gameObject, timeToLive);                    //destroy bullet after <timeToLive> seconds
    }

    public override void affectPlayer(GameObject player)
    {
        Debug.Log(player.name + "sagt 'Aua!'");
    }

}
