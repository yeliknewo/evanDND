using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	[SerializeField]
	private float zoomSpeed;
	[SerializeField]
	private float scrollSpeed;
	[SerializeField]
	private float zoomMin;
	[SerializeField]
	private float zoomMax;

	void Update()
	{
		GetCamera().orthographicSize = Mathf.Clamp(GetCamera().orthographicSize - Input.GetAxis(InputManager.AXIS_MOUSE_SCROLL_WHEEL) * zoomSpeed * Time.deltaTime, zoomMin, zoomMax);
		transform.position += new Vector3(Input.GetAxis(InputManager.AXIS_X), Input.GetAxis(InputManager.AXIS_Y)) * scrollSpeed * GetCamera().orthographicSize * Time.deltaTime;
	}

	private Camera GetCamera()
	{
		return GetComponent<Camera>();
	}
}
