using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody TargetRb;
    public float RayLength = 10f;
    public Animator TargetAnim;
    public Transform SpawnPoint;
    public GameObject Target;
    public GameObject Player;
    public Animator PlayerAnimator;
    public float LookTime;
    public Animation TurnAnim;

    // Start is called before the first frame update
    void Start()
    {
        //TargetAnim.speed = 4f;
        LookTime = 2f;
        TargetRb = gameObject.GetComponent<Rigidbody>();
        TargetRb.velocity = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetAnim.GetBool("IsTurning") == true)
        {
            StartCoroutine(MoveRay());
        }
    }
    public void TurnAround()
    {
        TargetAnim.SetBool("IsTurning", true);
        TargetRb.velocity = new Vector3(0, 0, 0);
    }

    IEnumerator StartWalking()
    {
        //yield return new WaitForSeconds(2f);
        TargetAnim.SetBool("IsTurning", false);
        yield return new WaitForSeconds(0.8f);
        TargetRb.velocity = new Vector3(1, 0, 0);       
    }

    IEnumerator MoveRay()
    {
        yield return new WaitForSeconds(2f);
        RaycastHit Hit;
        Vector3 RayDirection = Player.transform.position - SpawnPoint.transform.position;
        RayDirection.Normalize();
        Debug.DrawRay(SpawnPoint.position, RayDirection * RayLength, Color.white);
        if (Physics.Raycast(SpawnPoint.position, RayDirection, out Hit, RayLength))
        {
            if (Hit.transform.gameObject.name == "Player" && PlayerAnimator.GetBool("IsWalking") == true)
            {
                Debug.Log("Spotted");
            }
        }
        yield return new WaitForSeconds(LookTime);
        StartCoroutine(StartWalking());
    }
}
