using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CleaningController : MonoBehaviour
{
    float timeElapsed;

    ReceptionController receptionController;

    private void Start()
    {
        receptionController = GameObject.FindGameObjectWithTag("Reception").GetComponent<ReceptionController>();
    }
    public void CleanRoom()
    {
        for (int i = 0; i < receptionController.rooms.Count; i++)
        {
            if (receptionController.rooms[i].transform.position == this.transform.position)
            {
                receptionController.rooms[i].SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Employee"))
            timeElapsed = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Employee"))
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 5)
            {
                CleanRoom();
                Destroy(gameObject);
            }
        }            
    }
}
