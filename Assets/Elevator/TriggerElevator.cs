using UnityEngine;

public class TriggerElevator : MonoBehaviour
{

    [SerializeField] private Elevator _Elevator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _Elevator.CallElevator();

        }
    }
}
