//
// Copyright (c) SunnyMonster
// https://www.youtube.com/channel/UCbKQHYlzpR_pa5UL7JNP3kg/
//

using UnityEngine;

namespace MonoBehaviours.Menu
{
    public class Panorama : MonoBehaviour
    {
        [Header("References")]
        public Material unlit;
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
            quadLeft.sharedMaterial = new Material(unlit) {mainTexture = panoramaLeft};
            quadRight.sharedMaterial = new Material(unlit) {mainTexture = panoramaRight};
            quadFront.sharedMaterial = new Material(unlit) {mainTexture = panoramaFront};
            quadBack.sharedMaterial = new Material(unlit) {mainTexture = panoramaBack};
            quadTop.sharedMaterial = new Material(unlit) {mainTexture = panoramaTop};
            quadBottom.sharedMaterial = new Material(unlit) {mainTexture = panoramaBottom};
        }
    }
}