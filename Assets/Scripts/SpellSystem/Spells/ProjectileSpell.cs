using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class ProjectileSpell : Spell {

    protected int damageDealt;
    public virtual int DamageDealt
    {
        get
        {
            return damageDealt;
        }
    }

	protected float knockBackForce;
    public virtual float KnockBackForce
    {
        get
        {
            return knockBackForce;
        }
    }

    protected float spellSpeed;
    public virtual float SpellSpeed
    {
        get
        {
            return spellSpeed;
        }
    }

    public virtual Vector3 PushDirection
    {
        get
        {
            return transform.forward * -1;
        }
    }

    protected Rigidbody rigidBody;

    public abstract void affectPlayer(PlayerController player);             //affect the player that was hit by the spell in some way
    public abstract bool isDestroyedOnCollision(bool isPlayerCollision);    //determine whether spell should be destroyed on (player-)collision
    public abstract void reactToCollision(Collider c);                      //if spell is not destroyed on collision, it can react

    public void Initialize(PlayerController caster, int level, float timeToLive, int damageDealt, float knockBackForce, float spellSpeed)
    {
        base.Initialize(caster, level, timeToLive);
		rigidBody = GetComponent<Rigidbody> ();

        this.damageDealt = damageDealt;
        this.knockBackForce = knockBackForce;
        this.spellSpeed = spellSpeed;
	}

	void OnTriggerEnter(Collider collider) {
        PlayerController playerHit = collider.gameObject.GetComponent<PlayerController>();

        if (playerHit == null)
        {
            if (isDestroyedOnCollision(false)) { NetworkServer.Destroy(this.gameObject); }
            else { reactToCollision(collider); }
        }
        else
        {
            DebugConsole.Log("collision for player: " + playerHit.netId);
            playerHit.ServerOnHit(this);
            if (isDestroyedOnCollision(true)) { NetworkServer.Destroy(this.gameObject); }
            else { reactToCollision(collider); }
        }
	}


}
