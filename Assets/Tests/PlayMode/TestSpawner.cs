using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestSpawner
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestSpawnerSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestSpawnerWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator TestBasicSpawnerBehaviour()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        yield return null;

        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        var spawner = GameObject.Find("Spawner").GetComponent<Spawner>();

        var maxPoolSize = spawner.MaxPoolSize;
        var objectCount = 0;
        
        for (var i = 0; i < maxPoolSize; ++i)
        {
            gameManager.OnClickSpawnButton();
            ++objectCount;

            yield return null;

            var childCount = spawner.gameObject.transform.childCount;
            var activeCount = 0;
            for (var j = 0; j < childCount; ++j)
            {
                var childObject = spawner.gameObject.transform.GetChild(j);
                if (childObject.gameObject.activeSelf) ++activeCount;
            }
            var inactiveCount = childCount - activeCount;

            Assert.AreEqual(true, objectCount == activeCount);
            Assert.AreEqual(true, 0 == inactiveCount);
            Assert.AreEqual(true, objectCount == childCount);
            
            Assert.AreEqual(true, spawner.CountActive == activeCount);
            Assert.AreEqual(true, spawner.CountInactive == inactiveCount);
            Assert.AreEqual(true, spawner.CountAll == childCount);
        }

        for (var i = 0; i < maxPoolSize; ++i)
        {
            gameManager.OnClickSpawnButton();

            yield return null;

            var childCount = spawner.gameObject.transform.childCount;
            var activeCount = 0;
            for (var j = 0; j < childCount; ++j)
            {
                var childObject = spawner.gameObject.transform.GetChild(j);
                if (childObject.gameObject.activeSelf) ++activeCount;
            }
            var inactiveCount = childCount - activeCount;

            Assert.AreEqual(true, objectCount == activeCount);
            Assert.AreEqual(true, 0 == inactiveCount);
            Assert.AreEqual(true, maxPoolSize == childCount);
            
            Assert.AreEqual(true, spawner.CountActive == activeCount);
            Assert.AreEqual(true, spawner.CountInactive == inactiveCount);
            Assert.AreEqual(true, spawner.CountAll == childCount);
        }

        for (var i = 0; i < maxPoolSize; ++i)
        {
            gameManager.OnClickDespawnButton();
            --objectCount;

            yield return null;
            
            var childCount = spawner.gameObject.transform.childCount;
            var activeCount = 0;
            for (var j = 0; j < childCount; ++j)
            {
                var childObject = spawner.gameObject.transform.GetChild(j);
                if (childObject.gameObject.activeSelf) ++activeCount;
            }
            var inactiveCount = childCount - activeCount;

            Assert.AreEqual(true, objectCount == activeCount);
            Assert.AreEqual(true, maxPoolSize - objectCount == inactiveCount);
            Assert.AreEqual(true, maxPoolSize == childCount);
            
            Assert.AreEqual(true, spawner.CountActive == activeCount);
            Assert.AreEqual(true, spawner.CountInactive == inactiveCount);
            Assert.AreEqual(true, spawner.CountAll == childCount);
        }
        
        for (var i = 0; i < maxPoolSize; ++i)
        {
            gameManager.OnClickDespawnButton();

            yield return null;
            
            var childCount = spawner.gameObject.transform.childCount;
            var activeCount = 0;
            for (var j = 0; j < childCount; ++j)
            {
                var childObject = spawner.gameObject.transform.GetChild(j);
                if (childObject.gameObject.activeSelf) ++activeCount;
            }
            var inactiveCount = childCount - activeCount;

            Assert.AreEqual(true, 0 == activeCount);
            Assert.AreEqual(true, maxPoolSize == inactiveCount);
            Assert.AreEqual(true, maxPoolSize == childCount);
            
            Assert.AreEqual(true, spawner.CountActive == activeCount);
            Assert.AreEqual(true, spawner.CountInactive == inactiveCount);
            Assert.AreEqual(true, spawner.CountAll == childCount);
        }
    }

    [UnityTest]
    public IEnumerator TestPooledObjectIsInCamera()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        yield return null;

        var spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        var maxPoolSize = spawner.MaxPoolSize;
        
        // TODO
    }
}
