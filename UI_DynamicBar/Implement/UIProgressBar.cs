using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AngusChanToolket.UI_DynamicBar
{

    public class UIProgressBar : MonoBehaviour, IUIDynamicBar
    {
        [SerializeField] RectTransform healthLine;
        [SerializeField] Vector3 offset;

        RectTransform rect;

        void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        void IUIDynamicBar.SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        void IUIDynamicBar.SetProgress(float progress)
        {
            healthLine.localScale = new Vector3(progress, 1, 1);
        }
        void IUIDynamicBar.SetPosition(float x, float y, float z)
        {
            rect.position = new Vector3(x, y, z) + offset;
        }

        // Camera camera;


        // HealthSystem target;

        // Action<HealthSystem> disableHandler;

        // void Update()
        // {
        //     healthLine.localScale = new Vector3(target.ratio, 1, 1);
        //     rect.position = camera.WorldToScreenPoint(targetTransform.position) + offset;

        //     if(target == null || target.healthPoint <= 0)
        //     {
        //         Deactive();
        //     }
        // }

        // public void Initial(Camera camera, Action<HealthSystem> disableHandler)
        // {
        //     this.camera = camera;
        //     this.rect = GetComponent<RectTransform>();
        //     this.disableHandler = disableHandler;

        //     gameObject.SetActive(false);
        // }
        // public void Active(HealthSystem target)
        // {
        //     this.target = target;
        //     this.targetTransform = target.transform;

        //     gameObject.SetActive(true);
        // }
        // void Deactive()
        // {
        //     disableHandler.Invoke(target);

        //     gameObject.SetActive(false);

        // }
    }

}