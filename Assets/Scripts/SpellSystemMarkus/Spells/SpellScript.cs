using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class SpellScript : NetworkBehaviour {

    [@HideInInspector]
    public PlayerController caster;

    public abstract void castSpell();

    public virtual void Initialize(PlayerController caster)
    {
        this.caster = caster;
    }
}
