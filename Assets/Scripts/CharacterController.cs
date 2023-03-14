using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour {
	[SerializeField]
	private NavMeshAgent agent;

	[HideInInspector]
	public bool isMoving = false;

	private float moveToUpdateRate = 0.1f;
	private float lastMoveToUpdateTime;
	private Transform moveTarget;

	public void MoveToTarget(Transform target) {
		moveTarget = target;
	}

	public void LookTowards(Vector3 direction) {
		transform.rotation = Quaternion.LookRotation(direction);
	}

	public void MoveToPosition(Vector3 position) {
		agent.isStopped = false;
		agent.SetDestination(position);
	}

	public void StopMovement() {
		agent.isStopped = true;
		moveTarget = null;
	}

	private void Update() {
		if (moveTarget != null && Time.time - lastMoveToUpdateTime > moveToUpdateRate) {
			lastMoveToUpdateTime = Time.time;
			MoveToPosition(moveTarget.position);
		}

		isMoving = agent.velocity.magnitude > 0.1f;
	}
}
