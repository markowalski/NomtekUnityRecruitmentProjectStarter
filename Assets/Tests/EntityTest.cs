using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using markow;
using System;

/*
 * Class containing two unit tests. The first one checks whether objects of type Cube 
 * maintain the Rigidbody.isKinematic property after being dropped into the scene. The second one verifies 
 * whether the CubeEater object is indeed destroyed after transitioning to the Destroyed state.
 */
public class EntityTest
{
    [Test]
    // Used TestCase to avoid code duplication and to check different cases in one go
    [TestCase(ENTITY_STATE.Initialized)]
    [TestCase(ENTITY_STATE.Detached)]
    public void Cube_SetState__RigidbodyIsKinematicSetToTrue(ENTITY_STATE _state)
    {
        GameObject go = new GameObject();
        Rigidbody rb = go.AddComponent<Rigidbody>();
        Cube cube = go.AddComponent<Cube>();
        cube.SetState(_state);

        // Check whether the object in a specific state has its Rigidbody property set to TRUE
        Assert.IsTrue(rb.isKinematic);
    }

    // Use the UnityTest attribute because we need to be able to use Coroutine.
    [UnityTest]
    public IEnumerator CubeEater_SetStateToDestroyed_EntityIsProperlyDestroyed()
    {
        GameObject go = new GameObject();
        Rigidbody rb = go.AddComponent<Rigidbody>();
        CubeEater cubeEater = go.AddComponent<CubeEater>();
        cubeEater.SetState(ENTITY_STATE.Destroyed);

        // Make sure that the object is indeed given time to be destroyed
        yield return new WaitForSeconds(1f);

        Assert.IsFalse(go);
    }
}