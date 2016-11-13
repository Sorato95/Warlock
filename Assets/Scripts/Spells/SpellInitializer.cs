using UnityEngine;
using System.Collections;

public class SpellInitializer {

    public PlayerController caster;
    public OnSpellHitEvent onSpellHitEvent;

    public SpellInitializer(PlayerController caster)
    {
        this.caster = caster;
    }

    public SpellInitializer(PlayerController caster, OnSpellHitEvent onSpellHitEvent)
    {
        this.caster = caster;
        this.onSpellHitEvent = onSpellHitEvent;
    }
}
