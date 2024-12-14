using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Linq;

[TestFixture]
public class AsteroidTest : SceneTestFixture
{
    [Inject] IEntityManager<EntityType> entityManager;

    [UnityTest]
    public IEnumerator AsteroidSplitTest()
    {
        yield return LoadScene("GameScene");
        entityManager.HideAllEntities();
        Entity asteroidObject = entityManager.GetEntityOfType(EntityType.ASTEROID_BIG);
        Debug.Assert(asteroidObject != null);
        asteroidObject.gameObject.SetActive(true);
       // entityManager.AddEntity(asteroidObject);
        IDamagable damagable = asteroidObject.GetComponent<IDamagable>();
        damagable.TakeDamage();
        yield return new WaitForSeconds(0.5f);
        int amountOfMediumAsteroids = entityManager.CountActiveEntitiesOfType(EntityType.ASTEROID_MEDIUM);
        Debug.Assert(amountOfMediumAsteroids == 3);
    }
}