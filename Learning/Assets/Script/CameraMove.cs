using UnityEngine;

public class CameraMove : MonoBehaviour {
	Camera cam;
	public float view = 100.0f;
	public float ajustCam = 2.0f;

	void Start() {
		cam = GetComponent<Camera>();
	}

	void Update() {
		cam.orthographicSize = (Screen.height / view) / ajustCam;
	}
}
