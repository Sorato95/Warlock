using UnityEngine;
using System.Collections;

public abstract class ProjectileSpell : Spell {

    public abstract bool isDestroyedOnCollision(bool isPlayerCollision);
    public abstract void reactToCollision(Collider c);

    private OnSpellHitEvent onSpellHitEvent;

    public override void initializeSpell(SpellInitializer init)
    {
        base.initializeSpell(init);
        onSpellHitEvent = init.onSpellHitEvent;
    }

    public OnSpellHitEvent getOnSpellHitEvent()
    {
        return onSpellHitEvent;
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

            onSpellHitEvent.Invoke(this, transform.forward);
            affectPlayer(c.gameObject);
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
