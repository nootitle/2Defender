using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private GameObject StageBack;
    [SerializeField] private GameObject SkillList;
    [SerializeField] float _jumpPower = 5.0f;
    [SerializeField] float _sprintSpeed = 10.0f;
    bool _jumpTrigger = false;
    bool _sprintTrigger = false;
    Coroutine _sprintCo;

    private bool skillListCheck;
    private Rigidbody2D _rb = null;

    [SerializeField] GameObject _bg = null;
    private float pointX = 0;

    void Start()
    {
        skillListCheck = false;
        SkillList.SetActive(false);
        _rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");
        //float Ver = Input.GetAxis("Vertical");
        //GetAxisRaw : -1, 0, 1¸¸ Ãâ·Â

        jump();
        sprint();

        if(!_sprintTrigger)
            transform.Translate(Hor * 5.0f * Time.deltaTime, 0.0f, 0.0f);
        else
            transform.Translate(Hor * _sprintSpeed * Time.deltaTime, 0.0f, 0.0f);
        //Camera.main.GetComponent<CameraController>().setX(Hor * 0.025f * Time.deltaTime);
        //float xlimit = Camera.main.transform.position.x +
        //Camera.main.aspect * Camera.main.orthographicSize;
        //Vector2 limit = Camera.main.GetComponent<CameraController>().GetCameraEdge(0);

        if (Input.GetKeyDown(KeyCode.Tab))
            SkillListInvisible();

        if (transform.position.x > pointX - 30)
        {
            GameObject Obj = Instantiate(_bg);
            Vector3 vPos = new Vector3(pointX + 30.0f, 0.0f, 0.0f);
            pointX += 30.0f;
            Obj.transform.position = vPos;
        }
    }

    public void SkillListInvisible()
    {
        skillListCheck = !skillListCheck;
        SkillList.SetActive(skillListCheck);
    }

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!_jumpTrigger)
            {
                _jumpTrigger = true;
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
            }
        }
        jumpCoolDown();
    }

    private void sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!_sprintTrigger)
                _sprintCo = StartCoroutine(sprintTerm());
        }
        else
        {
            if(_sprintCo != null)
                StopCoroutine(_sprintCo);
            _sprintTrigger = false;
        }
    }

    void jumpCoolDown()
    {
        if(_jumpTrigger && _rb.velocity.y <= 0.01f && _rb.velocity.y >= -0.01f)
            _jumpTrigger = false;
    }

    IEnumerator sprintTerm()
    {
        yield return new WaitForSeconds(0.5f);

        _sprintTrigger = true;
    }
}
