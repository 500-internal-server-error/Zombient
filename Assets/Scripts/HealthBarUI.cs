using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
	[SerializeField]
	private Image healthFill;

	[SerializeField]
	private Color playerHealthColor;

	[SerializeField]
	private Color enemyHealthColor;

	private Character character;

	private void OnEnable() {
		character = GetComponentInParent<Character>();

		character.onSpawn += ShowHealthBar;
		character.onTakeDamage += UpdateHealthBar;
		character.onDie += HideHealthBar;

		healthFill.color = transform.parent.tag switch {
			"Player" => playerHealthColor,
			"Enemy" => enemyHealthColor,

			_ => Color.black
		};

		UpdateHealthBar();
	}

	private void Update() {
		transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
	}

	private void ShowHealthBar() {
		gameObject.SetActive(true);
	}

	private void UpdateHealthBar() {
		healthFill.fillAmount = (float) character.currentHP / (float) character.maxHp;
	}

	private void HideHealthBar() {
		gameObject.SetActive(false);
	}
}
