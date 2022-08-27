using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class ReduceHealthTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Reduce_Health_Test_Simple_Passes()
    {
        // arrange
        var dmgaplier = new DamageApplier();
        var charInfo = Substitute.For<ICharacterInfo>();
        charInfo.BaseHealth = 10;
        charInfo.Stats = new CharacterStats { armor = 0, resistant = 1, endurance = 0 };
        var damages = new List<DamageType>();
        damages.Add(new NormalDamage(10));

        //act
        var result = dmgaplier.ApplyDamage(damages, charInfo);

        //assert
        Assert.AreEqual(0, result.BaseHealth);
    }
    [Test]
    public void Reduce_Health_Test_Simple_damage_larger_than_health()
    {
        // arrange
        var dmgaplier = new DamageApplier();
        var charInfo = Substitute.For<ICharacterInfo>();
        charInfo.BaseHealth = 10;
        charInfo.Stats = new CharacterStats { armor = 0, resistant = 1, endurance = 0 };
        var damages = new List<DamageType>();
        damages.Add(new NormalDamage(15));

        //act
        var result = dmgaplier.ApplyDamage(damages, charInfo);

        //assert
        Assert.AreEqual(0, result.BaseHealth);
    }

    [Test]
    public void Reduce_Health_Test_two_damage_types()
    {
        // arrange
        var dmgaplier = new DamageApplier();
        var charInfo = Substitute.For<ICharacterInfo>();
        charInfo.BaseHealth = 20;
        charInfo.Stats = new CharacterStats { armor = 0, resistant = 1, endurance = 0 };
        var damages = new List<DamageType>();
        damages.Add(new NormalDamage(5));
        damages.Add(new FireDamage(5));

        //act
        var result = dmgaplier.ApplyDamage(damages, charInfo);

        //assert
        Assert.AreEqual(10, result.BaseHealth);
    }
    [Test]
    public void Add_Health_heal_damage_type()
    {
        // arrange
        var dmgaplier = new DamageApplier();
        var charInfo = Substitute.For<ICharacterInfo>();
        charInfo.BaseHealth = 5;
        charInfo.Stats = new CharacterStats { armor = 0, resistant = 0, endurance = 0 };
        var damages = new List<DamageType>();
        damages.Add(new HealDamage(5));

        //act
        var result = dmgaplier.ApplyDamage(damages, charInfo);

        //assert
        Assert.AreEqual(10, result.BaseHealth);
    }

    [Test]
    public void Reduce_Health_Does_Effect_Apply()
    {
        var dmgaplier = new DamageApplier();
        var charInfo = Substitute.For<ICharacterInfo>();
        charInfo.BaseHealth = 10;
        charInfo.Stats = new CharacterStats { armor = 0, resistant = 1, endurance = 0 };
        var damages = new List<DamageType>();
        damages.Add(new FireDamage(5));
        var obj = new GameObject();
        var result = dmgaplier.ApplyDamage(damages, charInfo, obj);

        Assert.IsTrue(obj.TryGetComponent<FireEffect>(out var effect));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ReduceHealthTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
