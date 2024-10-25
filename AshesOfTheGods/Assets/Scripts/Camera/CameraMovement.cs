using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Timeline;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float speed = 1;
    [SerializeField] private float CurrentDistanceToPlayer = 5;
    void MovementLogic()
    {
      
    }

    void Update()
    {
        MovementLogic();
    }
}
