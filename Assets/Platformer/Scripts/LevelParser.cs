using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * This script is responsible for reading a level layout from a text file and constructing the level
 * in a Unity scene by instantiating block GameObjects. The level file should be placed in the
 * Resources folder, and each line in the file represents a row of blocks.
 *
 * WHAT YOU NEED TO DO:
 * 1. In the for loop that iterates over each character (i.e. letter) in the current row, determine
 *    which type of block to create based on the letter (e.g., use 'R' for rock, 'B' for brick, etc.).
 *
 * 2. Instantiate the correct prefab (rockPrefab, brickPrefab, questionBoxPrefab, stonePrefab) corresponding
 *    to the letter.
 *
 * 3. Calculate the position for the new block GameObject using the current row and column index.
 *    - You will likely need to maintain a separate column counter as you iterate through the characters.
 *
 * 4. Set the instantiated block’s parent to 'environmentRoot' to keep the hierarchy organized.
 *
 * ADDITIONAL NOTES:
 * - The level reloads when the player presses the 'R' key, which clears all blocks under levelRoot
 *   and then re-parses the level file.
 * - Ensure that the level file's name (without the extension) matches the 'filename' variable.
 *
 * By completing these TODOs, you will enable the level parser to dynamically create and position
 * the blocks based on the level file data.
 */


public class LevelParser : MonoBehaviour
{
    public TextAsset levelFile;
    public Transform levelRoot;

    [Header("Prefabs")]
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject goal;
    public GameObject water;
    void Start()
    {
        LoadLevel();
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
            ReloadLevel();
    }

    void LoadLevel()
    {
        // Push lines onto a stack so we can pop bottom-up rows. This is easy to reason
        //  about, but an index-based loop over the string array is faster.
        Stack<string> levelRows = new Stack<string>();

        foreach (string line in levelFile.text.Split('\n'))
            levelRows.Push(line);

        int row = 0;
        while (levelRows.Count > 0)
        {
            string rowString = levelRows.Pop();
            char[] rowChars = rowString.ToCharArray();
            
            for (var columnIndex = 0; columnIndex < rowChars.Length; columnIndex++)
            {
                var currentChar = rowChars[columnIndex];

                if (currentChar == 'x')
                {
                    Vector3 position = new Vector3(columnIndex, row, 0);
                    Transform rockInstance = Instantiate(rockPrefab).transform;
                    rockInstance.position = position;
                }
                if (currentChar == 'b')
                {
                    Vector3 positionBrick = new Vector3(columnIndex, row, 0);
                    Transform brickInstance = Instantiate(brickPrefab).transform;
                    brickInstance.position = positionBrick;
                }
                if(currentChar == 's')
                {
                    Vector3 positionStone = new Vector3(columnIndex, row, 0);
                    Transform stoneInstance = Instantiate(stonePrefab).transform;
                    stoneInstance.position = positionStone;
                }
                if(currentChar == '?') 
                {
                    Vector3 positionQuestion = new Vector3(columnIndex, row, 0);
                    Transform questionInstance = Instantiate(questionBoxPrefab).transform;
                    questionInstance.position = positionQuestion;
                }
                if(currentChar == 'g')
                {
                    Vector3 positionQuestion = new Vector3(columnIndex, row, 0);
                    Transform questionInstance = Instantiate(goal).transform;
                    questionInstance.position = positionQuestion;
                }

                if (currentChar == 'w')
                {
                    Vector3 positionQuestion = new Vector3(columnIndex, row, 0);
                    Transform questionInstance = Instantiate(water).transform;
                    questionInstance.position = positionQuestion;
                }




                // Todo - Instantiate a new GameObject that matches the type specified by the character
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot

            }

            row++;
        }
    }

    // --------------------------------------------------------------------------
    void ReloadLevel()
    {
        foreach (Transform child in levelRoot)
           Destroy(child.gameObject);
        
        LoadLevel();
    }
}



