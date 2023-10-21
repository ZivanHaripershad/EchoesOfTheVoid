using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Level1UpgradeManager : MonoBehaviour
{

    [SerializeField] private List<UpgradeIcon> upgradeSprites;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Canvas canvas;

    private void Start()
    {
        if (SelectedUpgradeLevel1.Instance == null ||
            SelectedUpgradeLevel1.Instance.GetUpgrade() == null)
        {
            spriteRenderer.sprite = null;
            var descriptionText = canvas.transform.Find("Description").GetComponent<Text>();
            descriptionText.text = "";
            return;
        }
        
        var level1UpgradeName = SelectedUpgradeLevel1.Instance.GetUpgrade().GetName();

        for (int k = 0; k < upgradeSprites.Count; k++)
        {
            if (upgradeSprites[k].upgradeName.Equals(level1UpgradeName))
            {
                spriteRenderer.sprite = upgradeSprites[k].sprite;
                var descriptionText = canvas.transform.Find("Description").GetComponent<Text>();
                descriptionText.text = SelectedUpgradeLevel1.Instance.GetUpgrade().GetDescription();
            }
        }
    }
}
