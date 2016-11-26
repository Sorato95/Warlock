using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpellBookItem {

    private ScriptableSpell spellAsset;
    public ScriptableSpell SpellAsset
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

    public SpellBookItem(ScriptableSpell spellAsset, int level)
    {
        this.spellLevel = level;

        //each player holds their own copy of the spellAsset initialized for them as caster
        //the cloned spellAsset will be initialized for the caster by SpellBook's AddAndInitialize-method
        this.spellAsset = ScriptableSpell.Instantiate(spellAsset);                      
    }

    public GameObject generateSpell()
    {
        return spellAsset.Generate(spellLevel);
    }

}
