using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcRotation : MonoBehaviour
{
    static float direction;//to calculate the angle of rotation
    static float ZCoordinate;//the angle of rotation

    public static Quaternion CalculateRotation(Hero targetToAtack)
    {
        Vector3 targetPosition = targetToAtack.transform.position;//target coordinates
        Hero currentAtacker = BattleController.currentAtacker;//to access the coordinates of the attacking hero
        Vector3 atackerPosition = currentAtacker.transform.position;//coordinates of the attacking hero
        ZCoordinate = GetAngle(targetPosition, atackerPosition);//Calculation of the rotation angle
        Quaternion rotation = Quaternion.EulerAngles(0, 0, ZCoordinate);//to transform into a Quaternion
        return rotation;
    }
    private static float GetAngle(Vector3 targetPosition, Vector3 atackerPosition)
    {
        //calculates the arc tangent of the rotation angle
        direction = Mathf.Atan((targetPosition.y - atackerPosition.y) /
                               (targetPosition.x - atackerPosition.x));

        if (targetPosition.x > atackerPosition.x)//if the target is to the right of the hero
        {
            ZCoordinate = direction;
        }
        else
        {
            ZCoordinate = Mathf.PI + direction;
        }
        return ZCoordinate;
    }
}
