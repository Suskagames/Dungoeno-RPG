using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    public static GameMenager instance;
    private void Awake()
    {
        if(GameMenager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextMenager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceanLoaded;
    }

    //Ressources
    public List<Sprite> playerSprites;
    public List<Sprite> weponSprites;
    public List<int> weaponPrices;
    public List<int> xpTabel;


    //Rederences
    public Player player;
    public Weapon weapon;
    public FloatingTextMenager floatingTextMenager;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnim;

    public int angryGhost;


    //Logic 
    public int pesos;
    public int experience;

    //Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position,
        Vector3 motion, float duration)
    {
        floatingTextMenager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon

    public bool TryUpgradeWeapon()
    {
        //is this  weapon max lvl?
        if(weaponPrices.Count <= weapon.wepaonLevel)
            return false;
        if(pesos >= weaponPrices[weapon.wepaonLevel])
        {
            pesos -= weaponPrices[weapon.wepaonLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTabel[r];
            r++;

            if (r == xpTabel.Count) //Max lvl
                return r;
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while (r<level)
        {
            xp += xpTabel[r];
            r++;
        }
        return xp;
    }    

    public void GrandXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if(currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        Debug.Log("Level Up");
        player.OnLevelUp();
        OnHitpointChange();
    }
    //Hitpoint Bat
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }
    //On Scene Loaded
    public void OnSceanLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    //Save state

    /*
     * INT preferedSkin
     * INT pesos
     * INT experience
     * Int weponLevel
     */

    //save stats
    public void SaveState()
    {

        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.wepaonLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }
    //Load state
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //Change player skin 
        pesos = int.Parse(data[1]);

        //Experience
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1 )
            player.SetLevel(GetCurrentLevel());

        //Change whe weapon level
        weapon.SerWeaponLevel(int.Parse(data[3]));

        //Debug.Log("LoadState");
    }
    //Deat Menu and respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        player.Respawn();
    }
    protected virtual void AngryGhost(int i)
    {
        angryGhost += i;
    }
}
