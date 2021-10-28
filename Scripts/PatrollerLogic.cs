using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerLogic : MonoBehaviour
{
    public GameObject[] waypoints;
    private int waypointIdx;
    private Animator animator;


    private float positionPercentage;

    public Rigidbody2D rb2d;

    public Vector3 newPosition;
    public Vector3 oldPosition;
    public float speed = 100;
    public float resetTime;
    public Vector2 trackVelocity;

    private Vector2 lastPos;
    private Coroutine attackingRoutine;
    private Coroutine waypointsRoutine;
    private SpriteRenderer spriteRenderer;

    public GameObject player;
    public PlayerLogic playerLogic;

    // Use this for initialization
    void Start()
    {
        ChangeTarget();
        animator = GetComponent<Animator>();
        playerLogic = player.GetComponent<PlayerLogic>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var distanceToPlayer = Vector2.Distance(rb2d.position, player.transform.position);
        animator.SetFloat("DistanceToPlayer", distanceToPlayer);
        
        if (player.transform.position.x - rb2d.position.x < 0)
        {
            rb2d.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rb2d.transform.localScale = new Vector3(-1, 1, 1);
        }
        
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

        if (distanceToPlayer > 5)
        {
            if (waypointsRoutine == null)
            {
                waypointsRoutine = StartCoroutine(ChangeTarget());
            }
            
            positionPercentage += Time.deltaTime * speed;
            var pos = Vector3.Lerp(oldPosition, newPosition, positionPercentage);
            rb2d.MovePosition(pos);

            trackVelocity = (rb2d.position - lastPos) * 1 / Time.deltaTime;
            lastPos = rb2d.position;
        }
        else
        {
            if (waypointsRoutine != null)
            {
                StopCoroutine(waypointsRoutine);
                waypointsRoutine = null;
            }

            Move();
        }
        
        
    }
    
    private void Move()
    {
        var target = new Vector2(player.transform.position.x, player.transform.position.y);
        var newPosition = Vector2.MoveTowards(rb2d.position, target, speed * Time.deltaTime);
        rb2d.MovePosition(newPosition);
    }

    IEnumerator ChangeTarget()
    {
        while (true)
        {
            oldPosition = waypoints[waypointIdx].transform.position;
            waypointIdx += 1;
            if (waypointIdx >= waypoints.Length) waypointIdx = 0;
            newPosition = waypoints[waypointIdx].transform.position;
        
            positionPercentage = 0f;
            yield return new WaitForSeconds(resetTime);
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