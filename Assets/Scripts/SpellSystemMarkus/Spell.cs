using UnityEngine;
using System.Collections;


namespace SpellSystemMarkus {
	public class Spell : ScriptableObject {
		public string name = "New Spell";

		private PlayerController caster;

		public virtual void Initialize(PlayerController playerController) {
			playerController = caster;
		}

		public abstract void TriggerSpell();
	}
}