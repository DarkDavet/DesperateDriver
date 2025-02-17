using UnityEngine;
using UnityEngine.UI;

public class PlayButtonHandler : MonoBehaviour
{
    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SceneLoader.Instance.LoadGameplayScene();
    }
}
