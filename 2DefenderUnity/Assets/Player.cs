using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private GameObject StageBack;
    [SerializeField] private GameObject SkillList;

    private bool skillListCheck;

    [SerializeField] GameObject _bg = null;
    private SpriteRenderer BG;
    private float pointX = 0;

    void Start()
    {
        BG = _bg.transform.GetChild(0).GetComponent<SpriteRenderer>();
        float pointX = _bg.transform.position.x + (BG.size.x / 2);

        skillListCheck = false;
        SkillList.SetActive(false);
    }

    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");
        float Ver = Input.GetAxis("Vertical");
        //GetAxisRaw : -1, 0, 1¸¸ Ãâ·Â

        transform.Translate(Hor * 5.0f * Time.deltaTime, Ver * 5.0f * Time.deltaTime, 0.0f);
        Camera.main.GetComponent<CameraController>().setX(Hor * 0.025f * Time.deltaTime);
        //float xlimit = Camera.main.transform.position.x +
        //Camera.main.aspect * Camera.main.orthographicSize;
        Vector2 limit = Camera.main.GetComponent<CameraController>().GetCameraEdge(0);

        if (Input.GetKeyDown(KeyCode.Tab))
            SkillListInvisible();

        if (limit.x > pointX - 5)
        {
            GameObject Obj = Instantiate(_bg);
            Vector3 vPos = new Vector3(pointX + BG.size.x, 0.0f, 0.0f);
            pointX += BG.size.x;
            Obj.transform.position = vPos;
        }
    }

    public void SkillListInvisible()
    {
        skillListCheck = !skillListCheck;
        SkillList.SetActive(skillListCheck);
    }
}
