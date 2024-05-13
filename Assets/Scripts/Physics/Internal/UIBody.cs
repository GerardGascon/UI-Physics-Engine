using UnityEngine;

namespace Physics {
	internal sealed class UIBody {
		public Vector2 Position;
		private Vector2 _force;

		public float Mass { private set; get; }
		public float InvMass { private set; get; }
		public float Bounciness { private set; get; }

		public bool IsStatic { private set; get; }

		public float Radius { private set; get; }

		public Vector2 LinearVelocity { get; internal set; }

		private UIBody(Vector2 position, float mass, float bounciness, bool isStatic, float radius) {
			Position = position;
			LinearVelocity = _force = Vector2.zero;

			Mass = mass;
			Bounciness = bounciness;

			SetStatic(isStatic);

			Radius = radius;
		}

		internal void SetStatic(bool isStatic) {
			IsStatic = isStatic;

			if (!IsStatic) {
				InvMass = 1f / Mass;
			} else {
				InvMass = 0f;
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
		public void MoveTo(Vector2 newPosition) {
			LinearVelocity = newPosition - Position;
			Position = newPosition;
		}

		public static UIBody CreateCircle(float radius, Vector2 position, float mass, bool isStatic, float bounciness) {
			bounciness = Mathf.Clamp01(bounciness);

			return new UIBody(position, mass, bounciness, isStatic, radius);
		}
	}
}