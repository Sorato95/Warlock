

public class SpeedBoost : Spell
{
    private int buffIntKey;

    public override void castSpell()
    {
        buffIntKey = getCaster().movementManager.addInfluence(2 + getSpellLevel(), "Multiply");
        Destroy(this.gameObject, 2 + (0.5F * getSpellLevel()));
    }

    public void OnDestroy()
    {
        if (getCaster() != null)
        {
            getCaster().movementManager.removeInfluence(buffIntKey);
        }
    }

    public override float getKnockbackForce()
    {
        return 0;
    }

    public override void affectPlayer(PlayerController player)
    {

    }
}
