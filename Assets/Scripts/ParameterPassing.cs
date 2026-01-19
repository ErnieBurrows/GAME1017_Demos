using UnityEngine;

public class ParameterPassing : MonoBehaviour
{
    void Start()
    {
        int valueNumber = 5;
        int refNumber = 5;
        int outNumber;

        Debug.Log("Initial Values:");
        Debug.Log($"valueNumber = {valueNumber}");
        Debug.Log($"refNumber = {refNumber}");

        PassByValue(valueNumber);
        Debug.Log($"After PassByValue: valueNumber = {valueNumber}");

        PassByReference(ref refNumber);
        Debug.Log($"After PassByReference: refNumber = {refNumber}");

        PassByOut(out outNumber);
        Debug.Log($"After PassByOut: outNumber = {outNumber}");
    }

    // Passed by VALUE (copy is made)
    void PassByValue(int number)
    {
        number += 10;
        Debug.Log($"Inside PassByValue: number = {number}");
    }

    // Passed by REFERENCE (original is modified)
    void PassByReference(ref int number)
    {
        number += 10;
        Debug.Log($"Inside PassByReference: number = {number}");
    }

    // Passed by OUT (must be assigned inside the method)
    void PassByOut(out int number)
    {
        number = 20;
        Debug.Log($"Inside PassByOut: number = {number}");
    }
}
