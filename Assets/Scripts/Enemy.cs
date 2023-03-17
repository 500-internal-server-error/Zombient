using UnityEngine;

public class Enemy : Character {
	private enum State {
		Idle,
		Chase,
		Attack
	}

	private State currentState = State.Idle;

	private Animator anim;

	private float lastAttackTime;
	private float targetDistance;

	[SerializeField]
	private float chaseRange;

	protected override void Start() {
		base.Start();
		anim = GetComponentInChildren<Animator>();
	}

	private void Update() {
		SetTarget(isDead ? null : Player.current);
		if (target == null || target.isDead) {
			SetState(State.Idle);
			anim.SetBool("isMoving", false);
			return;
		}

		targetDistance = Vector3.Distance(transform.position, target.transform.position);

		switch (currentState) {
			case State.Idle: IdleUpdate(); break;
			case State.Chase: ChaseUpdate(); break;
			case State.Attack: AttackUpdate(); break;
		}

		anim.SetBool("isMoving", currentState == State.Chase);
	}

	protected override void Die() {
		base.Die();
		controller.StopMovement();
		anim.SetBool("isDead", true);
	}

	private void IdleUpdate() {
		if (targetDistance < chaseRange && targetDistance > attackRange) {
			SetState(State.Chase);
		} else if (targetDistance < attackRange) {
			SetState(State.Attack);
		}
	}

	private void ChaseUpdate() {
		if (targetDistance > chaseRange) {
			SetState(State.Idle);
		} else if (targetDistance < attackRange) {
			SetState(State.Attack);
		}
	}

	private void AttackUpdate() {
		if (targetDistance > attackRange) SetState(State.Chase);

		controller.LookTowards(target.transform.position - transform.position);

		if (Time.time - lastAttackTime > attackRate) {
			lastAttackTime = Time.time;
			anim.SetTrigger("isAttacking");
		}
	}

	private void SetState(State newState) {
		currentState = newState;

		switch (currentState) {
			case State.Idle: controller.StopMovement(); break;
			case State.Chase: controller.MoveToTarget(target.transform); break;
			case State.Attack: controller.StopMovement(); break;
		}
	}
}
