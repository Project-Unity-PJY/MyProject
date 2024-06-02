using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/ObjectData", order = 1)]
public class ObjectData : ScriptableObject
{
    public int id; // 오브젝트 ID
    public string[] dialogueLines; // 대사
    public Sprite dialogueImage; // 대화 이미지
}
