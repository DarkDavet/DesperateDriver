using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private UpgradeSetup upgradeSetup;
    [SerializeField] private FuelTank fuelTank;

    private void Start()
    {
        //fuelTank.SetupTank(upgradeSetup.capacity);
    }
}
