using UnityEngine;
using System.Collections;

public abstract class SpellBookItem : ScriptableObject {
    public abstract int getSpellLevel();
    public abstract void incrementSpellLevel();
    public abstract void setSpellLevel(int spellLevel);
    public abstract GameObject generateSpell(PlayerController caster);
}
