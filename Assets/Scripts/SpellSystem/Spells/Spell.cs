using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class Spell : NetworkBehaviour {

    protected PlayerController caster;
    public PlayerController Caster
    {
        get
        {
            return caster;
        }
    }

    protected float timeToLive;
    public virtual float TimeToLive
    {
        get
        {
            return timeToLive;
        }
    }

    protected int level;
    public int Level
    {
        get
        {
            return level;
        }
        //spellLevel can't be modified once it's generated -> no set
    }

    public abstract void castSpell();

    public virtual void Initialize(PlayerController caster, int level, float timeToLive)
    {
        this.caster = caster;
        this.level = level;
        this.timeToLive = timeToLive;
    }
}
