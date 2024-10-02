using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    [HideInInspector] public Transform Targets;
    [SerializeField] private bool isAuto;
    [SerializeField] private float Speed;
    [SerializeField] private UnityEvent OnStart;
    [SerializeField] private UnityEvent OnEnd;
    private bool isCall;
    private List<Vector3> PointsPosition = new List<Vector3>();
    
    private int Nume;
    private void Start()
    {       
        Transform[] points = Targets.GetComponentsInChildren<Transform>();
        print(points.Length);
        List<Transform> componentsPoints = new List<Transform>(points);
        print(componentsPoints.Count);
        PointsPosition = componentsPoints.ConvertAll(t => t.position);   
        //PointsPosition.Insert(0, transform.position);        
        enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            enabled = true;
            isCall = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {          
            other.transform.SetParent(null);
           // enabled = false;
        }
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, PointsPosition[Nume + 1], Speed * Time.fixedDeltaTime);   
        if (Vector3.Distance(transform.position, PointsPosition[Nume + 1]) < 0.001f)
        {
            Nume++;
            print(PointsPosition.Count + "==" + Nume);
            if (PointsPosition.Count-1 == Nume)
            {                
                if (isAuto)
                {                
                    PointsPosition.Reverse();
                    Nume = 0;
                }
                else
                {
                    Nume = PointsPosition.Count - 2;
                }            
                enabled = false;
                OnEnd?.Invoke();
            }
        }
    }

    public void CallElevator()
    {
        if (!isAuto)
        {
            PointsPosition.Reverse();
            Nume = 0;
        }
        enabled = true;
        OnStart?.Invoke();
    }

    public Vector3 GetPositionEnd()
    {
       return PointsPosition[PointsPosition.Count - 1];
    }
    public Vector3 GetPositionStart()
    {
        return PointsPosition[0];
    }

}
