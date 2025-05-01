using SaveSystem;
using SaveSystem.Interfaces;
using Tools.Managers.Interfaces;
using UnityEngine;
using VContainer;

namespace Tools.Managers
{
	public class FirstPersonCamera : MonoBehaviour, ICameraReader
	{
		[Range(100, 1000)]
		[SerializeField] private float mouseSensitivity = 200f;
		[SerializeField] private Transform playerTransform;
		
		[Inject] private ISaveDataApplier _saveDataApplier;
		
		private float _xRotation;
		private float _yRotation;

		private void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void Start()
		{
			var loadedSave = _saveDataApplier.GetSaveDataTobeApplied();
			if (loadedSave == null) return;

			transform.localRotation = Quaternion.Euler((Vector3)loadedSave.Value.cameraRotation);
			Debug.Log("Camera rotation is applied");
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