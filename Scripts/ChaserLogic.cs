using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserLogic : MonoBehaviour
{
    private GameObject player;
    private PlayerLogic playerLogic;

    private Coroutine attackingRoutine;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        var distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 1)
        {
            if (attackingRoutine == null)
            {
                attackingRoutine = StartCoroutine(AttackRoutine());
            }
        }
        else
        {
            if (attackingRoutine != null)
            {
                StopCoroutine(attackingRoutine);
                attackingRoutine = null;
            }
        }
    }
    
    IEnumerator AttackRoutine()
    {
        while (transform)
        {
            yield return new WaitForSeconds(0.5f);
            playerLogic.GotHit();
        }
    }
}
