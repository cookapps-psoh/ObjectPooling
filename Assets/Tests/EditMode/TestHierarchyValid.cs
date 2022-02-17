using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestHierarchyValid
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestMainScene()
    {
        var camera = GameObject.Find("Main Camera");
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        var spawner = GameObject.Find("Spawner").GetComponent<Spawner>();

        yield return null;

        Assert.AreEqual(false, camera == null);
        Assert.AreEqual(false, gameManager == null);
        Assert.AreEqual(false, spawner == null);
    }
}