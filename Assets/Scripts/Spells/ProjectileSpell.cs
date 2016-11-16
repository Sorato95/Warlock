using UnityEngine;
using System.Collections;

public abstract class ProjectileSpell : Spell {

    public abstract bool isDestroyedOnCollision(bool isPlayerCollision);
    public abstract void reactToCollision(Collider c);

    public override void initializeSpell(SpellInitializer init)
    {
        base.initializeSpell(init);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (isDestroyedOnCollision(true))
            {
                Destroy(gameObject);
            }
            else
            {
                reactToCollision(c);
            }

            PlayerController caster = this.getCaster();
            PlayerController playerHit = c.gameObject.GetComponent<PlayerController>();

            Debug.Log("caster:" + caster);
            OnSpellHitEvent hitEvent = playerHit.getOnSpellHitEvent();
            
            hitEvent.Invoke(this, transform.forward);
            affectPlayer(playerHit);
        }
        else
        {
            if (isDestroyedOnCollision(false))
            {
                Destroy(gameObject);
            }
            else
            {
                reactToCollision(c);
            }
        }
    }

}
