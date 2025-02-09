using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{
    public Image healthbar;
    private Image Border;

    private NewStribog info;

    void Awake()
    {
        info = FindFirstObjectByType<NewStribog>();
        healthbar = GetComponent<Image>();
        Border = GameObject.Find("Border").GetComponent<Image>();
    }
    void Update()
    {
        if (info == null)
        {
            Destroy(Border);
            Destroy(gameObject);

        }
        //healthbar.fillAmount = info.GetHp() / info.MaxHp;
        //print(info.GetHp());
    }
}
