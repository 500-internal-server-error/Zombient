using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour {

	[Header("Stats")]

	public int maxHp;

	[HideInInspector]
	public int currentHP;

	[SerializeField]
	private int damage;

	[SerializeField]
	protected float attackRange;

	[SerializeField]
	protected float attackRate;

	[Header("Components")]

	public CharacterController controller;

	[SerializeField]
	private HealthBarUI healthBarPrefab;

	protected Character target;

	[HideInInspector]
	public bool isDead;

	public event UnityAction onTakeDamage;
	public event UnityAction onDie;

	protected virtual void Start() {
		currentHP = maxHp;
		Instantiate(healthBarPrefab, transform.position + healthBarPrefab.transform.position, Quaternion.identity, transform);
	}

	public virtual void SetTarget(Character t) {
		target = t;
	}

	protected virtual void TakeDamage(int value) {
		currentHP -= value;
		onTakeDamage?.Invoke();
		if (currentHP <= 0) Die();
	}

	public virtual void AttackTarget() {
		target?.TakeDamage(damage);
	}

	protected virtual void Die() {
		isDead = true;
		target = null;
		onDie?.Invoke();
		Destroy(gameObject, 3.0f);
	}
}
