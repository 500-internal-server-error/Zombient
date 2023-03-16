using UnityEngine;

public class CharacterHelper : MonoBehaviour {
	private Character character;

	private void Start() {
		character = GetComponentInParent<Character>();
	}

	private void OnAttack() {
		character.AttackTarget();
	}
}
