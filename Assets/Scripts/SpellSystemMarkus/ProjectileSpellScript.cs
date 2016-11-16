using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class ProjectileSpellScript : NetworkBehaviour {
	
	[@HideInInspector]
	public float knockBackForce;
	[@HideInInspector]
	public float spellSpeed;
	[@HideInInspector]
	public float timeToLive;
	protected Rigidbody rigidBody;

	public void Initialize() {
		rigidBody = GetComponent<Rigidbody> ();
	}

	public delegate void CollisionEvent (Collider collider);
	public event CollisionEvent OnCollision;

	void OnTriggerEnter(Collider collider) {
		if (OnCollision != null) {
			OnCollision (collider);
		}
	}

	public abstract void castSpell ();
}
