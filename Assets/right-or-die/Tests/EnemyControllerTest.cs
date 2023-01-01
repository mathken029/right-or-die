using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyControllerTest
{
    [Test]
    public void DamageTest()
    {
        float _enemyHitpoint = 1;
        EnemyController _enemyController = new EnemyController();

        float _enemyHitpointDamaged = _enemyController.Damage(_enemyHitpoint);
        Assert.That(_enemyHitpointDamaged == _enemyHitpoint - 1);
    }

}
