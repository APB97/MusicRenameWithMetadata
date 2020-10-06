using System.Collections.Generic;
using JsonStructures;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    [RequireComponent(typeof(IActionsDropdowns), typeof(IParametersManager), typeof(IActionsArray))]
    public class ActionVisualizer : MonoBehaviour, ISelectedAction, IActionVisualizer
    {
        [SerializeField] private Transform actionsListingParent;
        [SerializeField] private Button buttonTemplate;
        [SerializeField] private Button buttonUpdate;
        [SerializeField] private Button buttonRemove;
        private IActionsDropdowns _actionsDropdowns;
        private IParametersManager _parametersManager;
        private IActionsArray _actions;

        private Button _button;

        public int SelectedIndex { get; private set; }

        private void Awake()
        {
            _actionsDropdowns = GetComponent<IActionsDropdowns>();
            _parametersManager = GetComponent<IParametersManager>();
            _actions = GetComponent<IActionsArray>();
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
                    SelectForUpdate(index - 1, btn);
                    ShowButtonsForSelection();
                    return;
                }
            });
        }

        public void ShowButtonsForSelection(bool show = true)
        {
            buttonUpdate.gameObject.SetActive(show);
            buttonRemove.gameObject.SetActive(show);
        }

        public void SelectForUpdate(int indexOfAdded, Button button)
        {
            SelectedIndex = indexOfAdded;
            _button = button;

            var definition = _actions[indexOfAdded];
            _actionsDropdowns.DropdownClass.value = _actionsDropdowns.DropdownClass.options.FindIndex(data => data.text == definition.ActionClass);
            _actionsDropdowns.DropdownAction.value = _actionsDropdowns.DropdownAction.options.FindIndex(data => data.text == definition.ActionName);

            _parametersManager.ClearParams();
            if (definition.ActionParameters == null) return;
            foreach (string parameter in definition.ActionParameters)
                _parametersManager.AddParam(parameter);
        }

        public void UpdateActionVisual()
        {
            string actionClass = _actionsDropdowns.DropdownClass.options[_actionsDropdowns.DropdownClass.value].text;
            string actionName = _actionsDropdowns.DropdownAction.options[_actionsDropdowns.DropdownAction.value].text;
            string[] parameters = _actions[SelectedIndex].ActionParameters;
            _button.GetComponentInChildren<Text>().text = FormatAction.ActionToString(actionClass, actionName, parameters);
        }
    
        public void DestroyActionVisual()
        {
            Destroy(_button.gameObject);
        }
    }
}