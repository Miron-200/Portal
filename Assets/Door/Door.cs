using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public enum Option
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    [SerializeField] private Option TypeOpen;    
    [SerializeField] private bool isClose;
    [SerializeField] private bool isKey;


    [HideInInspector] public MeshRenderer Indicator;
    [HideInInspector] public MeshRenderer KeyIndicator;
    [HideInInspector] public Color[] ColorIdicator;
    [HideInInspector] public Animation _Animation;
    private int NumeKey;
    private List<Material> Materials = new List<Material>();

    private bool isOpen;
  
    private void Start()
    {
        ColorIndicator();
        KeyAnalise();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Open");       
        OpenDoor();
    }
    private void OnTriggerExit(Collider other)
    {
        CloseDoor();
        print("Exit");
    }


    private void OpenDoor()
    {
        if (!isOpen && !isClose)
        {           
            _Animation.CrossFade(TypeOpen.ToString(), 0f, PlayMode.StopAll);
            _Animation[TypeOpen.ToString()].speed = 1.0f;     
            isOpen = true;
           
        }
    }

    private void CloseDoor()
    {
        if (isOpen)
        {
            if (_Animation.isPlaying)
            {
                _Animation.CrossFade(TypeOpen.ToString(), 0f, PlayMode.StopAll);
                _Animation[TypeOpen.ToString()].speed = -1f;
            }
            else
            {
                _Animation.CrossFade(TypeOpen.ToString()+"Close", 0f, PlayMode.StopAll);
                _Animation[TypeOpen.ToString()].speed = 1.0f;
            }
          
            isOpen = false;
        }
    }

    public void LockDoor(bool isClosed)
    {
        if (isKey) 
        {           
            NumeKey--;
            Materials[NumeKey].color = ColorIdicator[1];
            if (NumeKey > 0)
            {
                return;
            }
        }
        isClose = isClosed;
        ColorIndicator();
    }

    public void SetNumeKey()
    {
        NumeKey++;
    }

    public bool StatusDoor()
    {
        return isClose;
    }
    private void ColorIndicator()
    {
       Indicator.materials[0].color = ColorIdicator[isClose ? 0 : 1];
    }

    private void KeyAnalise()
    {
        if (isKey)
        {
            KeyIndicator.gameObject.SetActive(true);
            Materials.Add(KeyIndicator.materials[0]);
            for (int i=1; i < NumeKey; i++)
            {
                MeshRenderer key = Instantiate(KeyIndicator, KeyIndicator.transform, true);
                key.transform.localPosition = new Vector3(0, i * -2, 0);
                Materials.Add(key.materials[0]);
            }
        }
    }
}
