using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text TimeTxt;
    public float Interval = 0f;
    public int Seconds = 30;
    public EnemyController enemyController;
    public bool FunctionCalled = false;
    public Animator TargetAnim;
    public Animator PlayerAnim;
    public MugController mugController;
    public Vector3 PlayerPos;
    public Vector3 TargetPos;
    public GameObject Player;
    public GameObject Target;
    public Rigidbody TargetRb;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = Player.transform.position;
        TargetPos = Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (mugController.Mugged == false)
        {
            if (Interval < 1)
            {
                Interval += Time.deltaTime;
            }
            else
            {
                Seconds--;
                Interval = 0;
                TimeTxt.text = $"Time Remaining:  {Seconds}";
            }
            if (Seconds % 3 == 0)
            {
                int RandomNo = Random.Range(1, 6);
                if (RandomNo == 1 && FunctionCalled == false || RandomNo == 2 && FunctionCalled == false)
                {
                    Debug.Log("Turn");
                    enemyController.TurnAround();
                    FunctionCalled = true;
                    StartCoroutine(Reset());
                }
            }
        }        
        else
        {
            Debug.Log("Mugged");
        }
        if (Seconds == 0)
        {
            SceneManager.LoadScene("EndOfGameCanvas");
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(4f);
        FunctionCalled = false;
    }

    public void LoadNextLevel()
    {
        mugController.Mugged = false;
        Player.transform.position = PlayerPos;
        Target.transform.position = TargetPos;
        TargetAnim.SetBool("IsTurning", false);
        TargetAnim.SetBool("IsAttacked", false);
        PlayerAnim.SetBool("IsWalking", false);
        PlayerAnim.SetBool("IsMugging", false);
        PlayerAnim.SetBool("NewLevel", true);
        TargetRb.isKinematic = false;
        TargetRb.velocity = new Vector3(1, 0, 0);
        Seconds = 30;
        enemyController.AnimSpeed = enemyController.AnimSpeed + 0.8f;
    }
}
