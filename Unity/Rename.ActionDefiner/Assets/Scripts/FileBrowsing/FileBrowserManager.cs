using SimpleFileBrowser;
using UnityEngine;
using UnityEvents;

namespace FileBrowsing
{
    public class FileBrowserManager : MonoBehaviour
    {
        [SerializeField] private StringEvent onWantsToSaveAt;

        public void ShowSaveDialog()
        {
            FileBrowser.SetFilters(false, new FileBrowser.Filter("JavaScript Object Notation", ".json"));
            FileBrowser.ShowSaveDialog(OnSaveSuccess, OnSaveCancel);
        }

        private void OnSaveCancel()
        {
            FileBrowser.HideDialog();
        }

        private void OnSaveSuccess(string[] paths)
        {
            if (paths.Length != 1)
                return;
            onWantsToSaveAt.Invoke(paths[0]);
        }
    }
}