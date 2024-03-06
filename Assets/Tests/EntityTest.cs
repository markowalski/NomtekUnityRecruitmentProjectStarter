using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using markow;
using System;

public class EntityTest
{
    [Test]
    [TestCase(ENTITY_STATE.Initialized)]
    [TestCase(ENTITY_STATE.Detached)]
    public void Cube_SetState__RigidbodyIsKinematicSetToTrue(ENTITY_STATE _state)
    {
        GameObject go = new GameObject();
        Rigidbody rb = go.AddComponent<Rigidbody>();

        Cube cube = go.AddComponent<Cube>();
        cube.SetState(_state);

        Assert.IsTrue(rb.isKinematic);
    }

    [UnityTest]
    public IEnumerator CubeEater_SetStateToDestroyed_EntityIsProperlyDestroyed()
    {
        GameObject go = new GameObject();
        Rigidbody rb = go.AddComponent<Rigidbody>();
        CubeEater cubeEater = go.AddComponent<CubeEater>();
        cubeEater.SetState(ENTITY_STATE.Destroyed);

        yield return new WaitForSeconds(1f);

        Assert.IsFalse(go);
    }


}
