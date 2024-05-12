using UnityEngine;

namespace Physics {
	public sealed class UIBody {
		public Vector2 Position;
		private Vector2 _force;

		public readonly float Density;
		public readonly float Mass;
		public readonly float Restitution;
		public readonly float Area;

		public readonly bool IsStatic;

		public readonly float Radius;

		public Vector2 LinearVelocity { get; internal set; }

		private UIBody(Vector2 position, float density, float mass, float restitution, float area, bool isStatic, float radius) {
			Position = position;
			LinearVelocity = _force = Vector2.zero;

			Density = density;
			Mass = mass;
			Restitution = restitution;
			Area = area;

			IsStatic = isStatic;

			Radius = radius;
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
		public void MoveTo(Vector2 position) => Position = position;

		public static UIBody CreateCircle(float radius, Vector2 position, float density, bool isStatic, float restitution) {
			float area = radius * radius * Mathf.PI;
			restitution = Mathf.Clamp01(restitution);
			float mass = density * area;

			return new UIBody(position, density, mass, restitution, area, isStatic, radius);
		}
	}
}