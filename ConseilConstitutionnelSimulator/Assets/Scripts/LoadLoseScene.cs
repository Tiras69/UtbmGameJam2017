using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLoseScene : MonoBehaviour
{
    public Text defeatText;
    public Text defeatTextShadow;
    public Image defeatImage;

    // Use this for initialization
    void Start()
    {
        // Get defeat type from GameManager
        LoseCondition loseCondition = GameManager.Instance.loseCondition;
        // TODO depending the defeat type, assign the attributes (or remove them and the function)
        Sprite defeatSprite = null;
        string defeatDescription = null;
        if (loseCondition == LoseCondition.REVOLUTION)
        {
            defeatSprite = Resources.Load<Sprite>("revolution");
            defeatDescription = "Le peuple en a assez d'être exploité, il se révolte !";
        }
        else if (loseCondition == LoseCondition.BANKRUPT)
        {
            defeatSprite = Resources.Load<Sprite>("bankrupt");
            defeatDescription = "Vous avez perdu votre fortune ! A quoi bon gouverner ?";
        }
        else if (loseCondition == LoseCondition.JAIL)
        {
            defeatSprite = Resources.Load<Sprite>("prison");
            defeatDescription = "Vos pairs en ont eu assez de vos opinions progressistes. Ils vous ont fait arrêter !";
        }
        else // loseCondition == LoseCondition.NONE
        {
            defeatSprite = Resources.Load<Sprite>("revolution2");
            defeatDescription = "Quelque chose s'est mal passé ...";
        }
        this.loadDefeatScene(defeatSprite, defeatDescription);
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    void loadDefeatScene(Sprite defeatSprite, string defeatDescription)
    {
        if (defeatSprite != null)
            this.defeatImage.sprite = defeatSprite;
        if (defeatDescription != null)
        {
            this.defeatText.text = defeatDescription;
            this.defeatTextShadow.text = defeatDescription;
        }
    }
}
