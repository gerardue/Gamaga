using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions
{
    public static int bit(this int i) { return i == 0 ? 0 : 1; }
    public static int bitNeg(this int i) { return i == 0 ? 1 : 0; }
}
