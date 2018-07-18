using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private List<Vector2> directions = new List<Vector2>();
    [SerializeField]
    private List<GameObject> monsters;
    #endregion

    #region Levels Parameters
    private int actualLevel;
    [SerializeField]
    private Transform initialPosition;
    private List<GameObject> allMovingObjects = new List<GameObject>();
    #endregion

    #region Score Parameters
    private int score;
    private int scoreAdded;
    #endregion

    #region Re-Size Parameters
    [SerializeField]
    private Transform firstFloor;
    #endregion

    #region Sounds Parameters
    [SerializeField]
    private AudioSource writingSound;
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

        ReSize();

        uiControl = GetComponent<UIControl>();

        inputControl = gameObject.AddComponent<InputControl>();

        messageSystem = GetComponent<MessageSystem>();

        character = Instantiate(prfCharacter, initialPosition.position, Quaternion.identity)
            .GetComponent<Character>();

        directions.Add(Vector2.left);
        directions.Add(Vector2.right);
        FirstTwoHoles();
        scoreAdded = 5;
    }
    #endregion

    #region Functions For Holes And Monster Creation
    public void CreateHoleOrMonster(int HoleOrMonster, int wishOne)
    {
        int randomPosition = Random.Range(0, floors.Count - 1);
        MovingObjects mo;
        switch (HoleOrMonster)
        {
            case 0:
                mo = Instantiate(hole).GetComponent<MovingObjects>();
                mo.transform.position = new Vector2(Random.Range(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x,
                    Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x), 
                    floors[randomPosition].position.y + mo.InitialHigh);
                mo.IndexOnFloor = randomPosition;
                mo.Direction = (directions[Random.Range(0, directions.Count - 1)]);
                allMovingObjects.Add(mo.gameObject);
                break;

            case 1:
                mo = Instantiate(monsters[wishOne]).GetComponent<MovingObjects>();
                mo.transform.position = new Vector2(Random.Range(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x,
                    Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x),
                    floors[randomPosition].position.y + mo.InitialHigh);
                mo.IndexOnFloor = randomPosition;
                mo.Direction = (directions[Random.Range(0, directions.Count - 1)]);
                if (mo.Direction == Vector2.left)
                {
                    mo.GetComponent<SpriteRenderer>().flipX = true;
                }
                allMovingObjects.Add(mo.gameObject);
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
        mo.transform.position = new Vector2(Random.Range(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x,
                    Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x),
            floors[randomPosition].position.y);
        mo.IndexOnFloor = randomPosition;
        mo.Direction = Vector2.left;
        allMovingObjects.Add(mo.gameObject);

        mo = Instantiate(hole).GetComponent<MovingObjects>();
        mo.transform.position = new Vector2(Random.Range(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x,
                    Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x),
            floors[randomPosition].position.y);
        mo.IndexOnFloor = randomPosition;
        mo.Direction = Vector2.right;
        allMovingObjects.Add(mo.gameObject);
    }
    #endregion

    #region Functions For Levels
    public void NextLevel()
    {
        uiControl.ShowLinesContainer();
        messageSystem.NextLine(messageSystem.MessageData.Lines[actualLevel]);
        character.gameObject.SetActive(false);
    }

    public void ResetLevel()
    {
        foreach (GameObject item in allMovingObjects)
        {
            item.GetComponent<MovingObjects>().StopAllCoroutines();
            Destroy(item);
        }
        allMovingObjects.Clear();
        FirstTwoHoles();
        actualLevel++;
        scoreAdded += 5;
        if (actualLevel < messageSystem.MessageData.Lines.Count - 1)
        {
            for (int i = 0; i < actualLevel; i++)
            {
                CreateHoleOrMonster(1,i);
            }
        }
        else
        {
            //send Credits
        }
        uiControl.ShowLinesContainer();
        character.transform.position = initialPosition.position;
        character.gameObject.SetActive(true);
        character.Paralized = false;
        character.GetComponent<BoxCollider2D>().isTrigger = false;
        character.GetComponent<Rigidbody2D>().simulated = true;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(1);
    }
    #endregion

    #region Re-Size Floor
    private void ReSize()
    {
        float scale = Mathf.Abs(Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x - Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x);
        foreach (Transform item in floors)
        {
            item.localScale = new Vector2(scale *0.45f, item.localScale.y);
        }
        firstFloor.localScale = new Vector2(scale * 0.45f, firstFloor.localScale.y);
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

    #region Gets And Sets For Score
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int ScoreAdded
    {
        get
        {
            return scoreAdded;
        }
    }
    #endregion

    #region Gets And Sets For sounds
    public AudioSource WritingSound
    {
        get
        {
            return writingSound;
        }
    }
    #endregion
}
