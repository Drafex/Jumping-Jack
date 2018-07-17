﻿using System.Collections;
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
    [SerializeField]
    private GameObject monster;
    private List<Vector2> directions = new List<Vector2>();
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

        character = Instantiate(prfCharacter, initialPosition.position, Quaternion.identity)
            .GetComponent<Character>();

        directions.Add(Vector2.left);
        directions.Add(Vector2.right);
        FirstTwoHoles();
        scoreAdded = 5;
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
                allMovingObjects.Add(mo.gameObject);
                break;

            case 1:
                mo = Instantiate(monster).GetComponent<MovingObjects>();
                mo.transform.position = new Vector2(mo.transform.position.x,
                    floors[randomPosition].position.y + mo.InitialHigh);
                mo.IndexOnFloor = randomPosition;
                mo.Direction = (directions[Random.Range(0, directions.Count - 1)]);
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
        mo.transform.position = new Vector2(mo.transform.position.x,
            floors[randomPosition].position.y);
        mo.IndexOnFloor = randomPosition;
        mo.Direction = Vector2.left;
        allMovingObjects.Add(mo.gameObject);

        mo = Instantiate(hole).GetComponent<MovingObjects>();
        mo.transform.position = new Vector2(mo.transform.position.x,
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
        FirstTwoHoles();
        actualLevel++;
        scoreAdded += 5;
        if (actualLevel < messageSystem.MessageData.Lines.Count - 1)
        {
            for (int i = 0; i < actualLevel; i++)
            {
                CreateHoleOrMonster(1);
            }
        }
        else
        {
            //send Credits
        }
        uiControl.ShowLinesContainer();
        character.transform.position = initialPosition.position;
        character.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(1);
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
}
