using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public Health hp;
    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI gameoverText;
    private bool gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Health>();
    }

    // Update is called once per frame
    public void GotHit()
    {
        if (hp.currentHealth <= 0)
        {
            if (!gameOver)
            {
                GetComponent<Animator>().SetBool("Invisible", true);
                gameOver = true;
            }
        }
        else
        {
            hp.currentHealth -= 1;
            playerHpText.SetText("Player HP: " + hp.currentHealth);
        }
    }

    public void EndGame()
    {
        gameoverText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    
}
