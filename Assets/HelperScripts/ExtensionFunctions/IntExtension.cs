using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtension 
{
    public static int mod(this int x, int m)
    {
        return (x % m + m) % m;
    }
}
