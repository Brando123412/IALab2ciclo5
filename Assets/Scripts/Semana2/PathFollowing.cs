using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypePath { Comer, Jugar, Banno, Dormir }

[System.Serializable]
public class DataPath
{
    public TypePath type;
    public Transform[] paths;
    public bool IsDrawGizmo = false;
    public Color ColorPath;
    public Color ColorCurrentPoint;
    public DataPath()
    {
    }

}

public class PathFollowing : MonoBehaviour
{
    public List<DataPath> datapaths = new List<DataPath>();

    Dictionary<TypePath, Transform[]> dictPath = new Dictionary<TypePath, Transform[]>();
    [SerializeField]
    TypePath currentPath;
    public Transform currentPoint;
    public int indexcurrentPoint;
    public float stopdistance;

    #region Gismoz
    public bool DrawAllPaths;
    public bool IsDrawGizmo = false;



    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var path in datapaths)
        {

            dictPath.Add(path.type, path.paths);
        }

        foreach (var elemt in datapaths)
        {

            foreach (Transform t in elemt.paths)
            {
                Debug.Log("Key:" + elemt.type + " -> Value: " + t.position);

            }
        }
    }
    public Transform[] GetPaths(TypePath type)
    {
        return dictPath[type];
    }
    public void ChangePath(TypePath type)
    {
        currentPath = type;
        indexcurrentPoint = 0;
        currentPoint = GetPaths(currentPath)[indexcurrentPoint];

    }
    public Transform NextCurrentPointPath()
    {


        float distance = (transform.position - currentPoint.position).magnitude;
        //Debug.Log("distance: " + distance);
        if (distance < stopdistance)
        {
            indexcurrentPoint++;
            indexcurrentPoint = Math.Clamp(indexcurrentPoint, 0, GetPaths(currentPath).Length - 1);
            currentPoint = GetPaths(currentPath)[indexcurrentPoint];

        }
        return currentPoint;
    }
    private void OnDrawGizmos()
    {
        if (DrawAllPaths)
        {
            foreach (var item in datapaths)
            {
                if (!item.IsDrawGizmo) return;

                if (currentPoint != null)
                {
                    Gizmos.color = item.ColorCurrentPoint;
                    Gizmos.DrawSphere(currentPoint.position, 0.8f);
                }
                Transform[] array = GetPaths(item.type);
                if (array.Length == 0) return;
                Gizmos.color = item.ColorPath;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    Gizmos.DrawLine(array[i].position, array[i + 1].position);
                    Gizmos.DrawSphere(array[i].position, 0.8f);
                }
                Gizmos.DrawSphere(array[array.Length - 1].position, 0.8f);
            }

        }
        else
        {
            if (currentPoint != null)
            {

                Gizmos.color = GetColorCurrentPath();
                Gizmos.DrawSphere(currentPoint.position, 0.8f);
            }
            Transform[] array = GetPaths(currentPath);
            if (array.Length == 0) return;
            Gizmos.color = GetColorPath();

            for (int i = 0; i < array.Length - 1; i++)
            {
                Gizmos.DrawLine(array[i].position, array[i + 1].position);
                Gizmos.DrawSphere(array[i].position, 0.8f);
            }
            Gizmos.DrawSphere(array[array.Length - 1].position, 0.8f);
        }

        //Gizmos.DrawLine(array[array.Length - 1].position, array[0].position);
    }
    Color GetColorCurrentPath()
    {
        foreach (var item in datapaths)
        {
            if (item.type == currentPath)
                return item.ColorCurrentPoint;
        }
        return Color.black;
    }
    Color GetColorPath()
    {
        foreach (var item in datapaths)
        {
            if (item.type == currentPath)
                return item.ColorPath;
        }
        return Color.black;
    }
}