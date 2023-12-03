using Zenject;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Linq;

[TestFixture]
public class PlayerTest : SceneTestFixture
{
    [Inject] IEntityManager entityManager;
    [Inject] IPlayerFetcher playerFetcher;
    [Inject] ISceneManager sceneManager;

    [UnityTest]
    public IEnumerator PlayerThrustTest()
    {
        yield return LoadScene("GameScene");
        GameObject playerObject = playerFetcher.FetchPlayer().gameObject;
        Vector2 savedPosition = playerObject.transform.position;
        Rigidbody2D body = playerObject.GetComponent<Rigidbody2D>();
        IMovement movement = playerObject.GetComponent<IMovement>();
        movement.SetThrustOnOff(true);
        yield return new WaitForSeconds(0.5f);
        Vector2 newPosition = playerObject.transform.position;
        Debug.Assert(newPosition != savedPosition);
    }
    [UnityTest]
    public IEnumerator PlayerTurnTest()
    {
        yield return LoadScene("GameScene");
        GameObject playerObject = playerFetcher.FetchPlayer().gameObject;
        IMovement movement = playerObject.GetComponent<IMovement>();
        movement.SetTurnDirection(-1);
        Quaternion savedRotation = playerObject.transform.rotation;
        yield return new WaitForSeconds(0.5f);
        Quaternion newRotation = playerObject.transform.rotation;
        Debug.Assert(newRotation != savedRotation);
    }
    [UnityTest]
    public IEnumerator PlayerHyperdriveTest()
    {
        yield return LoadScene("GameScene");
        GameObject playerObject = playerFetcher.FetchPlayer().gameObject;
        ITeleport movement = playerObject.GetComponent<ITeleport>();
        Vector2 position_1 = playerObject.transform.position;
        movement.SetRandomPosition();
        Vector2 position_2 = playerObject.transform.position;
        movement.SetRandomPosition();
        Vector2 position_3 = playerObject.transform.position;
        Debug.Assert(position_1 != position_2 && position_2 != position_3);
    }
    [UnityTest]
    public IEnumerator HurtPlayerTest()
    {
        yield return LoadScene("GameScene");
        GameObject playerObject = playerFetcher.FetchPlayer().gameObject;
        IDamagable playerDamagable = playerObject.GetComponent<IDamagable>();
        IPlayerHealth playerHealth = playerObject.GetComponent<IPlayerHealth>();
        int savedHP = playerHealth.GetCurrentHP();
        playerDamagable.TakeDamage();
        int newHP = playerHealth.GetCurrentHP();
        Debug.Assert(newHP < savedHP);
    }
    [UnityTest]
    public IEnumerator HurtPlayerWithEntityTest()
    {
        yield return LoadScene("GameScene");
        GameObject playerObject = playerFetcher.FetchPlayer().gameObject;
        IPlayerHealth playerHealth = playerObject.GetComponent<IPlayerHealth>();
        GameObject asteroidObject = entityManager.GetEntityOfType(EntityType.ASTEROID_BIG).gameObject;
        Debug.Assert(asteroidObject != null);
        asteroidObject.SetActive(true);
        int savedHP = playerHealth.GetCurrentHP();
        playerObject.transform.position = asteroidObject.transform.position;
        yield return new WaitForSeconds(0.5f);
        int newHP = playerHealth.GetCurrentHP();
        Debug.Assert(newHP < savedHP);
    }
}