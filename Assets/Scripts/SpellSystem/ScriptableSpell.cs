using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Spells/UtilitySpell", order = 1)]
public class ScriptableSpell : ScriptableObject
{
    public string spellScriptName = "Script Name";          //assigned by Inspector
    protected System.Type spellScript;
    protected PlayerController caster;

    //called once per player (each player possesses their own copy of the spell asset in their spellbook)
    public void Initialize(NetworkInstanceId casterNetworkId)
    {
        this.spellScript = System.Reflection.Assembly.GetExecutingAssembly().GetType(spellScriptName);
        this.caster = ClientScene.FindLocalObject(casterNetworkId).GetComponent<PlayerController>();
    }

    //called each time a spell is being cast
    public virtual GameObject Generate(int spellLevel)
    {
        GameObject spellObject = new GameObject();
        Spell spell = (Spell)spellObject.AddComponent(spellScript);
        spellObject.name = spellScript.Name;

        spell.Initialize(caster, spellLevel);
        spell.castSpell();
        return spellObject;
    }
}