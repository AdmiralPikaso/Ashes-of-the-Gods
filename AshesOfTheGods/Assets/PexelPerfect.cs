using UnityEngine;
using Unity.Cinemachine; // Убедитесь, что Cinemachine установлен

[RequireComponent(typeof(CinemachineCamera))]
public class CinemachinePixelSnapModern : MonoBehaviour
{
    public float pixelsPerUnit = 32; // Должно совпадать с PPU спрайтов
    private CinemachineCamera cinemachineCam;

    void Start()
    {
        cinemachineCam = GetComponent<CinemachineCamera>();
    }

    void LateUpdate()
    {
        if (cinemachineCam.Follow != null)
        {
            // Получаем позицию цели
            Vector3 targetPos = cinemachineCam.Follow.position;
            
            // Округляем до ближайшего пикселя
            float pixelSize = 1f / pixelsPerUnit;
            targetPos.x = Mathf.Round(targetPos.x / pixelSize) * pixelSize*50;
            targetPos.y = Mathf.Round(targetPos.y / pixelSize) * pixelSize*500;            
                // Альтернатива: двигаем саму камеру
                transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
    }
}