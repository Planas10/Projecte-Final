//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameController : MonoBehaviour
//{
//    public List<Light> lights; // list of the four lights in order
//    public List<LightsController> panels; // list of the four panels in order

//    private int currentIndex = 0; // current index of the expected light color

//    // Update is called once per frame
//    void Update()
//    {
//        // check if the player has pressed the interaction key for the current panel
//        if (panels[currentIndex]. == KeyCode.E && Input.GetKeyDown(panels[currentIndex].interactionKey))
//        {
//            // check if the player has matched the expected light color
//            if (lights[currentIndex].color == panels[currentIndex].panelLight.color)
//            {
//                // increment the current index
//                currentIndex++;

//                // check if the player has matched all the colors
//                if (currentIndex == lights.Count)
//                {
//                    Debug.Log("You win!");
//                }
//            }
//            else
//            {
//                // reset the current index
//                currentIndex = 0;
//                Debug.Log("Wrong order!");
//            }
//        }
//    }
//}
