﻿using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Physics.Unity {
	public class UnityUIBody : MonoBehaviour {
		[SerializeField] private RectTransform rectTransform;
		[SerializeField] private Canvas canvas;

		[Space]
		[SerializeField] private float radius;
		[SerializeField] private float density;
		[SerializeField] private bool isStatic;
		[SerializeField, Range(0f, 1f)] private float restitution;

		private UIBody _body;
		private UnityUIWorld _world;

		private void Awake() {
			_body = UIBody.CreateCircle(radius, rectTransform.anchoredPosition, density, isStatic, restitution);

			_world = FindObjectOfType<UnityUIWorld>();
			Assert.IsNotNull(_world, "UnityUIWorld component not present in scene!");
		}

		private void Start() {
			_world.World.AddBody(_body);
		}

		private void Update() {
			rectTransform.anchoredPosition = _body.Position;
		}

		private void OnDestroy() {
			_world.World.RemoveBody(_body);
		}

		public void SetPosition(Vector2 position) {
			_body.MoveTo(position);
		}
		public Vector2 GetPosition() {
			return _body.Position;
		}
	}
}