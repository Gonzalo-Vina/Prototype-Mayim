using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject buttonRooms;
    [SerializeField] GameObject limitsRooms;

    [SerializeField] GameObject buttonRestaurant;
    [SerializeField] GameObject limitsRestaurant;

    GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();

        limitsRooms.SetActive(true);
        limitsRestaurant.SetActive(true);
    }
    public void RemoveLimitsRooms()
    {
        if(gameController.GetGold() >= 1000)
        {
            gameController.AddGold(-1000);
            buttonRooms.SetActive(false);
            limitsRooms.SetActive(false);
        }
        
    }
    public void RemoveLimitsRestaurant()
    {
        if (gameController.GetGold() >= 1000)
        {
            gameController.AddGold(-1000);
            buttonRestaurant.SetActive(false);
            limitsRestaurant.SetActive(false);
        }
            
    }
}
