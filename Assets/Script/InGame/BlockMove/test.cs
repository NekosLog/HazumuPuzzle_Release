using UnityEngine;

public class ReflectionCalculator : MonoBehaviour
{
    public Vector3 incidentVector; // 入射ベクトル
    public Vector3 surfaceNormal;  // 面の法線ベクトル

    void Start()
    {
        // 入射ベクトルと面の法線ベクトルから反射ベクトルを計算
        Vector3 reflectionVector = CalculateReflectionVector(incidentVector, surfaceNormal);

        Debug.Log("Reflection Vector: " + reflectionVector);
    }

    // 反射ベクトルを計算する関数
    Vector3 CalculateReflectionVector(Vector3 incident, Vector3 normal)
    {
        // 入射ベクトルと法線ベクトルの内積を計算
        float dotProduct = Vector3.Dot(incident, normal);

        // 反射ベクトルを計算
        Vector3 reflection = incident - 2 * dotProduct * normal;

        return reflection;
    }
}