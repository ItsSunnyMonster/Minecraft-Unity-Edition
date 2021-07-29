//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;
using UnityEngine.Serialization;

public class Panorama : MonoBehaviour
{
    [Header("References")]
    [FormerlySerializedAs("unlit")] 
    public Material baseMaterial;
    [Space]
    [Header("Textures")] 
    public Texture2D panoramaLeft;
    public Texture2D panoramaFront;
    public Texture2D panoramaRight;
    public Texture2D panoramaBack;
    public Texture2D panoramaTop;
    public Texture2D panoramaBottom;
    [Space]
    [Header("Quads")]
    public MeshRenderer quadLeft;
    public MeshRenderer quadFront;
    public MeshRenderer quadRight;
    public MeshRenderer quadBack;
    public MeshRenderer quadTop;
    public MeshRenderer quadBottom;

    private void Awake()
    {
        SetTextures();
    }

    public void SetTextures()
    {
        quadLeft.sharedMaterial = new Material(baseMaterial) {mainTexture = panoramaLeft};
        quadRight.sharedMaterial = new Material(baseMaterial) {mainTexture = panoramaRight};
        quadFront.sharedMaterial = new Material(baseMaterial) {mainTexture = panoramaFront};
        quadBack.sharedMaterial = new Material(baseMaterial) {mainTexture = panoramaBack};
        quadTop.sharedMaterial = new Material(baseMaterial) {mainTexture = panoramaTop};
        quadBottom.sharedMaterial = new Material(baseMaterial) {mainTexture = panoramaBottom};
    }

    public void ResetTextures()
    {
        quadLeft.sharedMaterial = baseMaterial;
        quadRight.sharedMaterial = baseMaterial;
        quadFront.sharedMaterial = baseMaterial;
        quadBack.sharedMaterial = baseMaterial;
        quadTop.sharedMaterial = baseMaterial;
        quadBottom.sharedMaterial = baseMaterial;
    }
}