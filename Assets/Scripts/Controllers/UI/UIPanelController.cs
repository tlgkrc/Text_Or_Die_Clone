using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers.UI
{
    public class UIPanelController 
    {
        #region Self Variables

        #region Private Variables

        private readonly List<GameObject> _panels;

        #endregion

        #endregion
        public UIPanelController(ref List<GameObject> panels)
        {
            _panels = panels;
        }
        
        public void Execute(UIPanels panelParam,bool isOpen)
        {
            _panels[(int) panelParam].SetActive(isOpen);
        }
    }
}