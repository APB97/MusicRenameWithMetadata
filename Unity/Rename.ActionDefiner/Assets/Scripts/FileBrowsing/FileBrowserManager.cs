using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.Events;
using UnityEvents;

namespace FileBrowsing
{
    public class FileBrowserManager : MonoBehaviour
    {
        [SerializeField] private StringEvent onWantsToSaveAt;
        [SerializeField] private StringEvent onWantsToLoad;

        [SerializeField] private UnityEvent onBrowserClosed;
        private readonly FileBrowser.Filter _filters = new FileBrowser.Filter("JavaScript Object Notation", ".json");

        public void ShowSaveDialog()
        {
            FileBrowser.SetFilters(false, _filters);
            FileBrowser.ShowSaveDialog(OnSaveSuccess, OnCancel);
        }

        private void OnCancel()
        {
            FileBrowser.HideDialog();
            onBrowserClosed.Invoke();
        }

        private void OnSaveSuccess(string[] paths)
        {
            if (paths.Length != 1)
                return;
            onWantsToSaveAt.Invoke(paths[0]);
            onBrowserClosed.Invoke();
        }

        public void ShowLoadDialog()
        {
            FileBrowser.SetFilters(false, _filters);
            FileBrowser.ShowLoadDialog(OnLoadSuccess, OnCancel);
        }

        private void OnLoadSuccess(string[] paths)
        {
            if (paths.Length != 1)
                return;
            onWantsToLoad.Invoke(paths[0]);
            onBrowserClosed.Invoke();
        }
    }
}