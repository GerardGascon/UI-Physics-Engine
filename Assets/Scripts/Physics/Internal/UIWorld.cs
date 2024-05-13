using System.Collections.Generic;
using UnityEngine;

namespace Physics {
	internal class UIWorld {
		private readonly List<UIBody> _bodies = new();

		private readonly Rect _screenRect;

		public UIWorld(Rect screenRect) {
			_screenRect = screenRect;
		}

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
			CheckCollisionsBetweenBodies();
			CheckCollisionsAgainstBorders();
		}

		private void CheckCollisionsAgainstBorders() {
			foreach (UIBody body in _bodies) {
				if (body.Position.x - body.Radius < _screenRect.x) {
					body.MoveTo(new Vector2(_screenRect.x + body.Radius, body.Position.y));
					body.LinearVelocity = new Vector2(-body.LinearVelocity.x, body.LinearVelocity.y);
				}else if (body.Position.x + body.Radius > _screenRect.width) {
					body.MoveTo(new Vector2(_screenRect.width - body.Radius, body.Position.y));
					body.LinearVelocity = new Vector2(-body.LinearVelocity.x, body.LinearVelocity.y);
				}

				if (body.Position.y + body.Radius > _screenRect.y) {
					body.MoveTo(new Vector2(body.Position.x, _screenRect.y - body.Radius));
					body.LinearVelocity = new Vector2(body.LinearVelocity.x, -body.LinearVelocity.y);
				}else if (body.Position.y - body.Radius < -_screenRect.height) {
					body.MoveTo(new Vector2(body.Position.x, -_screenRect.height + body.Radius));
					body.LinearVelocity = new Vector2(body.LinearVelocity.x, -body.LinearVelocity.y);
				}
			}
		}

		private void CheckCollisionsBetweenBodies() {
			for (int i = 0; i < _bodies.Count - 1; i++) {
				UIBody bodyA = _bodies[i];

				for (int j = i + 1; j < _bodies.Count; j++) {
					UIBody bodyB = _bodies[j];

					if (bodyA.IsStatic && bodyB.IsStatic)
						continue;
					if (!bodyA.Enabled || !bodyB.Enabled)
						continue;

					if (UICollisions.IntersectCircles(bodyA, bodyB, out Vector2 normal, out float depth)) {
						if (bodyA.IsStatic) {
							bodyB.Move(normal * depth);
						} else if (bodyB.IsStatic) {
							bodyA.Move(-normal * depth);
						} else {
							bodyA.Move(-normal * depth / 2f);
							bodyB.Move(normal * depth / 2f);
						}

						ResolveCollision(bodyA, bodyB, normal, depth);
					}
				}
			}
		}

		private static void ResolveCollision(UIBody bodyA, UIBody bodyB, Vector2 normal, float depth) {
			Vector2 relativeVelocity = bodyB.LinearVelocity - bodyA.LinearVelocity;

			if (Vector2.Dot(relativeVelocity, normal) > 0f)
				return;

			if (relativeVelocity == Vector2.zero)
				relativeVelocity = normal * depth;

			float e = Mathf.Min(bodyA.Bounciness, bodyB.Bounciness);
			float j = -(1 + e) * Vector2.Dot(relativeVelocity, normal);
			j /= bodyA.InvMass + bodyB.InvMass;

			Vector2 impulse = j * normal;

			bodyA.LinearVelocity -= impulse * bodyA.InvMass;
			bodyB.LinearVelocity += impulse * bodyB.InvMass;
		}
	}
}