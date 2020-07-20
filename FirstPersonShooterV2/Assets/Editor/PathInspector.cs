using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PathModel))]
public class PathInspector : Editor
{
    private void OnSceneGUI()
    {
        PathModel pathModel = target as PathModel;

        Vector3[] points = pathModel.GetPoints();

        Handles.color = Color.red;

        Handles.DrawAAPolyLine(10f, points);

        for (int i = 0; i < (pathModel.Loop ? points.Length - 1 : points.Length); i++)
        {
            EditorGUI.BeginChangeCheck();
            points[i] = Handles.DoPositionHandle(points[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(pathModel, "Move Point");
                EditorUtility.SetDirty(pathModel);
                pathModel.Points[i].position = points[i];
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PathModel pathModel = target as PathModel;
        if (GUILayout.Button("Add Point"))
        {
            pathModel.AddPoint();
        }
    }
}
