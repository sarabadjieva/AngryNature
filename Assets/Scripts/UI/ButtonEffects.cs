using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonEffects : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    public Vector3 from;
    public Vector3 to = new Vector3(0.9f, 0.9f, 1f);

    [Header("Sprites")]
    public Image imgButton;
    public List<Sprite> sprites;

    [Header("Audio")]
    public bool PlaySound = true;
    public AudioClip CustomSound;

    public void Start()
    {
        button = this.GetComponent<Button>();
        from = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlaySound && button.interactable)
        {
            if (CustomSound != null)
            {
                AudioManager.Instance.PlayOneShot(CustomSound);
            }
            else
            {
                AudioManager.Instance.PlayButtonSound();
            }
        }
    }

    public void SwitchSprites(int spriteIndex)
    {
        imgButton.sprite = sprites[spriteIndex];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button.interactable)
        {
            transform.localScale = to;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button.interactable)
        {
            transform.localScale = from;
        }
    }
}
