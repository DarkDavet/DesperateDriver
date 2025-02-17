using UnityEngine;

public class LevelEntrySetup : MonoBehaviour
{
    [SerializeField] private Material skyboxMat;

    private void Start()
    {
        SetSkybox(skyboxMat);
    }

    private void SetSkybox(Material skyboxMat)
    {
        RenderSettings.skybox = skyboxMat;
    }
}
