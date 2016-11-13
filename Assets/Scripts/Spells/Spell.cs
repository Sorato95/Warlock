using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour
{
    //assumes spell is already initialized -- unexpectable behaviour if it isn't
    public abstract void castSpell();           
    public abstract float getKnockbackForce();
    public abstract void affectPlayer(GameObject player);

    private PlayerController caster;
    
    // Called when script instance is being loaded (== sorta-constructor)
    void Awake()
    {
        
    }

    public virtual void initializeSpell(SpellInitializer init)
    {
        caster = init.caster;
    }

    public PlayerController getCaster()
    {
        return caster;
    }

    public Rigidbody getRigidbody()
    {
        return GetComponent<Rigidbody>();
    }




}
