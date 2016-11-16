using UnityEngine;
using System.Collections;

namespace SpellSystemMarkus {
	[CreateAssetMenu(menuName = "Spells/ProjectileSpell")]
	public class ProjectileSpell : SpellSystemMarkus.Spell {
		public GameObject prefab;
		public float knockbackForce = 100F;
		public float spellSpeed = 6F;
		public int timeToLive = 5;

		public override void TriggerSpell() {
			ISpellScript spell = this.prefab.GetComponent<ISpellScript> ();
		}
	}
}