using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum Languages
{
    ENGLISH = 0,
    HINDI = 1,
    MARATHI = 2
}

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    [SerializeField]
    private List<LocalizedComponent> localizedComponents = new List<LocalizedComponent>();
    [SerializeField]
    private Languages currentLanguage;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Init(Languages currentLanguage)
    {
        this.currentLanguage = currentLanguage;
        SetLanguage(currentLanguage);
    }

    public void SetLanguage(Languages language)
    {
        this.currentLanguage = language;
        for (int i = 0; i < localizedComponents.Count; i++)
        {
            localizedComponents[i].SetLanguage(language);
        }
    }

    public void AddLComponent(LocalizedComponent l)
    {
        if (!localizedComponents.Contains(l))
            localizedComponents.Add(l);

        l.SetLanguage(currentLanguage);
    }

    public void RemoveLComponent(LocalizedComponent l)
    {
        if (localizedComponents.Contains(l))
            localizedComponents.Remove(l);
    }

    private void OnSceneLoaded(Scene s, LoadSceneMode l)
    {
        localizedComponents.Clear();
        GameObject[] parents = SceneManager.GetActiveScene().GetRootGameObjects();

        for (int i = 0; i < parents.Length; i++)
        {
            Transform parent = parents[i].transform;
            localizedComponents.AddRange(parent.GetComponentsInChildren<LocalizedComponent>(true));
        }

        SetLanguage(currentLanguage);
    }
}
