using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class TimedMessage : MonoBehaviour
    {
        [SerializeField]
        float timeSpan = 1;

        float timeLeft;

        public void ShowUp()
        {
            timeLeft = timeSpan;
            gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}