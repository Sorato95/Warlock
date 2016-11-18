using UnityEngine;
using System.Collections;

public class SpellBookItem {

    private ScriptableObject spellAsset;
    public ScriptableObject SpellAsset
    {
        get
        {
            return spellAsset;
        }
    }

    private int spellLevel;
    public int SpellLevel {
        get
        {
            return spellLevel;
        }
        set
        {
            spellLevel = value;
        }
    }

    public void incrementSpellLevel()
    {
        spellLevel++;
    }

    public SpellBookItem(ScriptableObject spellAsset, int level)
    {
        this.spellAsset = spellAsset;
        this.spellLevel = level;
    }

}
