using UnityEngine;
using System.Collections;

namespace CMF
{
	//This script smoothes the position of a gameobject;
	public class SmoothCameraZoom : MonoBehaviour
	{
		//The camera to adjust
		public Camera cam;

		//The controller whose velocity we want to track
		public AdvancedWalkerController controller;

		//Speed that controls how fast the current position will be smoothed toward the target position when 'Lerp' is selected as smoothType;
		public float lerpSpeed = 20f;

		//FOV limits for the camera
		public float minOrthoSize = 90f;
		public float maxOrthoSize = 120f;

		//Limitas on controller speed
		public float sqrSpeedForMaxOrthoSize = 50*50;

		//'UpdateType' controls whether the smoothing function is called in 'Update' or 'LateUpdate';
		public enum UpdateType
		{
			Update,
			LateUpdate
		}
		public UpdateType updateType;

		void Awake()
		{
		}

		//OnEnable;
		void OnEnable()
		{
			ResetCurrentOrthoSize();
		}

		void Update()
		{
			if (updateType == UpdateType.LateUpdate)
				return;
			SmoothUpdate();
		}

		void LateUpdate()
		{
			if (updateType == UpdateType.Update)
				return;
			SmoothUpdate();
		}

		void SmoothUpdate()
		{
			//Smooth current FOV;
			cam.orthographicSize = Smooth(cam.orthographicSize, controller.GetVelocity().sqrMagnitude, lerpSpeed);
		}

		float Smooth(float curOrthoSize, float curSqrSpeed, float _smoothTime)
		{
			float _target = Mathf.Lerp(minOrthoSize, maxOrthoSize, curSqrSpeed / sqrSpeedForMaxOrthoSize);

			return Mathf.Lerp(curOrthoSize, _target, Time.deltaTime * _smoothTime);
		}

		public void ResetCurrentOrthoSize()
		{
			cam.orthographicSize = minOrthoSize;
		}
	}
}