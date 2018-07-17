using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    #region References
    private Character character;
    private UIControl uiControl;
    private InputControl inputControl;
    private MessageSystem messageSystem;
    public static GameControl instance;
    #endregion

    #region Creating Holes and Monster Parameters
    [SerializeField]
    private List<Transform> floors;
    [SerializeField]
    private GameObject hole;
    [SerializeField]
    private GameObject monster;
    private List<Vector2> directions = new List<Vector2>();
    #endregion

    #region Prefabs
    [SerializeField]
    private GameObject prfCharacter;
    #endregion

    #region Unity Fucntions
    void Start()
    {
        if (instance)
        {
            Destroy(instance);
        }
        instance = this;

        uiControl = GetComponent<UIControl>();

        inputControl = gameObject.AddComponent<InputControl>();

        messageSystem = GetComponent<MessageSystem>();

        character = Instantiate(prfCharacter).GetComponent<Character>();

        directions.Add(Vector2.left);
        directions.Add(Vector2.right);
        FirstTwoHoles();
    }
    #endregion

    #region Functions For Holes And Monster Creation
    public void CreateHoleOrMonster(int HoleOrMonster)
    {
        int randomPosition = Random.Range(0, floors.Count - 1);
        MovingObjects mo;
        switch (HoleOrMonster)
        {
            case 0:
                mo = Instantiate(hole).GetComponent<MovingObjects>();
                mo.transform.position = new Vector2(mo.transform.position.x,
                    floors[randomPosition].position.y + mo.InitialHigh);
                mo.IndexOnFloor = randomPosition;
                mo.Direction = (directions[Random.Range(0, directions.Count - 1)]);
                break;

            case 1:
                mo = Instantiate(monster).GetComponent<MovingObjects>();
                mo.transform.position = new Vector2(mo.transform.position.x,
                    floors[randomPosition].position.y + mo.InitialHigh);
                mo.IndexOnFloor = randomPosition;
                mo.Direction = (directions[Random.Range(0, directions.Count - 1)]);
                break;
            default:
                break;
        }
    }

    private void FirstTwoHoles()
    {
        MovingObjects mo;
        int randomPosition = Random.Range(0, floors.Count - 1);

        mo = Instantiate(hole).GetComponent<MovingObjects>();
        mo.transform.position = new Vector2(mo.transform.position.x,
            floors[randomPosition].position.y);
        mo.IndexOnFloor = randomPosition;
        mo.Direction = Vector2.left;

        mo = Instantiate(hole).GetComponent<MovingObjects>();
        mo.transform.position = new Vector2(mo.transform.position.x,
            floors[randomPosition].position.y);
        mo.IndexOnFloor = randomPosition;
        mo.Direction = Vector2.right;
    }
    #endregion

    #region Gets For References
    public Character Character
    {
        get
        {
            return character;
        }
    }

    public UIControl UIControl
    {
        get
        {
            return uiControl;
        }
    }

    public InputControl InputControl
    {
        get
        {
            return inputControl;
        }
    }

    public MessageSystem MessageSystem
    {
        get
        {
            return messageSystem;
        }
    }
    #endregion

    #region Gets And Sets For Creating Holes And Monsters
    public List<Transform> Floors
    {
        get
        {
            return floors;
        }
    }
    #endregion
}
