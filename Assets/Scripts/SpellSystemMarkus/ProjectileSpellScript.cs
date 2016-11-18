using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class ProjectileSpellScript : SpellScript {
	
	[@HideInInspector]
	public float knockBackForce;
	[@HideInInspector]
	public float spellSpeed;
	[@HideInInspector]
	public float timeToLive;
	protected Rigidbody rigidBody;

    public abstract void reactToCollision(Collider c);
    public abstract bool isDestroyedOnCollision(bool isPlayerCollision);

    public void Initialize(PlayerController caster, float knockBackForce, float spellSpeed, float timeToLive)
    {
        base.Initialize(caster);
		rigidBody = GetComponent<Rigidbody> ();

        this.knockBackForce = knockBackForce;
        this.spellSpeed = spellSpeed;
        this.timeToLive = timeToLive;
	}

	/*public delegate void CollisionEvent (Collider collider);
	public event CollisionEvent OnCollisionEvent;*/

	void OnTriggerEnter(Collider collider) {
		//if (OnCollisionEvent != null) {
            //OnCollisionEvent(collider);

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
        }

        DebugConsole.Log("collision for player: " + playerHit.netId);
        
        //}
	}


}
