using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    [SerializeField]
    private GameCoordinator coordinator;

    [SerializeField] private Text playerTurn;
    [SerializeField] private Text p1Type;
    [SerializeField] private Text p2Type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(coordinator.Player1Turn)
        {
            playerTurn.text = "Player turn: 1";
        } else
        {
            playerTurn.text = "Player turn: 2";
        }

        p1Type.text = "player 1 type: " + coordinator.P1.ToString();
        p2Type.text = "player 2 type: " + coordinator.P2.ToString();
    }
}
