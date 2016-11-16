using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Spells/ProjectileSpell")]
public class ScriptableProjectileSpell : SpellSystemMarkus.ScriptableSpell {
	public GameObject prefab;
	public float knockbackForce = 100F;
	public float spellSpeed = 6F;
	public float timeToLive = 5F;
	public bool destroyOnCollision = false;

	private ProjectileSpellScript projectileSpellScript;

	public override void Initialize(NetworkInstanceId networkId) {
		base.Initialize (networkId);

		GameObject spellInstance = (GameObject)Instantiate (prefab, caster.spellSpawner.position, caster.spellSpawner.rotation);

		projectileSpellScript = spellInstance.GetComponent<ProjectileSpellScript> ();

		projectileSpellScript.knockBackForce = this.knockbackForce;
		projectileSpellScript.spellSpeed = this.spellSpeed;
		projectileSpellScript.timeToLive = this.timeToLive;

		projectileSpellScript.OnCollision += OnCollision;
	}

	public virtual void OnCollision (Collider collider)
	{
		PlayerController playerHit = collider.gameObject.GetComponent<PlayerController>();
        DebugConsole.Log("collision for player: " + playerHit.netId);
		playerHit.ServerOnHit (projectileSpellScript);
	}

	public override GameObject TriggerSpell() {
		projectileSpellScript.Initialize ();
		projectileSpellScript.castSpell ();
		return projectileSpellScript.gameObject;
	}
}
