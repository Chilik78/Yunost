using UnityEngine;

namespace Player
{
    public static class MovementsHelpers
    {
        private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
    }
}

