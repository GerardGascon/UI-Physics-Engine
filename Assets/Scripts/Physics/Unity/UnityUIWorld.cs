using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Physics.Unity {
	public class UnityUIWorld : MonoBehaviour {
		[SerializeField] private Canvas canvas;

		internal UIWorld World;

		private void Awake() {
			Assert.IsNotNull(canvas, $"Canvas not assigned in {name}");
			World = new UIWorld(canvas.pixelRect);
		}

		private void Update() {
			World.Step(Time.deltaTime);
		}
	}
}