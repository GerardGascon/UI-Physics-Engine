using UnityEngine;

namespace Physics {
	internal static class UICollisions {
		public static bool IntersectCircles(UIBody bodyA, UIBody bodyB, out Vector2 normal, out float depth) {
			normal = Vector2.zero;
			depth = 0;

			float distance = Vector2.Distance(bodyA.Position, bodyB.Position);
			float radii = bodyA.Radius + bodyB.Radius;

			if (distance >= radii)
				return false;

			normal = (bodyB.Position - bodyA.Position).normalized;
			depth = radii - distance;

			return true;
		}
	}
}