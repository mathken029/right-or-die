using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyControllerTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void EnemyControllerTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EnemyControllerTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [Test]
    public void DamageTest()
    {
        float _enemyHitpoint = 1;
        float _enemyHitpointBefore = _enemyHitpoint;
        EnemyController _enemyController = new EnemyController();

        _enemyController.Damage(_enemyHitpoint);
        Assert.That(_enemyHitpoint == _enemyHitpointBefore - 1);
    }

}
