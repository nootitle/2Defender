using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    void Start()
    {
        //object[] Objects = Resources.LoadAll<Sprite>("Sprites");
    }

    public static object[] GetLoadData(string _FileName)
    {
        object[] Objects = Resources.LoadAll<Sprite>(_FileName);

        if (Objects == null)
            return null;

        return Objects;
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class SkillSlotController : MonoBehaviour
{
    [SerializeField] private Image SlotSprite;
    [SerializeField] private GameObject SlotEffect;

    private float CoolTime;

    private bool SkillOn;

    private void Start()
    {
        CoolTime = 4.0f;

        SkillOn = false;
    }

    public void StartCooldown()
    {
        if (SkillOn)
            return;

        SkillOn = true;
        StartCoroutine( SkillCooldown() );
    }
    
    IEnumerator SkillCooldown()
    {
        while (SlotSprite.fillAmount < 1.0f)
        {
            SlotSprite.fillAmount += (Time.deltaTime / CoolTime);

            yield return null;
        }
        
        SkillOn = false;
        SlotSprite.fillAmount = 0.0f;

        SlotEffect.SetActive(true);
    }
}


public class SkillSlotEditor : EditorWindow
{
    [MenuItem("Editor Tools/Skill Editor")]
    private static void WindowBox()
    {
        /*
        SkillSlotEditor Window = (SkillSlotEditor)GetWindow(typeof(SkillSlotEditor));
        Window.Show();
        */
/*
GetWindow(typeof(SkillSlotEditor)).Show();
    }



    private Object _Object = null;




private void OnGUI()
{
    object[] Objects = Resources.LoadAll<Sprite>("Sprites");





    EditorGUILayout.BeginHorizontal();

    /***********************************************************
     * [GUILayout]

     * GUILayout.Width(값)
     * GUILayout.Height(값)
     * GUILayout.MinWidth(폭 최소값)
     * GUILayout.MaxWidth(폭 최대값)
     * GUILayout.MinHeight(높이 최소값)
     * GUILayout.MaxHeight(높이 최대값)
     * GUILayout.ExpandWidth(true or false : 수평 확장을 허용하거나 막는다)
     * GUILayout.ExpandHeight(true or false : 수직 확장을 허용하거나 막는다)

     **********************************************************/










    /*****************************************************************************************************************
     * [EditorStyles]  글꼴
     * 
     * 참고
     * https://docs.unity3d.com/kr/530/ScriptReference/EditorStyles.html
     * 
     * boldFont   굵은 글꼴(Bold)
     * boldLabel   굵은 글꼴 스타일
     * centeredGreyMiniLabel   작은 글꼴의 라벨 스타일
     * colorField   색상 필드에서 사용되는 스타일
     * foldout   EditorGUI.Foldout에서 사용되는 스타일
     * foldoutPreDrop   EditorGUI.Foldout에서 사용되는 스타일
     * helpBox   EditorGUI.HelpBox에 사용되는 스타일
     * inspectorDefaultMargins   관리자에서 사용되는 기본 여백을 얻기 위해 콘텐츠를 이 스타일로 수직 그룹에서 랩합니다.
     * inspectorFullWidthMargins   관리자에서 사용되는 폭의 여백을 얻기 위해 콘텐츠를 이 스타일로 수직 그룹에서 랩합니다.
     * label   라벨이 붙는 모든 EditorGUI에서 사용되는 스타일
     * largeLabel   큰 글꼴의 라벨 스타일
     * layerMaskField   레이어 마스크에서 사용되는 스타일
     * miniBoldFont   작고 굵은 글꼴
     * miniBoldLabel   작은 글씨로 굵은 라벨에 사용되는 스타일
     * miniButton   작은 버튼의 스타일
     * miniButtonLeft   작고 좌편향인 버튼 스타일
     * miniButtonMid   작고 중앙에 가까운 버튼 스타일
     * miniButtonRight   작고 우편향인 버튼 스타일
     * miniFont   작은 글꼴
     * miniLabel   작은 글꼴의 라벨 스타일
     * miniTextField   작은 텍스트 필드에서 사용되는 스타일
     * numberField   숫자를 입력하는 필드에서 사용되는 스타일
     * objectField   오브젝트 필드에서 사용되는 스타일
     * objectFieldMiniThumb   Style used for object fields that have a thumbnail (e.g Textures).
     * objectFieldThumb   오브젝트 필드에서 사용되는 아이콘 이미지의 스타일
     * popup   EditorGUI.Popup과 EditorGUI.EnumPopup에서 사용되는 스타일
     * radioButton   라디오 버튼에 사용되는 스타일
     * standardFont   일반적인 표준 글꼴
     * textArea   EditorGUI.TextArea에 사용되는 스타일
     * textField   EditorGUI.TextField에서 사용되는 스타일
     * toggle   EditorGUI.Toggle에서 사용되는 스타일
     * toggleGroup   EditorGUILayout.BeginToggleGroup에서 사용되는 스타일
     * toolbar   창 상단에 표시할 도구 모음의 배경 스타일
     * toolbarButton   도구 모음 단추에서 사용되는 스타일
     * toolbarDropDown   도구 모음의 드롭 다운에서 사용되는 스타일
     * toolbarPopup   도구 모음의 팝업에서 사용되는 스타일
     * toolbarTextField   도구 모음의 텍스트 필드에 사용되는 스타일
     * whiteBoldLabel   흰색 글자이고 굵은 라벨 스타일
     * whiteLabel   흰색 문자 라벨 스타일
     * whiteLargeLabel   흰색 글자의 큰 글꼴 라벨 스타일
     * whiteMiniLabel   흰색 문자 작은 글꼴의 라벨 스타일
     * wordWrappedLabel   범위 이상의 문자가 입력되었을 때, 반환하는 스타일
     * wordWrappedMiniLabel   작은 글꼴로 범위 이상의 문자가 입력되었을 때, 반환하는 스타일
     ******************************************************************************************************************/
/*
    EditorGUILayout.BeginVertical();
    GUILayout.Space(20);
    GUILayout.Label("Create Skill", EditorStyles.boldLabel);

    EditorGUILayout.BeginHorizontal();
    GUILayout.Space(20);


    _Object = EditorGUILayout.ObjectField("Object Prefab", _Object, typeof(Sprite), false);

    EditorGUILayout.EndHorizontal();




    // ** 현재 들고있는 색을 보관.
    Color Oldcolor = GUI.backgroundColor;

    // ** 현재 색을 빨강색으로 변경.
    GUI.backgroundColor = Color.red;


    if (GUILayout.Button("1",
        GUILayout.MinHeight(20), GUILayout.MinWidth(50),
        GUILayout.Height(23), GUILayout.Width(150),
        GUILayout.MaxHeight(25), GUILayout.MaxWidth(300)))
    {
        /*
        GameObject SkillObj = (GameObject)Instantiate(_Object);

        SkillObj.transform.SetParent( GameObject.Find("Canvas").transform );

        Image Tex = SkillObj.GetComponent<Image>();

        Tex.sprite = (Sprite)Objects[0];
         */

/*
        GameObject SkillObj = new GameObject("Temp");

        SkillObj.transform.SetParent(GameObject.Find("Canvas").transform);

        Image Tex = SkillObj.AddComponent<Image>();

        Tex.sprite = (Sprite)_Object;
    }

    // ** 최초 색으로 변경.
    GUI.backgroundColor = Oldcolor;







    if (GUILayout.Button("2"))
    {

    }

    if (GUILayout.Button("3"))
    {

    }
    EditorGUILayout.EndVertical();

    EditorGUILayout.EndHorizontal();
}
}


/*
public class SkillSlotInspector : Editor
{
    private void OnSceneGUI()
    {
        if (GUILayout.Button("1"))
        {

        }

        if (GUILayout.Button("2"))
        {

        }

        if (GUILayout.Button("3"))
        {

        }
    }
}
 */
