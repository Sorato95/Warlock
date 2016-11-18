using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Spells/UtilitySpell", order = 1)]
public class ScriptableSpell : ScriptableObject {
    public string spellScriptName = "Script Name";          //assigned by Inspector
        
    protected PlayerController caster;
    protected System.Type spellScript;

    protected void Initialize(NetworkInstanceId networkId)
    {
        this.caster = ClientScene.FindLocalObject(networkId).GetComponent<PlayerController>();
        this.spellScript = System.Reflection.Assembly.GetExecutingAssembly().GetType(spellScriptName);
    }

    public virtual GameObject Generate(NetworkInstanceId networkId) {
        this.Initialize(networkId);

        GameObject spellObject = new GameObject(); 
        SpellScript spell = (SpellScript) spellObject.AddComponent(spellScript);
        spellObject.name = spellScript.Name;

        spell.Initialize(caster);
        spell.castSpell();
        return spellObject;
    }
}