using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInvenController : MonoBehaviour
{
    public GameObject Parent;
    public GameObject SkillPrefab;
    private string FileName = "Sprites";

    void Start()
    {
        object[] Objects = LoadData.GetLoadData(FileName);

        for (int i = 0; i < Objects.Length; ++i)
        {
            GameObject SkillObj = Instantiate(SkillPrefab);

            SkillObj.transform.SetParent(Parent.transform);
            SkillObj.transform.name = "Skill_" + i.ToString();
            Image Texture = SkillObj.GetComponent<Image>();
            Texture.sprite = Objects[i] as Sprite;

            RectTransform ContentObject = Parent.GetComponent<RectTransform>();

            int count = (int)(Objects.Length * 0.1f) + 1;
            ContentObject.sizeDelta = new Vector2(1215.0f, count * 125);
        }
    }
}
