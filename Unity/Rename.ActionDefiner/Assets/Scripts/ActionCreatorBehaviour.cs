using System.Collections.Generic;
using JsonStructures;
using UnityEngine;
using UnityEngine.UI;

public class ActionCreatorBehaviour : MonoBehaviour
{
    [SerializeField] private Text actionClass;
    [SerializeField] private Text actionName;
    [SerializeField] private InputField path;
    private ActionsCreator _creator;
    
    private void Awake()
    {
        _creator = new ActionsCreator(new List<ActionDefinition>());
    }

    public void Add()
    {
        _creator.Add(actionClass.text, actionName.text);
    }

    public void CreateFile()
    {
        _creator.CreateFile(path.text);
    }
}