using System;
using UnityEngine;

namespace Physics.Unity {
	public class UnityUIWorld : MonoBehaviour {
		internal UIWorld World;

		private void Awake() {
			World = new UIWorld();
		}

		private void Update() {
			World.Step(Time.deltaTime);
		}
	}
}