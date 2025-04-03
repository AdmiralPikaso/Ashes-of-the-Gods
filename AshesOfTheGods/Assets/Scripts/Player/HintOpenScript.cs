using UnityEngine;

public class HintOpenScript : MonoBehaviour
{
    

     private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    [SerializeField] private GameObject hintHintText;
    [SerializeField] private GameObject hintPanel;
    
    void Update()
    {
        if (Vector2.Distance(player.GetComponent<CapsuleCollider2D>().bounds.center, transform.position) <= 5f)
        {
            hintHintText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
                hintPanel.SetActive(!hintPanel.activeSelf);

        }
        else
        {
            hintHintText.SetActive(false);
            hintPanel.SetActive(false);
        }

    }
}
