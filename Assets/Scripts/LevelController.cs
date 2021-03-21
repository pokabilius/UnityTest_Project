using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Monster[] _monsters;

    [SerializeField] string _nextLevelName;
 
    void OnEnable() 
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead())
        {
            GoToNextLevel();
        }
    }

    bool MonstersAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    void GoToNextLevel()
    {
        Debug.Log("go to level" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

}
