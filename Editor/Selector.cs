using UnityEditor;

namespace Graphene.BehaviourTree
{
    static class Selector
    {
        [MenuItem("Packages/Graphene")]
        static void OpenPackageDirectory()
        {
            var path = "Packages/BehaviourTree-upm/package.json";
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(path);
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
}
