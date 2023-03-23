using UnityEngine;

public abstract class SelfDestructableGameObject : MonoBehaviour {
	private float selfDestructTimer;
	private bool selfDestructTimerActive;

	protected virtual void OnEnable() {
		selfDestructTimer = 0;
		selfDestructTimerActive = false;
	}

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
