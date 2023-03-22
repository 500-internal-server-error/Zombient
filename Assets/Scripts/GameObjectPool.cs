using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour {
	public static GameObjectPool instance { get; private set; }

	private Dictionary<string, List<SelfDestructableGameObject>> pooledGameObjects;

	private void Start() {
		if (instance == null) {
			instance = this;

			pooledGameObjects = new Dictionary<string, List<SelfDestructableGameObject>>();
		} else {
			Debug.LogWarning("Attempting to instantiate multiple copies of singleton GameObjectPool, self-destructing");
			Object.Destroy(this);
		}
	}

	public GameObject Instantiate(SelfDestructableGameObject original, Vector3 position, Quaternion rotation) {
		if (!pooledGameObjects.ContainsKey(original.tag)) {
			pooledGameObjects.Add(original.tag, new List<SelfDestructableGameObject>());
		}

		List<SelfDestructableGameObject> container = pooledGameObjects[original.tag];

		foreach (SelfDestructableGameObject clone in container) {
			if (!clone.gameObject.activeInHierarchy) {
				clone.gameObject.SetActive(true);

				clone.transform.position = position;
				clone.transform.rotation = rotation;

				return clone.gameObject;
			}
		}

		SelfDestructableGameObject newClone = Object.Instantiate(original, position, rotation, transform);
		container.Add(newClone);

		return newClone.gameObject
		;
	}

	public void Destroy(SelfDestructableGameObject obj, float t = 0) {
		if (!pooledGameObjects.ContainsKey(obj.tag)) {
			Debug.LogWarning("Attempted to destroy a non-pooled GameObject!");
			return;
		}

		obj.SelfDestruct(t);
	}

	public int CountActive(SelfDestructableGameObject obj) {
		if (!pooledGameObjects.ContainsKey(obj.tag)) return 0;

		int count = 0;

		foreach (SelfDestructableGameObject clone in pooledGameObjects[obj.tag]) {
			if (clone.gameObject.activeInHierarchy) {
				count++;
			}
		}

		return count;
	}
}
