using UnityEngine;

public class HintOpenScript : MonoBehaviour
{
    

     private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    [SerializeField] private GameObject hintHintText;
    [SerializeField] private GameObject hintHintOffText;
    [SerializeField] private GameObject hintPanelText;
    
    void Update()
    {
        if (Vector2.Distance(player.GetComponent<CapsuleCollider2D>().bounds.center, transform.position) <= 5f)
        {
            hintHintText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                hintPanelText.GetComponentInParent<GameObject>().SetActive(hintPanelText.GetComponentInParent<GameObject>().activeSelf);
                hintPanelText.SetActive(!hintPanelText.activeSelf);
            }
            if (hintPanelText.activeSelf)
            {
                hintHintOffText.SetActive(true);
                hintHintText.SetActive(false);
            }
            else
            {
                hintHintText.SetActive(true);
                hintHintOffText.SetActive(false);
            }
        }
        else
        {
            hintHintText.GetComponentInParent<GameObject>().SetActive(false);
            hintHintText.SetActive(false);
            hintPanelText.SetActive(false);
            hintHintOffText.SetActive(false);
        }

        
        
        
    }
}
