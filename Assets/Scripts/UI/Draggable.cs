using System;
using Physics.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
	public class Draggable : MonoBehaviour, IDragHandler {
		[SerializeField] private Canvas canvas;

		private UnityUIBody _uiBody;

		private void Awake() {
			_uiBody = GetComponent<UnityUIBody>();
		}

		public void OnDrag(PointerEventData eventData) {
			_uiBody.SetPosition(_uiBody.GetPosition() + eventData.delta / canvas.scaleFactor);
		}
	}
}