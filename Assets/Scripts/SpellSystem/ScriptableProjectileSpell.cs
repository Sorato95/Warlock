using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Spells/ProjectileSpell", order = 2)]
public class ScriptableProjectileSpell : ScriptableSpell {
	public GameObject prefab;                   //assigned by Inspector
    public float knockbackForce = 100F;         //assigned by Inspector
	public float spellSpeed = 6F;               //assigned by Inspector
    public float timeToLive = 5F;               //assigned by Inspector

	public override GameObject Generate(int spellLevel) {
		GameObject spellObject = (GameObject) Instantiate(prefab, caster.spellSpawner.position, caster.spellSpawner.rotation);
        ProjectileSpell projectileSpell = (ProjectileSpell) spellObject.AddComponent(spellScript);
        spellObject.name = spellScript.Name;

        projectileSpell.Initialize(caster, spellLevel, knockbackForce, spellSpeed, timeToLive);
        projectileSpell.castSpell();
        return projectileSpell.gameObject;
    }
}
