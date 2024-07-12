using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HierarchyImporter : Editor
{
    private static string[] objectNames = new string[]
      {
        "------------------------------------------------------------------------------ ",
        "Scene Static Object",
        "------------------------------------------------------------------------------ ",
        "Intreactable Object",
        "------------------------------------------------------------------------------ ",
        "UI Setup",
        "------------------------------------------------------------------------------ ",
        "Light Setup",
        "------------------------------------------------------------------------------ ",
        "Game Manager",
        "------------------------------------------------------------------------------ ",
        "Effects Manager",
        "------------------------------------------------------------------------------ ",
        "Audio Controller",
        "------------------------------------------------------------------------------ ",
        "Extra Objects",
        "------------------------------------------------------------------------------ ",
        "Player Setup",
        "------------------------------------------------------------------------------ "
      };

    [MenuItem("Exposit/Import Hierarchy")]
    public static void ImportHierarchy()
    {
        for (int i = 0; i < objectNames.Length; i++)
        {
            GameObject newObject = new GameObject(objectNames[i]);

            if (i == 5)
            {
                GameObject childObject = new GameObject("Static UI");
                GameObject childObject2 = new GameObject("Intractable UI");
                childObject.transform.parent = newObject.transform;
                childObject2.transform.parent = newObject.transform;
            }
            if (i == 7)
            {
                GameObject childObject = new GameObject("Bake Lights");
                GameObject childObject2 = new GameObject("Realtime Lights");
                childObject.transform.parent = newObject.transform;
                childObject2.transform.parent = newObject.transform;
            }
        }

    }
    /*
    

     [MenuItem("Exposit/Make 3D intreactable")]
     public static void MakeThreeDIntractable()
     {
         GameObject selectedObject = Selection.activeGameObject;
         if (selectedObject != null)
         {
             // Add split merge script
             if (selectedObject.GetComponent<BoxCollider>() == null)
             {
                 selectedObject.AddComponent<BoxCollider>();
             }

             // Add label manger
             if (selectedObject.GetComponent<ExpositGrabInteractable>() == null)
             {
                 selectedObject.AddComponent<ExpositGrabInteractable>();
             }

             EditorUtility.SetDirty(selectedObject);
         }
         else
         {
             EditorUtility.DisplayDialog("No Object Selected", "Please select an object in the hierarchy to make it grabbable.", "OK");
         }

     }
    */
}
