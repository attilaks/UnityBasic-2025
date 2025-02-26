using System;
using UnityEngine;

namespace Tools
{
	public class FirstPersonCamera : MonoBehaviour
	{
		[SerializeField] private float mouseSensitivity = 200f;
		
		[SerializeField] private Transform _playerBody;
		private float _xRotation;
		private float _yRotation;

		private void Awake()
		{
			// _playerBody = gameObject.GetComponentInParent<Transform>();
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void Update()
		{
			var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

			if (mouseX != 0)
			{
				var x = 7;
			}
			_playerBody.Rotate(Vector3.up * mouseX);
			
			_xRotation -= mouseY;
			_yRotation += mouseX;

			transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
			// _playerBody.Rotate(0, targetAngle, 0);
		}
	}
}