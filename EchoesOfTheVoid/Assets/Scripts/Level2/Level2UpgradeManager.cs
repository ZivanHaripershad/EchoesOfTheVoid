using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Level2UpgradeManager : MonoBehaviour
{

    [SerializeField] private List<UpgradeIcon> upgradeSprites;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Canvas canvas;

    private void Start()
    {
        if (SelectedUpgradeLevel2.Instance == null ||
            SelectedUpgradeLevel2.Instance.GetUpgrade() == null)
        {
            spriteRenderer.sprite = null;
            var descriptionText = canvas.transform.Find("Description").GetComponent<Text>();
            descriptionText.text = "";
            return;
        }
        
        var level2UpgradeName = SelectedUpgradeLevel2.Instance.GetUpgrade().GetName();

        for (int k = 0; k < upgradeSprites.Count; k++)
        {
            if (upgradeSprites[k].upgradeName.Equals(level2UpgradeName))
            {
                spriteRenderer.sprite = upgradeSprites[k].sprite;
                var descriptionText = canvas.transform.Find("Description").GetComponent<Text>();
                descriptionText.text = SelectedUpgradeLevel2.Instance.GetUpgrade().GetDescription();
            }
        }
    }
}
