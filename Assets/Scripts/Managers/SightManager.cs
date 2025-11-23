using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SightManager : MonoBehaviour
{
    [SerializeField] List<SightSerializable> sights;

    [Serializable]
    class SightSerializable
    {
        public CanvasGroup canvasGroup;
        public StarsController stars;
    }

    void Start()
    {
        for (int i = 1; i < sights.Count; i++)
        {
            sights[i].canvasGroup.blocksRaycasts = false;
        }
    }

    public void CheckPassed()
    {
        for (int i = 0; i < sights.Count; i++)
        {
            if (sights[i].stars.GetIsFull() && i + 1 != sights.Count)
                sights[i + 1].canvasGroup.blocksRaycasts = true;
        }
    }
}
