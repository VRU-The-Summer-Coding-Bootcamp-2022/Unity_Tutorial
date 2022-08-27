using UnityEngine;

[System.Serializable]
public abstract class DamageType
{
    protected float baseValue;
    protected DamageType(float baseValue)
    {
        this.baseValue = baseValue;
    }
    public virtual void Apply(ref ICharacterInfo charInfo) 
    {
        charInfo.BaseHealth -= (baseValue / charInfo.Stats.resistant) - charInfo.Stats.armor;
        charInfo.BaseHealth =Mathf.Max(0, charInfo.BaseHealth);
    }
    public virtual void ApplyEffect(ICharacterInfo charStats) => Debug.Log("Effect Applied");
    public virtual void ApplyEffect(ICharacterInfo charStats, GameObject target) => Debug.Log($"{this} Applied on {target.name}");
}


public class NormalDamage : DamageType
{
    public NormalDamage(float baseValue) : base(baseValue)
    {
    }
}
public class FireDamage : DamageType
{
    public FireDamage(float baseValue) : base(baseValue)
    {
    }

    public override void Apply(ref ICharacterInfo charInfo)
    {
        base.Apply(ref charInfo);
    }
    public override void ApplyEffect(ICharacterInfo charStats, GameObject target)
    {
        Debug.Log("fire damage");

        target.AddComponent<FireEffect>().Init(3);
    }
}
public class HealDamage : DamageType
{
    public HealDamage(float baseValue) : base(baseValue)
    {
    }

    public override void Apply(ref ICharacterInfo charInfo)
    {
    }

    public override void ApplyEffect(ICharacterInfo charStats)
    {
        Debug.Log("heal damage?");
        //charStats.baseHealth += baseValue;
    }
}



// More on applying effects on objects
public abstract class ModifierEffect : MonoBehaviour
{
    protected float _duration;
    public void Init(float duration)
    {
        _duration = duration;
    }

    abstract protected void Start();
    public abstract void Apply();
}
public class FireEffect : ModifierEffect
{
    protected override void Start() => Apply();
    public override void Apply()
    {
        // Do some firey stuff
    }

}