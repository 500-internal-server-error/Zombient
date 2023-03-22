using UnityEngine;

public abstract class SelfDestructableGameObject : MonoBehaviour {
	private float selfDestructTimer = 0;
	private bool selfDestructTimerActive = false;

	protected virtual void Update() {
		if (selfDestructTimerActive) {
			selfDestructTimer -= Time.deltaTime;
			if (selfDestructTimer <= 0) OnSelfDestruct();
		}
	}

	protected abstract void OnSelfDestruct();

	public void SelfDestruct(float delay = 0) {
		selfDestructTimerActive = true;
		selfDestructTimer = delay;
	}
}
