using UnityEngine;
using System.Collections;

namespace SpellSystemMarkus {
	public class Fireball : ProjectileSpellScript {

		public override void castSpell() {
			rigidBody.velocity = this.transform.forward * this.spellSpeed;
			Destroy(gameObject, this.timeToLive);
		}

	}
}