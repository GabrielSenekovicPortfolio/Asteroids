using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

[TestFixture]
public class EnemyShipTest : SceneTestFixture
{
    [Inject]IEntityManager<EntityType> entityManager;
    [UnityTest]
    public IEnumerator EnemyShipThrustTest()
    {
        yield return LoadScene("GameScene");
        GameObject shipObject = entityManager.GetEntityOfType(EntityType.ENEMYSHIP_BIG).gameObject;
        Debug.Assert(shipObject != null);
        shipObject.SetActive(true);
        Vector2 savedPosition = shipObject.transform.position;
        Rigidbody2D body = shipObject.GetComponent<Rigidbody2D>();
        IMovement movement = shipObject.GetComponent<IMovement>();
        movement.SetThrustOnOff(true);
        yield return new WaitForSeconds(0.5f);
        Vector2 newPosition = shipObject.transform.position;
        Debug.Assert(newPosition != savedPosition);
    }
    [UnityTest]
    public IEnumerator EnemyShipTurnTest()
    {
        yield return LoadScene("GameScene");
        GameObject shipObject = entityManager.GetEntityOfType(EntityType.ENEMYSHIP_BIG).gameObject;
        Debug.Assert(shipObject != null);
        shipObject.SetActive(true);
        IMovement movement = shipObject.GetComponent<IMovement>();
        movement.SetTurnDirection(-1);
        Quaternion savedRotation = shipObject.transform.rotation;
        yield return new WaitForSeconds(0.5f);
        Quaternion newRotation = shipObject.transform.rotation;
        Debug.Assert(newRotation != savedRotation);
    }
    [UnityTest]
    public IEnumerator HurtEnemyShipTest()
    {
        yield return LoadScene("GameScene");
        GameObject shipObject = entityManager.GetEntityOfType(EntityType.ENEMYSHIP_BIG).gameObject;
        Debug.Assert(shipObject != null);
        shipObject.SetActive(true);
        IDamagable damagable = shipObject.GetComponent<IDamagable>();
        IHealth playerHealth = shipObject.GetComponent<IHealth>();
        int savedHP = playerHealth.GetCurrentHP();
        damagable.TakeDamage();
        int newHP = playerHealth.GetCurrentHP();
        Debug.Assert(newHP < savedHP);
        Debug.Assert(!shipObject.activeSelf);
    }
}
