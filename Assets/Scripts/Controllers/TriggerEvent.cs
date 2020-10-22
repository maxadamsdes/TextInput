using UnityEngine;
using UnityEngine.UI;

public class TriggerEvent : MonoBehaviour
{
    public GameObject inputManager;
    public GameObject playerObj;
    public Text storyHead;
    public Text storyNarrative;
    public InteractionObject currentInterObjScript = null;
    public Animator anim;
    public bool failsafe = false;
    string locationStr;
    public Collider2D Destination;
    

    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("GameController");
        storyHead.text = GameModel.currentLocale.Name;
    }

    void Update()
    {
        if (Destination)
        {
            if ((!failsafe) && (Destination.tag == "Entrance") && (Input.GetKeyDown("e")))
            {
                Debug.Log("Got go " + Destination.name);
                GameModel.nextLocale = GameModel.currentLocale.getLocation(Destination.name);
                if (GameModel.nextLocale == null)
                {
                    storyHead.text = "Sorry can't go " + Destination.name + " | " + GameModel.currentLocale.Name;
                }
                else
                {
                    GameModel.loadLevel.LoadLocation();
                }
                failsafe = true;

            }
        }

    }


    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D encounter)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(encounter, gameObject, OnTriggerExit2D);
        if (encounter.tag == "Trap")
        {
            Debug.Log("Got go " + encounter.name);
            GameModel.nextLocale = GameModel.currentLocale.getLocation(encounter.name);
            if (GameModel.nextLocale == null)
            {
                storyHead.text = (GameModel.currentLocale.Name);
                storyNarrative.text = ($"Sorry can't go {encounter.name}");
            }
            else
            {
                GameModel.loadLevel.LoadLocation();
                
            }
            failsafe = true;
        }
        else if (encounter.tag == "Entrance")
        {
            GameModel.nextLocale = GameModel.currentLocale.getLocation(encounter.name);
            GameModel.currentIntObj = encounter.gameObject;
            GameModel.nextLocation = encounter.name;
            storyHead.text = "Type to 'enter' to go to " + GameModel.currentLocale.getLocation(GameModel.currentIntObj.name).Name;
        }
        else if (encounter.tag == "Respawn")
        {
            if (encounter.name == "RespawnerCave")
            {
                Vector3 temp = new Vector3(2.4f, 1.47f, 0);
                playerObj.transform.position = temp;
                playerObj.GetComponent<PlayerMovement>().horizontalMove = 0f;
            }
            else if (encounter.name == "RespawnerCave2")
            {
                Vector3 temp = new Vector3(3.06f, 1.59f, 0);
                playerObj.transform.position = temp;
                playerObj.GetComponent<PlayerMovement>().horizontalMove = 0f;
            }


        }
        else if (encounter.tag == "Sign")
        {
            GameModel.currentIntObj = encounter.gameObject;
            storyHead.text = GameModel.currentIntObj.tag;
            storyNarrative.text = GameModel.currentIntObj.GetComponent<Text>().text;
        }
        else if (encounter.tag != "Entrance")
        {
            if (encounter.name == "Chest")
            {
                anim = encounter.GetComponent<Animator>();
                anim.SetBool("Encountered", true);
                GameModel.pickUpAble = false;

            }
            else
            {
                GameModel.pickUpAble = true;
            }
            GameModel.currentIntObj = encounter.gameObject;
            storyHead.text = GameModel.currentIntObj.tag;
            storyNarrative.text = GameModel.currentIntObj.GetComponent<Text>().text;
            playerObj.GetComponent<PlayerMovement>().horizontalMove = 0f;
            playerObj.GetComponent<PlayerMovement>().locked = true;
            playerObj.GetComponent<PlayerMovement>().enabled = false;
            if (encounter.name == "Coin")
            {
                GameModel.itemToAdd = 0;
            }
            else if (encounter.name == "Rune")
            {
                GameModel.itemToAdd = 1;
            }
            else if (encounter.name == "Key")
            {
                GameModel.itemToAdd = 2;
            }
            inputManager.GetComponent<MenuController>().openText();
            failsafe = true;
        }
    }

    void OnTriggerExit2D(Collider2D encounter)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(encounter, gameObject);
        if (encounter.name == "Chest")
        {
            anim = encounter.GetComponent<Animator>();
            anim.SetBool("Encountered", false);
        }
        else if (encounter.tag == "Entrance")
        {
            Destination = null;
            storyHead.text = GameModel.currentLocale.Name;
            
        }
        else
        {
            storyHead.text = GameModel.currentLocale.Name;
            storyNarrative.text = GameModel.currentLocale.Story;
        }
        GameModel.pickUpAble = false;
        GameModel.currentIntObj = null;
        failsafe = false;
        
    }

}
