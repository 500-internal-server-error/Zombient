using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour {
	[SerializeField]
	private GameObject nav;

	[SerializeField]
	private LayerMask layerMask;

	private void Update() {
		if (Player.current == null) {
			Debug.LogWarning("Singleton Player is not initialized yet, returning");
			return;
		}

		nav.SetActive(Player.current.controller.isMoving && !Player.current.isDead);
		if (Player.current.isDead) return;
		if (Mouse.current.rightButton.wasPressedThisFrame) Click();
	}

	private void Click() {
		Vector2 mousePos = Mouse.current.position.ReadValue();
		Ray ray = Camera.main.ScreenPointToRay(mousePos);

		if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerMask)) {
			int hitLayer = hit.collider.gameObject.layer;

			if (hitLayer == LayerMask.NameToLayer("Ground")) {
				Player.current.SetTarget(null);
				Player.current.controller.StopMovement();
				Player.current.controller.MoveToPosition(hit.point);
			} else if (hitLayer == LayerMask.NameToLayer("Enemy")) {
				Character enemy = hit.collider.GetComponent<Character>();
				Player.current.SetTarget(enemy);
			}

			nav.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
		}
	}
}
