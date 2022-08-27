using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BadHealth : MonoBehaviour
{
    [SerializeField] private float _baseHealth = 50;
    [SerializeField] private CharacterStats _charStats;

    public float BaseHealth { get; set; }

    public UnityEvent onZeroHealth = new UnityEvent();

    public void ReduceHealth(List<DamageType> damages)
    {
        foreach (var dmg in damages)
        {

            //dmg.Apply(this:BadHealth);
        }
        if (_baseHealth < 0.001f)
        {
            if (onZeroHealth == null) Debug.Log("null");
            onZeroHealth.Invoke();
        }
    }
}

[System.Serializable]
public struct CharacterStats
{
    public float armor;
    public float resistant;
    public float endurance;
}







public interface ICharacterInfo
{
    public float BaseHealth { get; set; }
    public CharacterStats Stats { get; set; }
}
public class Health : MonoBehaviour , ICharacterInfo
{
    [SerializeField] private float _baseHealth = 50;
    [SerializeField] private CharacterStats _charStats;
    public float BaseHealth { get { return _baseHealth; } set { _baseHealth = value; } }
    public CharacterStats Stats { get { return _charStats; } set { _charStats= value; } }

    public UnityEvent onZeroHealth = new UnityEvent();
    
    private DamageApplier _damageApplier = new DamageApplier();
    
    public void ReduceHealth(List<DamageType> damages)
    {
        var result = _damageApplier.ApplyDamage(damages, this, gameObject);
        
        BaseHealth = result.BaseHealth;
        Stats = result.Stats;

        if (BaseHealth< 0.001f)
        {
            if (onZeroHealth == null) Debug.Log("null");
            onZeroHealth.Invoke(); 
        }
    }
}
public class DamageApplier
{
    public ICharacterInfo ApplyDamage(List<DamageType> damages, ICharacterInfo charInfo,GameObject target=null)
    {
        foreach (var dmg in damages)
        {
            dmg.Apply(ref charInfo);

            if (target)
                dmg.ApplyEffect(charInfo,target);
            else
                dmg.ApplyEffect(charInfo);
            
        }
        return charInfo;
    }

}
