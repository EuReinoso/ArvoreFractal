using System.Text.RegularExpressions;
using System;

namespace MgEngine.Util
{
    public static class MgMath
    {
        public static float ToRadians(float angleDegrees)
        {
            return (float)(angleDegrees * (Math.PI / 180));
        }
        public static float ToDegrees(float angleRadians)
        {
            return (float)(angleRadians / (Math.PI / 180));
        }
    }
}
