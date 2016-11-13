using UnityEngine;
using System.Collections;

public class SpellInitializer {

    public PlayerController caster;
    public int spellLevel;

    public SpellInitializer(PlayerController caster, int spellLevel)
    {
        this.caster = caster;
        this.spellLevel = spellLevel;
    }
}
