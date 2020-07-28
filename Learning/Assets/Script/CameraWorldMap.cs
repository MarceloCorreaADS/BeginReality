using UnityEngine;

public class CameraWorldMap : MonoBehaviour {
	Camera cam;
	public float view = 100.0f;
	public float ajustCam = 2.0f;
	public float m_speed = 0.1f;
	public Transform target;

	void Start() {
		cam = GetComponent<Camera>();
	}

	void Update() {
		cam.orthographicSize = (Screen.height / view) / ajustCam;
		if (target) {
			transform.position = Vector3.Lerp(transform.position, target.position, m_speed);
		}
	}
}
