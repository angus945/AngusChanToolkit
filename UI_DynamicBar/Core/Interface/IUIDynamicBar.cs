using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngusChanToolket.UI_DynamicBar
{
    public interface IUIDynamicBar
    {
        void SetActive(bool active);
        void SetPosition(float x, float y, float z);
        void SetProgress(float progress);
    }

}