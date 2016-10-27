using UnityEngine;

public static class Layers
{
    public static int Default = LayerMask.NameToLayer("Default");
    public static int Destroyers = LayerMask.NameToLayer("Destroyers");
    public static int IgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
    public static int Terrain = LayerMask.NameToLayer("Terrain");
    public static int Scenery = LayerMask.NameToLayer("Scenery");
    public static int Player = LayerMask.NameToLayer("Player");
}