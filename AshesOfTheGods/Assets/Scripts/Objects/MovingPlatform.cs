using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float platformSpeed;
    [SerializeField] private CollisionCheck collisionCheck;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private PlayerMovement player;
    private Transform targetTransform;
    private Vector2 lastPosition;     // Позиция объекта в предыдущем кадре
    private Vector2 currentSpeed;     // Текущая скорость объекта

    private Vector2 currentSpeedplayer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        targetTransform = pointB;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Текущее положение объекта
        Vector2 currentPosition = transform.position;

        // Вычисление изменения позиции за последний кадр
        Vector2 deltaPosition = currentPosition - lastPosition;

        // Вычисление скорости: изменение позиции / время между кадрами
        currentSpeed = deltaPosition / Time.fixedDeltaTime;

        // Обновляем lastPosition для следующего кадра
        lastPosition = currentPosition;

        MovementLogic();
    }

    private void MovementLogic()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, platformSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetTransform.position) < 0.1f)
        {
            targetTransform = (targetTransform == pointA) ? pointB : pointA;
        }
        if (collisionCheck.contact)
        {
            print(currentSpeed.x);
            player.SetVelocityX(currentSpeed.x);
            print(player.GetVelocityX());
        }
    }
}