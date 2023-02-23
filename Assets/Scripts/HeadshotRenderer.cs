using UnityEngine;
using UnityEngine.UI;

public class HeadshotRenderer : MonoBehaviour
{
    Camera headshotCamera;
    public RenderTexture headshotRenderTexture { get; private set; }
    [SerializeField] RawImage activeCharacterHeadshot;
    [SerializeField] RawImage dynastCharacterHeadshot;

    private void Start()
    {
        headshotCamera = gameObject.GetComponentInChildren<Camera>();

        headshotRenderTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        headshotRenderTexture.Create();

        headshotCamera.targetTexture = headshotRenderTexture;

        if (activeCharacterHeadshot && dynastCharacterHeadshot)
        {
            activeCharacterHeadshot.texture = headshotRenderTexture;
            dynastCharacterHeadshot.texture = headshotRenderTexture;
        }
    }
}
