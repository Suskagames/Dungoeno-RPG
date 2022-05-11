using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text fileds
    public Text levelText, hitPointText, pesosText, upgradeCostText, xpText;

    //Logic 

    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character selectio 

    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameMenager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChange();
        }
        //If we went too far away
        else
        {
            if (currentCharacterSelection > 0)
                currentCharacterSelection = GameMenager.instance.playerSprites.Count - 1;

            OnSelectionChange();
        }
        
    }
    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameMenager.instance.playerSprites[currentCharacterSelection];
        GameMenager.instance.player.SwapSprite(currentCharacterSelection);
    }
    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameMenager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //Upgrade the character info

    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameMenager.instance.weponSprites[GameMenager.instance.weapon.wepaonLevel];
        if (GameMenager.instance.weapon.wepaonLevel == GameMenager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameMenager.instance.weaponPrices[GameMenager.instance.weapon.wepaonLevel].ToString();

        //Mete
        levelText.text = GameMenager.instance.GetCurrentLevel().ToString();
        hitPointText.text = GameMenager.instance.player.hitpoint.ToString();
        pesosText.text = GameMenager.instance.pesos.ToString();

        //xp bar 
        int currLevel = GameMenager.instance.GetCurrentLevel();

        if(GameMenager.instance.GetCurrentLevel() == GameMenager.instance.xpTabel.Count)
        {
            xpText.text = GameMenager.instance.experience.ToString() + "total expience points"; //Display total xp
            xpBar.localScale = new Vector3(0.5f, 0, 0);
        }
        else
        {
            int prevLevelXp = GameMenager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameMenager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameMenager.instance.experience - prevLevelXp;

            float comletionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(comletionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + "/" + diff;
        }
    }
}
