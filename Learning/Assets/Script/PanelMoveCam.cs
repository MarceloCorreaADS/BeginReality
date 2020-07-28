using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PanelMoveCam : MonoBehaviour, IPointerDownHandler, IDragHandler, IScrollHandler{

	Camera cam;
	private Vector3 posCamIni;
	public bool canMoveCamera = false;
	public float mapWidth = 60.0f;
	public float mapHeight = 40.0f;

	public float mouseMove = 20f;

	private float minX, maxX, minY, maxY;
	private float horizontalExtent, verticalExtent;

	public Vector3 oldPos;
	public Vector3 panOrigin;

	private CameraMove cameraMove;

	// Use this for initialization
	void Start() {
		cam = Camera.main;
		posCamIni = cam.transform.position;
		CalculateLevelBounds();
		cameraMove = cam.GetComponent<CameraMove>();
	}
	void CalculateLevelBounds() {
		verticalExtent = cam.orthographicSize;
		horizontalExtent = cam.orthographicSize * Screen.width / Screen.height;
		minX = horizontalExtent - mapWidth / 2.0f;
		maxX = mapWidth / 2.0f - horizontalExtent;
		minY = verticalExtent - mapHeight / 2.0f;
		maxY = mapHeight / 2.0f - verticalExtent;
	}

	void LateUpdate() {
		Vector3 limitedCameraPosition = cam.transform.position;
		limitedCameraPosition.x = Mathf.Clamp(limitedCameraPosition.x, minX + posCamIni.x, maxX + posCamIni.x);
		limitedCameraPosition.y = Mathf.Clamp(limitedCameraPosition.y, minY + posCamIni.y, maxY + posCamIni.y);
		cam.transform.position = limitedCameraPosition;
	}

	public void OnPointerDown(PointerEventData data) {
		oldPos = cam.transform.position;
		panOrigin = cam.ScreenToViewportPoint(Input.mousePosition);                    //Get the ScreenVector the mouse clicked
	}

	public void OnDrag(PointerEventData eventData) {
		Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition) - panOrigin;    //Get the difference between where the mouse clicked and where it moved
		cam.transform.position = oldPos + -pos * mouseMove;                                         //Move the position of the camera to simulate a drag, speed * 10 for screen to worldspace conversion
	}

	public void OnScroll(PointerEventData eventData) {
		Vector2 mouseWheel = eventData.scrollDelta;
		float ajustCam = cameraMove.ajustCam;
		ajustCam += mouseWheel.y * 0.05f;
		if (ajustCam < 4.0f && ajustCam > 0.5f)
			cameraMove.ajustCam = ajustCam;
	}
}
