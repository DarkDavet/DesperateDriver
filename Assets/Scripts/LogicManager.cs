using UnityEngine;

public class LogicManager : Singleton<LogicManager>
{
    [SerializeField] private Player player;
    [SerializeField] private ItemController itemController;
    [SerializeField] private GameEvent m_WinEvent;
    [SerializeField] private GameEvent m_LoseEvent;

    [SerializeField] private GameObject mainEventListener;

    public void Win()
    {
        m_WinEvent.Raise();
    }

    public void Lose()
    {
        m_LoseEvent.Raise();
        ResetLevel();
    }

    public void ResetLevel()
    {
        if (player != null)
        {
            player.ResetPlayer();
        }
        if (itemController != null)
        {
            itemController.ResetItems();
        }

    }
}
