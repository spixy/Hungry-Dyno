using UnityEngine;

public static class SortingLayers
{ 
    public static int Default = LayerMask.NameToLayer("Default");
    public static int Sky = LayerMask.NameToLayer("Sky");
    public static int BackObjects = LayerMask.NameToLayer("BackObjects");
    public static int Terrain = LayerMask.NameToLayer("Terrain");
    public static int Player = LayerMask.NameToLayer("Player");
    public static int FrontObjects = LayerMask.NameToLayer("FrontObjects");
}
