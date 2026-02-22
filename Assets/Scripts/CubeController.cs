using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    [Header("Target Cube")]
    [SerializeField] private GameObject targetCube;

    [Header("Rotation Force")]
    [SerializeField] private float torqueForceMin = -10f;
    [SerializeField] private float torqueForceMax = 10f;

    public Image image;
    public Sprite imageRedSprite;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = targetCube.GetComponent<Rigidbody>();

        if (_rb != null)
            _rb.isKinematic = true;
    }

    public bool DeActivateCube()
    {
        if (_rb == null) return false;

        _rb.isKinematic = false;
        _rb.AddForce(Vector3.up * 5f +new Vector3(Random.Range(torqueForceMin,torqueForceMax),0,0), ForceMode.Impulse);

        // Добавляем вращающий момент по оси Z
        _rb.AddTorque(Vector3.forward * Random.Range(torqueForceMin, torqueForceMax), ForceMode.Impulse);
        _rb = null;
        return true;
    }

    public void ActivateCube()
    {
         image.sprite  = imageRedSprite;
    }
}