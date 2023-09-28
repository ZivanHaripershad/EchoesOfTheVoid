using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel2 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite enabledButton;
    public Sprite disabledButton;
    public Sprite onHoverButton;
    public UpgradeScene2Manager upgradeScene2Manager;
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
        if (upgradeScene2Manager.GetUpgrade() != null)
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
        if (upgradeScene2Manager.GetUpgrade() != null)
        {
            Debug.Log("Level 2 Upgrade: " + upgradeScene2Manager.GetUpgrade().GetName());
            SelectedUpgradeLevel2.Instance.SetUpgrade(upgradeScene2Manager.GetUpgrade());
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneManager.LoadScene("Level2");
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
