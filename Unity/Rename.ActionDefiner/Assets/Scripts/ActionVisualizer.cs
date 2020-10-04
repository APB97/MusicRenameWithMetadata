using System.Collections.Generic;
using JsonStructures;
using UnityEngine;
using UnityEngine.UI;

public class ActionVisualizer : MonoBehaviour
{
    [SerializeField] private Transform actionsListingParent;
    [SerializeField] private Button buttonTemplate;
    private ActionCreatorBehaviour _actionCreatorBehaviour;

    private void Awake()
    {
        _actionCreatorBehaviour = GetComponent<ActionCreatorBehaviour>();
    }

    public void AddVisualFor(ActionDefinition definition)
    {
        AddVisualFor(definition.ActionClass, definition.ActionName, definition.ActionParameters);
    }

    private void AddVisualFor(string actionClassText, string actionNameText, IEnumerable<string> parameters)
    {
        var btn = Instantiate(buttonTemplate, actionsListingParent);
        btn.GetComponentInChildren<Text>().text = FormatAction.ActionToString(actionClassText, actionNameText, parameters);
        btn.gameObject.SetActive(true);
        btn.onClick.AddListener(() =>
        {
            for (int index = 1; index < actionsListingParent.childCount; index++)
            {
                if (actionsListingParent.GetChild(index) != btn.transform) continue;
                _actionCreatorBehaviour.SelectForUpdate(index - 1, btn);
                return;
            }
        });
    }
}