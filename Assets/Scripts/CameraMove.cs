using Photon.Pun;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	private PhotonView photonView;

	public Transform target;
	private Transform camera;
	public Vector3 offset;
	public float sensitivity = 3; // чувствительность мышки
	public float limitMax = 80; // ограничение вращения по Y
	public float limitMin = 35;
	public float hidePl = 1.2f; 
	public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
	public float zoomMax = 10; // макс. увеличение
	public float zoomMin = 3; // мин. увеличение
	private float X, Y;

	void Start()
	{
		photonView = GetComponent<PhotonView>();

		camera = transform;
		limitMax = Mathf.Abs(limitMax);
		if (limitMax > 90) limitMax = 90;
		offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
		camera.position = target.position + offset;
	}

	void Update()
	{
		if (!photonView.IsMine) camera.gameObject.SetActive(false);

		if (Input.GetAxis("Mouse ScrollWheel") > 0 && -Mathf.Abs(zoomMax) != zoomMax) offset.z += zoom;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0 && -Mathf.Abs(zoomMin) != zoomMin) offset.z -= zoom;
		offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

		X = camera.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
		Y += Input.GetAxis("Mouse Y") * sensitivity;
		Y = Mathf.Clamp(Y, -limitMax, limitMin);
		camera.localEulerAngles = new Vector3(-Y, X, 0);
		camera.position = camera.localRotation * offset + target.position;

		Debug.DrawRay(target.position, offset);
	}
}