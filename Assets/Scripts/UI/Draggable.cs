using Physics.Unity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
	public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
		[SerializeField] private Canvas canvas;
		[SerializeField] private float force;

		private UnityUIBody _uiBody;

		private void Awake() {
			_uiBody = GetComponent<UnityUIBody>();
		}

		public void OnDrag(PointerEventData eventData) {
			_uiBody.Move(eventData.delta);
		}

		public void OnBeginDrag(PointerEventData eventData) {
			_uiBody.SetEnabledState(false);
		}

		public void OnEndDrag(PointerEventData eventData) {
			_uiBody.SetEnabledState(true);
		}

		public void OnPointerEnter(PointerEventData eventData) {
			_uiBody.SetStaticState(true);
		}

		public void OnPointerExit(PointerEventData eventData) {
			_uiBody.SetStaticState(false);
		}
	}
}