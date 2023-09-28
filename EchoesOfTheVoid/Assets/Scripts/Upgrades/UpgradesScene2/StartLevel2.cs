using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel2 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite enabledButton;
    public Sprite disabledButton;
    public Sprite onHoverButton;
    public UpgradeScene1Manager upgradeScene1Manager;
    public MouseControl mouseControl;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = true;
        mouseControl.EnableMouse();
    }

    private void Update()
    {
        CheckIfUpgradeIsSet(enabledButton);
    }

    private void CheckIfUpgradeIsSet(Sprite enabledSprite)
    {
        if (upgradeScene1Manager.GetUpgrade() != null)
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledButton;
        }
    }

    private void OnMouseDown()
    {
        if (upgradeScene1Manager.GetUpgrade() != null)
        {
            SelectedUpgradeLevel1.Instance.SetUpgrade(upgradeScene1Manager.GetUpgrade());
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneManager.LoadScene("Level1");
        }
        else
        {
            AudioManager.Instance.PlaySFX("CannotDeposit");
        }
        
    }

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        CheckIfUpgradeIsSet(onHoverButton);
    }

    private void OnMouseExit()
    {
        CheckIfUpgradeIsSet(enabledButton);
    }
}
