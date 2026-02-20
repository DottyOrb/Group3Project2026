using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    private static int collectables;
    public TMP_Text collectableText;
    private void Awake()
    {
        instance = this;
    }

    public int getCollectables() 
    { 
        return collectables;
    }
    public void setCollectables(int collectable) 
    {
        collectables += collectable;
        collectableText.text = "Secret Collectables: " +collectables.ToString();
    }

}
