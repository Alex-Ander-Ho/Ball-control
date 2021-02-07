using Photon.Pun;
using UnityEngine;

public class ModelsScript : MonoBehaviour
{
    private PhotonView photonView;

    public Transform target;
    public GameObject cam;
    public float smooth = 12;

    private Vector3 offset;

    // расположение относительно игрока
    [Header("LocalPosition")]
    public float px;
    public float py;
    public float pz;
    // локальный поворот относительно игрока
    [Header("LocalRotation")]
    public float rx;
    public float ry;
    public float rz;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        offset = new Vector3(px, py, pz);
        transform.localEulerAngles = new Vector3(rx, cam.transform.localEulerAngles.y + ry, rz);
        transform.position = transform.localRotation * offset + target.position;
    }
}
