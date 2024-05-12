using System.Collections.Generic;
using UnityEngine;

namespace Physics {
	internal class UIWorld {
		private readonly List<UIBody> _bodies = new();

		public void AddBody(UIBody body) => _bodies.Add(body);
		public void RemoveBody(UIBody body) => _bodies.Remove(body);

		public void Step(float deltaTime) {
			MovementStep(deltaTime);
			CollisionStep(deltaTime);
		}

		private void MovementStep(float deltaTime) {
			foreach (UIBody t in _bodies)
				t.Step(deltaTime);
		}

		private void CollisionStep(float deltaTime) {
			for (int i = 0; i < _bodies.Count - 1; i++) {
				UIBody bodyA = _bodies[i];

				for (int j = i + 1; j < _bodies.Count; j++) {
					UIBody bodyB = _bodies[j];

					if (UICollisions.IntersectCircles(bodyA, bodyB, out Vector2 normal, out float depth)) {
						bodyA.Move(-normal * depth / 2f);
						bodyB.Move(normal * depth / 2f);

						ResolveCollision(bodyA, bodyB, normal, depth);
					}
				}
			}
		}

		private void ResolveCollision(UIBody bodyA, UIBody bodyB, Vector2 normal, float depth) {
			Vector2 relativeVelocity = bodyB.LinearVelocity - bodyA.LinearVelocity;

			float e = Mathf.Min(bodyA.Restitution, bodyB.Restitution);
			float j = -(1 + e) * Vector2.Dot(relativeVelocity, normal);
			j /= 1f / bodyA.Mass + 1f / bodyB.Mass;

			bodyA.LinearVelocity -= j / bodyA.Mass * normal;
			bodyB.LinearVelocity += j / bodyB.Mass * normal;
		}
	}
}