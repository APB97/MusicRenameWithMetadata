using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    public class ParametersCreatorBehaviour : MonoBehaviour, IParametersManager
    {
        [SerializeField] private Transform layout;
        [SerializeField] private GameObject template;

        /// <summary>
        /// Add parameter template instance for UI, optionally setting its entered value.
        /// </summary>
        /// <param name="parameter">Text to set for parameter template instance, or null of not necessary.</param>
        public void AddParam(string parameter = null)
        {
            // Create and activate
            GameObject instance = Instantiate(template, layout);
            instance.SetActive(true);
        
            // Skip if no parameter value to set
            if (parameter == null) return;
        
            // Set input field's value to parameter
            instance.GetComponentInChildren<InputField>().text = parameter;
        }

        /// <summary>
        /// Clears parameters UI layout.
        /// </summary>
        public void ClearParams()
        {
            // Reverse iteration
            for (int i = layout.childCount - 1; i >= 0; i--)
            {
                // Destroy only active to keep the template
                var gameObj = layout.GetChild(i).gameObject;
                if (gameObj.activeSelf)
                    Destroy(gameObj);
            }
        }

        /// <summary>
        /// Removes parameters from visual layout and returns list of their values.
        /// </summary>
        /// <returns>List of parameters from removed visual representation.</returns>
        public List<string> PopAllParameters()
        {
            List<string> parameters = new List<string>();

            // Iterate backwards to avoid adjusting 'i' in loop body
            for (int i = layout.childCount - 1; i >= 0; i--)
            {
                Transform child = layout.GetChild(i);
            
                // Skip child when it is inactive
                if (!child.gameObject.activeSelf) continue;
            
                // Add child's text to parameters
                string text = child.GetComponentsInChildren<Text>().Last().text;
                parameters.Add(text);
            
                // Destroy child GameObject
                Destroy(child.gameObject);
            }
        
            // Reverse to get original order
            parameters.Reverse();
            return parameters;
        }
    }
}