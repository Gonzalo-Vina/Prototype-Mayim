using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    bool employeeSelected;

    [SerializeField] float durationDay;
    float timeElapsed;
    int numberCustomerOfDay;

    float timeElapsedBetweenInstance;
    float instanceCadence;

    Vector3 targetPosition;

    private Camera mainCamera;

    [SerializeField] LayerMask layerMaskEmployee;
    [SerializeField] LayerMask layerMaskBackground;

    [SerializeField] GameObject employeeGO;

    [SerializeField] GameObject prefabCustomer;

    [Header("UI Component")]
    [SerializeField] TMP_Text dayText, goldText;
    int dayNumber, goldNumber;

    ReceptionController receptionController;
    

    void Start()
    {
        receptionController = GameObject.FindGameObjectWithTag("Reception").GetComponent<ReceptionController>();

        mainCamera = Camera.main;
        AddDay(1);
        AddGold(100);
        SetCustomerNumber();
        timeElapsed = 0;
        instanceCadence = (durationDay / 2) / numberCustomerOfDay;
    }

    
    void Update()
    {
        MouseManager();
        TimeEventsManager();
    }

    void TimeEventsManager()
    {
        timeElapsed += Time.deltaTime;
        timeElapsedBetweenInstance += Time.deltaTime;

        if (timeElapsed > durationDay) 
        {
            AddDay(1);
            timeElapsed = 0;
            timeElapsedBetweenInstance = 0;

            SetCustomerNumber();
            instanceCadence = (durationDay / 2) / numberCustomerOfDay;
        }

        StartCoroutine(InstantiateCustomer());
    }
    void MouseManager()
    {
        if (Input.GetMouseButtonDown(0) && !employeeSelected)
            SelectEmployee();

        else if (Input.GetMouseButtonDown(1) && employeeSelected)
            DeselectEmployee();

        else if (Input.GetMouseButtonDown(0) && employeeSelected)
            MoveEmployee();
    }
    void SelectEmployee()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, layerMaskEmployee))
        {
            if (hit.transform.gameObject.CompareTag("Employee"))
            {
                employeeSelected = true;
                employeeGO = hit.transform.gameObject;
            }            
        }      
    }
    void DeselectEmployee()
    {
        employeeSelected = false;
        employeeGO = null;
    }
    void MoveEmployee()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;

        employeeGO.GetComponent<EmployeeController>().Move(targetPosition);
    }
    void SetCustomerNumber()
    {
        numberCustomerOfDay = Random.Range(1, receptionController.rooms.Count - 1);
    }
    public IEnumerator InstantiateCustomer()
    {
        for (int i = 0; i < numberCustomerOfDay; i++)
        {
            yield return new WaitForSeconds(instanceCadence);

            if (timeElapsedBetweenInstance >= instanceCadence * (i + 1))
            {
                Instantiate(prefabCustomer, new Vector3(7.35f, -4.5f, 0f), Quaternion.identity);
                timeElapsedBetweenInstance = 0;
            }

            if(i == numberCustomerOfDay - 1)
            {
                instanceCadence = durationDay * 2; //Ésto hace que no se instancie mas huespedes hasta el otro dia
            }
        }        
    }
    void AddDay(int extraDay)
    {
        dayNumber += extraDay;
        dayText.text = dayNumber.ToString();
    }
    public void AddGold(int extraGold)
    {
        goldNumber += extraGold;
        goldText.text = goldNumber.ToString();
    }
    public int GetDay()
    {
        return dayNumber;
    }
    public int GetGold()
    {
        return goldNumber;
    }
}
