using Physics.Unity;
using UnityEngine;

namespace UI {
	public class Draggable : MonoBehaviour {
		[SerializeField] private Canvas canvas;
		[SerializeField] private float force;

		private UnityUIBody _uiBody;

		private void Awake() {
			_uiBody = GetComponent<UnityUIBody>();
		}

		private void Update() {
			Vector2 dir = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

			_uiBody.AddForce(dir * force);
		}
	}
}