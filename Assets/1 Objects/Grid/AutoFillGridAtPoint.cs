using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class AutoFillGridAtPoint : MonoBehaviour
    {
        private void Start()
        {
            GridManager.Instance.SetBlockedState(this.transform.position, true);
        }
    }
}
