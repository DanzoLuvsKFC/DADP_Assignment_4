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
    public MugController mugController;
    public float AnimSpeed;
    public ProgressBar progressBar;
    public float Timer = 0;
    public bool looking = false;
    public bool Turned = false;

    // Start is called before the first frame update
    void Start()
    {
        LookTime = 2f;
        TargetRb = gameObject.GetComponent<Rigidbody>();
        TargetRb.velocity = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetAnim.GetBool("IsTurning") == true && mugController.Mugged == false)
        {
            StartCoroutine(MoveRay());
        }
        if (looking)
        {
            if (Timer < 2 && Turned == false)
            {
                Timer += Time.deltaTime;
                RaycastHit Hit;
                Vector3 RayDirection = Player.transform.position - SpawnPoint.transform.position;
                RayDirection.Normalize();
                Debug.DrawRay(SpawnPoint.position, RayDirection * RayLength, Color.black);
                if (Physics.Raycast(SpawnPoint.position, RayDirection, out Hit, RayLength))
                {
                    if (Hit.transform.gameObject.name == "Player" && PlayerAnimator.GetBool("IsWalking") == true)
                    {
                        progressBar.progressValue += 0.05f;
                    }
                }
            }
            else
            {
                looking = false;
            }
        }
    }
    public void TurnAround()
    {
        TargetAnim.SetBool("IsTurning", true);
        TargetAnim.speed = AnimSpeed;
        Turned = false;
        TargetRb.velocity = new Vector3(0, 0, 0);
    }

    IEnumerator StartWalking()
    {
        TargetAnim.SetBool("IsTurning", false);
        Turned = true;
        TargetAnim.speed = 1;
        yield return new WaitForSeconds(0.8f);
        TargetRb.velocity = new Vector3(1, 0, 0);       
    }

    IEnumerator MoveRay()
    {
        yield return new WaitForSeconds(1f);
        looking = true;
        Timer = 0;
        yield return new WaitForSeconds(LookTime);
        StartCoroutine(StartWalking());
    }
}
