using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI TimeTxt;
    public float Interval = 0f;
    public int Seconds = 20;
    public EnemyController enemyController;
    public bool FunctionCalled = false;
    public Animator TargetAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        if (Seconds % 2 == 0)
        {
            int RandomNo = Random.Range(1, 8);
            if (RandomNo == 1 && FunctionCalled == false || RandomNo == 2 && FunctionCalled == false)
            {
                Debug.Log("Turn");
                enemyController.TurnAround();
                //TargetAnim.speed = 4f;
                FunctionCalled = true;
                StartCoroutine(Reset());
            }
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(4f);
        FunctionCalled = false;
    }
}
