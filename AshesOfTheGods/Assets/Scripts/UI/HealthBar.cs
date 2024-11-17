using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    private Image Border;

    private NewStribog info;

    void Awake()
    {
        info = FindFirstObjectByType<NewStribog>();
        healthBar = GetComponent<Image>();
        Border = GameObject.Find("Border").GetComponent<Image>();
    }
    void Update()
    {
        if (info == null)
        {
            Destroy(Border);
            Destroy(gameObject);

        }
        healthBar.fillAmount = info.GetHp() / info.MaxHp;
        print(info.GetHp());
    }
}
