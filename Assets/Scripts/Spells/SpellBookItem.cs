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

    public SpellBookItem(GameObject spellPrefab, int level)
    {
        this.spellPrefab = spellPrefab;
        this.spellLevel = level;
    }

}
