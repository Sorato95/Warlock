using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace SpellSystemMarkus {
	public abstract class ScriptableSpell : ScriptableObject {
		public string spellName = "New Spell";

		protected PlayerController caster;
		protected NetworkInstanceId casterNetworkId;

		public virtual void Initialize(NetworkInstanceId networkId) {
			this.caster = ClientScene.FindLocalObject (networkId).GetComponent<PlayerController>();
			this.casterNetworkId = networkId;
		}

		public abstract GameObject TriggerSpell();
	}
}