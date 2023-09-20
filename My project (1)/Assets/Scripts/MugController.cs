using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugController : MonoBehaviour
{
    public float RayLength = 1f;
    public bool CanMug = false;
    public Animator PlayerAnim;
    public Animator TargetAnim;
    public GameObject Player;
    public GameObject Target;
    public Rigidbody TargetRb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit Hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * RayLength, Color.white);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, RayLength))
        {
            if (Hit.transform.gameObject.name == "Target")
            {
                Debug.Log("Muggable");
                CanMug = true;
            }
        }
        if (CanMug && Input.GetKeyDown(KeyCode.Space))
        {
            Mug();
        }
    }

    void Mug()
    {
        PlayerAnim.SetBool("IsMugging", true);
        StartCoroutine(TargetMug());
        TargetRb.velocity = new Vector3(0, 0, 0);
    }

    IEnumerator TargetMug()
    {
        yield return new WaitForSeconds(0.2f);
        TargetAnim.SetBool("IsAttacked", true);
    }
}
