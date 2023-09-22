using Godot;
using System;

public partial class GameManager : Node
{
    public static Player Player;
    public static Camera3D BaseCam;
    public static Label Label1, Label2, Label3;

    public static void SetPlayer(Player playerNode)
    {
        Player = playerNode;
    }
    public static void SetBaseCam(Camera3D camNode)
    {
        BaseCam = camNode;
    }

    public static void SetLabel1(Label label)
    {
        Label1 = label;
    }

    public static void SetLabel2(Label label)
    {
        Label2 = label;
    }

    public static void SetLabel3(Label label)
    {
        Label3 = label;
    }
}
