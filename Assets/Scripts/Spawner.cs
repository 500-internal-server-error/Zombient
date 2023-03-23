using UnityEngine;

public class Spawner : MonoBehaviour {
	[SerializeField]
	private SelfDestructableGameObject spawnedObjectPrefab;

	[SerializeField, Min(0)]
	private int maxSpawnedObjects;

	[SerializeField, Min(0)]
	private float spawnDelay;

	private float spawnTimer;

	private void Start() {
		spawnTimer = spawnDelay;
	}

	private void Update() {
		spawnTimer -= Time.deltaTime;

		if (spawnTimer <= 0) {
			if (GameObjectPool.instance.CountActive(spawnedObjectPrefab) < maxSpawnedObjects) {
				GameObjectPool.instance.Instantiate(
					spawnedObjectPrefab,
					new Vector3(
						Player.current.transform.position.x + Random.Range(-2.0f, 2.0f),
						Player.current.transform.position.y,
						Player.current.transform.position.z + Random.Range(-2.0f, 2.0f)
					),
					Quaternion.identity
				);
			}

			spawnTimer = spawnDelay;
		}
	}
}
