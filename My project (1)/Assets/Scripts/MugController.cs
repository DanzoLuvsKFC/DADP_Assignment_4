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
    public bool Mugged = false;
    public GameObject MugTxt;
    public GameObject LevelPnl;

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
            if (Hit.transform.gameObject.name == "Target" && !Mugged)
            {
                Debug.Log("Muggable");
                CanMug = true;
                MugTxt.SetActive(true);
            }
        }
        else
        {
            MugTxt.SetActive(false);
            CanMug = false;
        }
        if (CanMug && Input.GetKeyDown(KeyCode.Space))
        {
            Mug();
        }
    }

    void Mug()
    {
        TargetRb.velocity = new Vector3(0, 0, 0);
        PlayerAnim.SetBool("IsMugging", true);
        StartCoroutine(TargetMug());
    }

    IEnumerator TargetMug()
    {
        yield return new WaitForSeconds(0.2f);
        TargetAnim.SetBool("IsAttacked", true);
        PlayerAnim.SetBool("IsWalking", false);
        TargetRb.isKinematic = true; 
        yield return new WaitForSeconds(6f);
        TargetAnim.SetBool("IsAttacked", false);
        TargetRb.velocity = new Vector3(0, 0, 0);
        Mugged = true;
        MugTxt.SetActive(false);
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0.01f);
        Debug.Log("Show Canvas");
        LevelPnl.SetActive(true);
    }
}
