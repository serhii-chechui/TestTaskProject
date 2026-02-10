using UnityEngine;
using UnityEngine.UI;

namespace ProductMadness.TestTaskProject.UI.UI.Common.Scripts
{
    public class PopupButton : Button
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }
        #endif
        
    }
}