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
    private GameObject fan;
    private LoadFAN FanController;
    //public SkinnedMeshRenderer ref_SMR_EYE_DEF; //EYE_DEFへの参照
    //public SkinnedMeshRenderer ref_SMR_EL_DEF;  //EL_DEFへの参照
    public SkinnedMeshRenderer ref_SMR_MTH_DEF;  //MTH_DEFへの参照
    //public float ratio_Close = 85;           //閉じ目ブレンドシェイプ比率
    //public float ratio_Open = 0;
    public float uuu = 100;
    public float notUuu = 0;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        fan = GameObject.Find("LoadFAN");
        FanController = fan.GetComponent<LoadFAN>();
        anim = GetComponent<Animator>();
        isLook = false;
        anim.SetBool("isComing", false);
        oriPos = transform.position;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLook)
        {
            targetPos = target.position;
            targetPos.y = transform.position.y;
            targetOri = targetPos;
            targetPos.x += (float)0.4 * (Mathf.Sin((target.rotation.eulerAngles.y-110) * Mathf.Deg2Rad) * -1 );
            targetPos.z += (float)0.4 * (Mathf.Cos((target.rotation.eulerAngles.y-110) * Mathf.Deg2Rad) * -1 );
            transform.LookAt(targetPos);
            if (transform.position.x >= targetPos.x + 0.05 || transform.position.y >= targetPos.y + 0.05 || transform.position.x <= targetPos.x - 0.05 || transform.position.y <= targetPos.y - 0.05)
            {
                transform.position += transform.forward * moveSpeed;
            }
            else
            {
                FanController.Play();
                anim.SetBool("isFuu", true);
                anim.SetBool("isComing", false);
                SetFuu();
                sound.PlayOneShot(sound.clip);
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
                targetPos = target.position;
                targetPos.y = transform.position.y;
                transform.LookAt(targetPos);
                isBack = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FanController.Stop();
            anim.SetBool("isFuu", false);
            anim.SetBool("isComing", true);
            isLook = true;
            isBack = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FanController.Stop();
            SetNotFuu();
            anim.SetBool("isFuu", false);
            anim.SetBool("isComing", false);
            isLook = false;
            isBack = false;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            FanController.Stop();
            SetNotFuu();
            anim.SetBool("isFuu", false);
            anim.SetBool("isComing", true);
            isBack = true;
            isLook = false;
        }
    }

    void SetFuu()
    {
        //ref_SMR_EYE_DEF.SetBlendShapeWeight(6, ratio_Close);
        //ref_SMR_EL_DEF.SetBlendShapeWeight(6, ratio_Close);
        ref_SMR_MTH_DEF.SetBlendShapeWeight(8, uuu);
    }

    void SetNotFuu()
    {
        //ref_SMR_EYE_DEF.SetBlendShapeWeight(6, ratio_Open);
        //ref_SMR_EL_DEF.SetBlendShapeWeight(6, ratio_Open);
        ref_SMR_MTH_DEF.SetBlendShapeWeight(8, notUuu);
    }
}
