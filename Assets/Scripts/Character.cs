using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour {
	[Header("Stats")]

	private int currentHP;
	private int maxHp;
	private int damage;

	[SerializeField]
	protected float attackRange;

	[SerializeField]
	protected float attackRate;

	[Header("Components")]

	public CharacterController controller;
	private GameObject healthBarPrefab;
	protected Character target;

	[HideInInspector]
	public bool isDead;

	private event UnityAction onTakeDamage;
	private event UnityAction onDie;

	public virtual void SetTarget(Character t) {
		target = t;
	}

	protected virtual void TakeDamage(int value) {
		currentHP -= value;
		onTakeDamage?.Invoke();
		if (currentHP <= 0) Die();
	}

	protected virtual void AttackTarget() {
		target?.TakeDamage(damage);
	}

	protected virtual void Die() {
		isDead = true;
		onDie?.Invoke();
		Destroy(gameObject, 3.0f);
	}
}
