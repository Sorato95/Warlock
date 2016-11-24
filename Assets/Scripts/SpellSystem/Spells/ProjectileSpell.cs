using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class ProjectileSpell : Spell {
	
	protected float knockBackForce;
    public float KnockBackForce
    {
        get
        {
            return knockBackForce;
        }
    }

    protected float spellSpeed;
    public float SpellSpeed
    {
        get
        {
            return spellSpeed;
        }
    }

    protected float timeToLive;
    public float TimeToLive
    {
        get
        {
            return timeToLive;
        }
    }

    protected Rigidbody rigidBody;

    public abstract void reactToCollision(Collider c);
    public abstract bool isDestroyedOnCollision(bool isPlayerCollision);

    public void Initialize(PlayerController caster, int level, float knockBackForce, float spellSpeed, float timeToLive)
    {
        base.Initialize(caster, level);
		rigidBody = GetComponent<Rigidbody> ();

        this.knockBackForce = knockBackForce;
        this.spellSpeed = spellSpeed;
        this.timeToLive = timeToLive;
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
            playerHit.ServerOnHit(this);
            if (isDestroyedOnCollision(true)) { NetworkServer.Destroy(this.gameObject); }
            else { reactToCollision(collider); }

            DebugConsole.Log("collision for player: " + playerHit.netId);
        }
	}


}
