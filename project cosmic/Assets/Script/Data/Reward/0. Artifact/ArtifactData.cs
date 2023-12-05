using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewArtifactData", menuName = "ArtifactData")]
public class ArtifactData : ScriptableObject
{
    //
    public int artifactID = 0;
    public Sprite artifactSprite;
    public string artifactName = "Artifact Name";

    public OutcomeOffset[] eventOffset;


    //얻을 유물 코멘트
    public string detailText;

    //얻은 유물 코멘트
    public string commentText = "Comment";

}

[System.Serializable]
public class EffectNode
{
    bool reward;
    string text;
}
