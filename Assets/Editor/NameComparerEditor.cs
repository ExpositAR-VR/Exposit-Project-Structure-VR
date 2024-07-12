using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectInformation))]
public class NameComparerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObjectInformation comparer = (ObjectInformation)target;

        // Get the GameObject's name
        string gameObjectName = comparer.gameObject.name;

        // Compare the names
        if ("Scene Static Object" == gameObjectName)
        {
            EditorGUILayout.HelpBox("Scene static objects are elements that remain fixed in the scene and do not move or change during gameplay. They serve as the static environment, providing structure and visual elements such as walls, terrain, trees, and other static scenery.", MessageType.Info);
        }
        if ("Intreactable Object" == gameObjectName)
        {
            EditorGUILayout.HelpBox("Interactable objects are game elements that players can interact with during gameplay. These objects are designed to respond to player actions, such as picking up, using, or manipulating. Examples include weapons, tools, buttons, levers, and interactive items in the game world.", MessageType.Info);
        }
        if ("UI Setup" == gameObjectName)
        {
            EditorGUILayout.HelpBox("The UI setup hierarchy is dedicated to managing the user interface elements of the game. This includes designing and organizing UI elements such as menus, buttons, health bars, score displays, tooltips, and other graphical user interface components to provide feedback and interaction to the player.", MessageType.Info);
        }
        if ("Light Setup" == gameObjectName)
        {
            EditorGUILayout.HelpBox("The light setup hierarchy contains components and settings related to scene lighting. It encompasses both baked lighting and real-time lighting setups, including directional lights, point lights, spotlights, light probes, and other lighting configurations to illuminate the scene and create ambiance.", MessageType.Info);
        }
        if ("Player Setup" == gameObjectName)
        {
            EditorGUILayout.HelpBox(" The player setup hierarchy is used to configure and manage player-related components and functionality. It includes setting up the XR origin for virtual reality experiences, configuring player controllers, input handling, camera setup, and other player-related systems.", MessageType.Info);
        }
        if ("Game Manager" == gameObjectName)
        {
            EditorGUILayout.HelpBox("The game manager hierarchy houses scripts and components responsible for managing various aspects of the game. This includes game state management, input handling, scene transitions, scoring, level loading, and other core game functionalities to ensure smooth gameplay flow.", MessageType.Info);
        }
        if ("Effects Manager" == gameObjectName)
        {
            EditorGUILayout.HelpBox("The effects manager hierarchy is dedicated to managing visual effects in the game. It includes components for controlling particle effects, shader effects, post-processing effects, and other special effects to enhance the visual experience of the game.", MessageType.Info);
        }
        if ("Audio Controller" == gameObjectName)
        {
            EditorGUILayout.HelpBox("The audio controller hierarchy is used to manage audio-related components and assets in the game. It includes audio sources, audio listeners, ambient sounds, sound effects, music tracks, and other audio-related elements to provide immersive audio feedback to players during gameplay.", MessageType.Info);
        }

    }
}
