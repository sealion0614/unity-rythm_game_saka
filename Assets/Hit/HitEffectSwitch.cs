using UnityEngine;
using UnityEngine.UI;

public class HitEffectSwitch : MonoBehaviour
{
    [Header("按鍵設定")]
    public KeyCode keyToPress;

    [Header("打擊點設定")]
    public Image hitPointImage;
    public Sprite hitPointNormalSprite;
    public Sprite hitPointPressedSprite;

    [Header("軌道設定")]
    public Image trackImage;
    public Sprite trackNormalSprite;
    public Sprite trackPressedSprite;

    void Start() 
    {
        if (hitPointImage != null) hitPointImage.sprite = hitPointNormalSprite;
        if (trackImage != null) trackImage.sprite = trackNormalSprite;
    }

    void Update() 
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (hitPointImage != null) hitPointImage.sprite = hitPointPressedSprite;
            if (trackImage != null) trackImage.sprite = trackPressedSprite;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            if (hitPointImage != null) hitPointImage.sprite = hitPointNormalSprite;
            if (trackImage != null) trackImage.sprite = trackNormalSprite;
        }
    }
}