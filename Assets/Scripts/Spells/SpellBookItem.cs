using UnityEngine;
using System.Collections;

public class SpellBookItem {

    private GameObject spellPrefab;
    private int spellLevel;

    public SpellBookItem(GameObject spellPrefab, int level)
    {
        this.spellPrefab = spellPrefab;
        this.spellLevel = level;
    }

    public GameObject getSpellPrefab()
    {
        return spellPrefab;
    }

    public int getSpellLevel()
    {
        return spellLevel;
    }
}
