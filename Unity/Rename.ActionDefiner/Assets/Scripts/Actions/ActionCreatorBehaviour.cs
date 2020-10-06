using System.Collections.Generic;
using JsonStructures;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    [RequireComponent(typeof(IActionVisualizer), typeof(ISelectedAction), typeof(IParametersManager))]
    public class ActionCreatorBehaviour : MonoBehaviour, IActionsArray
    {
        [SerializeField] private Text actionClass;
        [SerializeField] private Text actionName;
        [SerializeField] private InputField path;

        private ActionsCreator _creator;
    
        private IParametersManager _parametersManager;
        private IActionVisualizer _visualizer;
        private ISelectedAction _selectionInfo;

        private void Awake()
        {
            _creator = new ActionsCreator(new List<ActionDefinition>());
            _parametersManager = GetComponent<IParametersManager>();
            _visualizer = GetComponent<IActionVisualizer>();
            _selectionInfo = GetComponent<ISelectedAction>();
        }

        public void Load(string filePath)
        {
            foreach (ActionDefinition definition in _creator.LoadFile(filePath))
            {
                _visualizer.AddVisualFor(definition);
            }
        }
    
        public void Add()
        {
            var parameters = _parametersManager.PopAllParameters();
            ActionDefinition definition = _creator.Add(actionClass.text, actionName.text, parameters.ToArray());
            _visualizer.AddVisualFor(definition);
        }

        public void CreateFile(string filepath)
        {
            _creator.CreateFile(filepath);
        }
    
        public void CreateFile()
        {
            _creator.CreateFile(path.text);
        }

        public void UpdateCurrentElement()
        {
            var parameters = _parametersManager.PopAllParameters();
            _creator.UpdateAt(_selectionInfo.SelectedIndex, actionClass.text, actionName.text, parameters.ToArray());
        }

        public void RemoveCurrentElement()
        {
            _creator.RemoveAt(_selectionInfo.SelectedIndex);
            _parametersManager.ClearParams();
        }

        public ActionDefinition this[int index] => _creator[index];
    }
}