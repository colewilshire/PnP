using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadshotRender : MonoBehaviour
{
    private HeadshotRenderer headshotRenderer;
    private TestPlayer player;
    private RawImage activeCharacterHeadshot;
    private CharacterSelectionHandler character;

    private void Update()
    {
        player = FindObjectOfType<TestPlayer>();
        if (!player) return;

        headshotRenderer = player.GetComponent<HeadshotRenderer>();

        if (headshotRenderer)
        {
            GetComponent<RawImage>().texture = headshotRenderer.headshotRenderTexture;
        }
    }
}
