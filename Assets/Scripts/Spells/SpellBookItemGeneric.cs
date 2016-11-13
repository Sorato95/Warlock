using UnityEngine;
using System.Collections;

public class SpellBookItem <T> : SpellBookItem where T : Spell{

    private GameObject spellPrefab;
    private int spellLevel;

    public SpellBookItem(GameObject spellPrefab, int level)
    {
        this.spellPrefab = spellPrefab;
        this.spellLevel = level;
    }

    public override int getSpellLevel()
    {
        return spellLevel;
    }

    public override void incrementSpellLevel()
    {
        spellLevel++;
    }

    public override void setSpellLevel(int spellLevel)
    {
        this.spellLevel = spellLevel;
    }

    public override GameObject generateSpell(PlayerController caster)
    {
        GameObject spellObj;

        if (spellPrefab != null)
        {
            spellObj = (GameObject)Instantiate(spellPrefab, caster.getSpellSpawner().position, caster.getSpellSpawner().rotation);
            Spell spell = spellObj.GetComponent<T>();
            spell.initializeSpell(new SpellInitializer(caster, spellLevel));
            spell.castSpell();
            return spellObj;
        }
        else
        {
            spellObj = null;
        }

        return spellObj;
    }
}
