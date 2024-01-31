using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUI : MonoBehaviour
{
    [SerializeField] private Text txtPlayer;
    [SerializeField] private Text txtEnemy;
    [SerializeField] private Text txtMessage;
    private string txtNewMessage;
    [SerializeField] private EnemyDetection enemyCount;
    [SerializeField] private OpenDoor openDoor;
    public Player player;

    void Start()
    {
        txtPlayer.text = (player.life / 100 + 1).ToString();
    }

    void Update()
    {
        UpdateCountEnemy();

        if(enemyCount.slimes.Count <= 0)
        {
            OpenTheDoor();
        }
    }

    public void UpdateLifePlayer(int life)
    {
        txtPlayer.text = (life / 100 + 1).ToString();

        if(life <= 0)
        {
            txtPlayer.text = 0.ToString();
        }
    }
    public void UpdateCountEnemy()
    {
        txtEnemy.text = enemyCount.slimes.Count.ToString();
    }

    void OpenTheDoor() {
        txtNewMessage = "Go to the next level";
        txtMessage.text = txtNewMessage;
        txtMessage.color = Color.green;

        openDoor.OpenDoors();
    }
    
}
