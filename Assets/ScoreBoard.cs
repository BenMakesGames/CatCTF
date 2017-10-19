using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {

    public Flag[] Flags;

    public int PointsToWin = 5;

    public int BluePoints = 0;
    public int RedPoints = 0;

    public void PointForBlue()
    {
        BluePoints++;
    }

    public void PointForRed()
    {
        RedPoints++;
    }

    private void FixedUpdate()
    {
        foreach(Flag f in Flags)
        {
            if (f.IsGoingHome) continue;

            // blue team's side is at X > 0
            if (f.transform.position.x > 0 && f.Team == Team.Red)
            {
                PointForBlue();
                f.ReturnHome();
            }
            // red team's side is at X <= 0
            else if (f.transform.position.x <= 0 && f.Team == Team.Blue)
            {
                PointForRed();
                f.ReturnHome();
            }
        }
    }
}
