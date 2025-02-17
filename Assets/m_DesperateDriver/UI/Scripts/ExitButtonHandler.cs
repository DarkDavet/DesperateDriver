using UnityEngine;
using UnityEngine.UI;

public class ExitButtonHandler : MonoBehaviour
{
    public Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SceneLoader.Instance.LoadMainMenuScene();
    }
}
