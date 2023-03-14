using UnityEngine;

public class Player : Character {
	public static Player current { get; private set; }
	private Animator anim;
	private float lastAttackTime;

	private void Start() {
		if (current == null) {
			current = this;
			anim = GetComponentInChildren<Animator>();

			if (anim == null) Debug.LogWarning("Component Animator is not found on this object!");
		} else {
			Debug.LogWarning("Attempting to instantiate multiple copies of singleton Player, self-destructing");
			Destroy(this);
		}
	}

	private void Update() {
		if (target != null && !target.isDead) {
			float targetDistance = Vector3.Distance(transform.position, target.transform.position);
			if (targetDistance < attackRange) {
				controller.StopMovement();
				controller.LookTowards(target.transform.position - transform.position);
				if (Time.time - lastAttackTime > attackRate) {
					lastAttackTime = Time.time;
					anim.SetTrigger("isAttacking");
				}
			} else {
				controller.MoveToTarget(target.transform);
			}
		}

		anim.SetBool("isMoving", controller.isMoving);
	}

	protected override void Die() {
		base.Die();
		anim.SetBool("isDead", true);
	}
}
