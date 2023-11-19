using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionController : MonoBehaviour
{
    public List<GameObject> rooms = new List<GameObject>();

    [SerializeField] private GameObject[] extraRooms;

    bool hotelFull;

    GameController gameController;

    [SerializeField] GameObject prefabCleanIcon;

    private void Start()
    {
        hotelFull = false;

        gameController = GameObject.FindGameObjectWithTag("Game Controller").GetComponent<GameController>();
    }
    private void Update()
    {
        
    }

    IEnumerator SetRoomToCustomer(GameObject customer)
    {
        yield return new WaitForSeconds(5f);


        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].activeSelf)
            {
                customer.GetComponent<CustomerController>().SetRoom(rooms[i]);
                customer.GetComponent<CustomerController>().haveRoom = true;
                rooms[i].SetActive(false);
                i = rooms.Count;
            }
        }
    }
    void CheckAvailableRooms()
    {
        int availableRooms = 0;

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].activeSelf)
            {
                availableRooms++;
            }
        }

        if(availableRooms > 0) 
        {
            hotelFull = false;
        }
        else
        {
            hotelFull = true;
        }
    }
    IEnumerator DismissCustomer(GameObject customer)
    {
        yield return new WaitForSeconds(5f);
        customer.GetComponent<CustomerController>().Move(new Vector3(7.35f, -4.5f, 0f));
        yield return new WaitForSeconds(4f);
        Destroy(customer);
    }
    IEnumerator CheckOutCustomer(GameObject customer)
    {
        yield return new WaitForSeconds(4f);
        customer.GetComponent<CustomerController>().Move(new Vector3(7.35f, -4.5f, 0f));
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].transform.position == customer.GetComponent<CustomerController>().room.transform.position)
            {
                Instantiate(prefabCleanIcon, rooms[i].transform.position, Quaternion.identity);
            }
        }

        gameController.AddGold(100);
        Destroy(customer);
    }
    public void AddRooms()
    {
        foreach (GameObject room in extraRooms)
        {
            rooms.Add(room);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Customer"))
        {
            CheckAvailableRooms();

            if (other.gameObject.GetComponent<CustomerController>().haveRoom == false && !hotelFull)
            {
                StartCoroutine(SetRoomToCustomer(other.gameObject));
            }
            else if (other.gameObject.GetComponent<CustomerController>().haveRoom == false && hotelFull)
            {
                StartCoroutine(DismissCustomer(other.gameObject));
            }

            if (other.gameObject.GetComponent<CustomerController>().haveRoom == true &&
                other.gameObject.GetComponent<CustomerController>().checkoutTime == true)
            {
                StartCoroutine(CheckOutCustomer(other.gameObject));
            }
        }
    }
}
