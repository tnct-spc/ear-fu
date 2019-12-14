using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuu : MonoBehaviour
{
    private Animator anim;
    private Vector3 targetPos, targetOri, oriPos;
    public Transform target;
    public float moveSpeed;
    private bool isLook, isBack;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isLook = false;
        anim.SetBool("isComing", false);
        oriPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLook)
        {
            targetPos = target.position;
            targetPos.y = transform.position.y;
            targetOri = targetPos;
            targetPos.x += (float)0.8 * (Mathf.Sin((target.rotation.eulerAngles.y-90) * Mathf.Deg2Rad) * -1 );
            targetPos.z += (float)0.8 * (Mathf.Cos((target.rotation.eulerAngles.y-90) * Mathf.Deg2Rad) * -1 );
            transform.LookAt(targetPos);
            if (transform.position.x >= targetPos.x + 0.05 || transform.position.y >= targetPos.y + 0.05 || transform.position.x <= targetPos.x - 0.05 || transform.position.y <= targetPos.y - 0.05)
            {
                transform.position += transform.forward * moveSpeed;
            }
            else
            {
                anim.SetBool("isFuu", true);
                anim.SetBool("isComing", false);
                anim.SetBool("backASAP", false);
                transform.LookAt(targetOri);
                isLook = false;
            }
        }
        if (isBack)
        {
            targetPos = oriPos;
            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);
            if (transform.position.x >= targetPos.x + 0.005 || transform.position.y >= targetPos.y + 0.005 || transform.position.x <= targetPos.x - 0.005 || transform.position.y <= targetPos.y - 0.005)
            {
                transform.position += transform.forward * moveSpeed;
            }
            else
            {
                anim.SetBool("isComing", false);
                anim.SetBool("backASAP", false);
                targetPos = target.position;
                targetPos.y = transform.position.y;
                transform.LookAt(targetPos);
                isBack = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("backASAP", true);
            anim.SetBool("isFuu", false);
            anim.SetBool("isComing", true);
            isLook = true;
            isBack = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            anim.SetBool("isFuu", false);
            anim.SetBool("isComing", false);
            isLook = false;
            isBack = false;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            anim.SetBool("backASAP", true);
            anim.SetBool("isFuu", false);
            anim.SetBool("isComing", true);
            isBack = true;
            isLook = false;
        }
    }
}
