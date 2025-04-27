using SaveSystem;
using Tools.Managers.Interfaces;
using UnityEngine;

namespace Tools.Managers
{
	public class FirstPersonCamera : MonoBehaviour, ICameraReader
	{
		[Range(100, 1000)]
		[SerializeField] private float mouseSensitivity = 200f;
		[SerializeField] private Transform playerTransform;
		
		private float _xRotation;
		private float _yRotation;

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void Update()
		{
			var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
			
			_xRotation = Mathf.Clamp(_xRotation - mouseY, -90f, 90f);
			_yRotation += mouseX;
			
			playerTransform.localRotation = Quaternion.Euler(0f, _yRotation, 0f);
			transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
		}

		public SerializableVector3 CameraRotation => transform.localRotation.eulerAngles;
	}
}