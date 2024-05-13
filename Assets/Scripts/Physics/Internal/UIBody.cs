using UnityEngine;
using UnityEngine.Assertions;

namespace Physics {
	internal sealed class UIBody {
		public Vector2 Position;
		private Vector2 _force;

		public float Mass { private set; get; }
		public float InvMass { private set; get; }
		public float Bounciness { private set; get; }

		public bool IsStatic { private set; get; }
		public bool Enabled { set; get; }

		public float Radius { private set; get; }

		public Vector2 LinearVelocity { get; internal set; }

		private UIBody(Vector2 position, float mass, float bounciness, bool isStatic, float radius, bool enabled) {
			Position = position;
			LinearVelocity = _force = Vector2.zero;

			Mass = mass;
			Bounciness = bounciness;
			Enabled = enabled;

			SetStatic(isStatic);

			Radius = radius;
		}

		internal void SetStatic(bool isStatic) {
			IsStatic = isStatic;

			if (IsStatic) {
				LinearVelocity = Vector2.zero;
				InvMass = 0f;
			} else {
				InvMass = 1f / Mass;
			}
		}

		public void Step(float deltaTime) {
			Vector2 acceleration = _force / Mass;

			LinearVelocity += acceleration * deltaTime;
			Position += LinearVelocity * deltaTime;
		}

		public void AddForce(Vector2 amount) {
			_force = amount;
		}

		public void Move(Vector2 amount) => Position += amount;
		public void MoveTo(Vector2 newPosition) => Position = newPosition;

		public static UIBody CreateCircle(float radius, Vector2 position, float mass, bool isStatic, float bounciness,
			bool enabled)
		{
			bounciness = Mathf.Clamp01(bounciness);

			Assert.AreNotEqual(0, mass, "Mass cannot be 0");

			return new UIBody(position, mass, bounciness, isStatic, radius, enabled);
		}
	}
}