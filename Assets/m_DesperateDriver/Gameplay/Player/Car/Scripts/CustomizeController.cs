using UnityEngine;

public class CustomizeController : MonoBehaviour
{
    [SerializeField] private PlayerSetup playerSetup;
    [SerializeField] private MeshRenderer carBody;
    [SerializeField] private MeshRenderer carSeams;
    void Start()
    {
        carBody.material = playerSetup.carBodyMaterial;
        carSeams.material = playerSetup.carSeamsMaterial;
    }
}
