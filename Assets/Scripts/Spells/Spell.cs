using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour
{

    public abstract float getKnockbackForce();
    public abstract void castSpell();
    public abstract void affectPlayer(GameObject player);

    private Rigidbody rigidbody;
    private PlayerController caster;
    private OnSpellHitEvent onSpellHitEvent = new OnSpellHitEvent();

    // Called when script instance is being loaded
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public PlayerController getCaster()
    {
        return caster;
    }

    public Rigidbody getRigidbody()
    {
        return rigidbody;
    }

    public void setCaster(PlayerController caster)
    {
        this.caster = caster;
        this.castSpell();           //only cast spell once caster is set
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "Player")
        {
            Destroy(gameObject);

            onSpellHitEvent.Invoke(this, c);
            affectPlayer(c.gameObject);
        }
    }
}
