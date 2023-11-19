using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    private float posX, posY;
    public int dayEntered;

    NavMeshAgent agent;

    [SerializeField] private GameObject reception;
    [SerializeField] public GameObject room;
    public bool haveRoom;
    public bool checkoutTime;

    [SerializeField] GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();

        posX = gameObject.transform.position.x;
        posY = gameObject.transform.position.y;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        reception = GameObject.FindGameObjectWithTag("Reception");

        Move(reception.transform.position);

        haveRoom = false;
        checkoutTime = false;

        dayEntered = gameController.GetDay();
    }

    void Update()
    {
        if (haveRoom && dayEntered == gameController.GetDay())
        {
            Move(room.transform.position);
        } 
        else if(haveRoom && dayEntered != gameController.GetDay())
        {
            checkoutTime = true;
            Move(reception.transform.position);            
        }
    }
    public void Move(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public void SetRoom(GameObject roomTarget)
    {
        room = roomTarget;
    }
}
