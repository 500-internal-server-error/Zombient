using UnityEngine;
using UnityEngine.Events;

public class Character : SelfDestructableGameObject {

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

	private HealthBarUI healthbar;

	protected Character target;

	[HideInInspector]
	public bool isDead;

	public event UnityAction onSpawn;
	public event UnityAction onTakeDamage;
	public event UnityAction onDie;

	protected override void OnEnable() {
		base.OnEnable();

		currentHP = maxHp;
		isDead = false;

		if (healthbar == null) {
			healthbar = Instantiate(healthBarPrefab, transform.position + healthBarPrefab.transform.position, Quaternion.identity, transform);
		}

		onSpawn?.Invoke();
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
		controller.StopMovement();
		GameObjectPool.instance.Destroy(this, 3.0f);
	}

	protected override void OnSelfDestruct() {
		gameObject.SetActive(false);
	}
}
