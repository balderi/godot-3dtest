using Godot;
using System;

public partial class CameraRig : Node3D
{
	public SubViewport BgView, FgView;
	public Camera3D BgCam, FgCam;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BgView = GetNode<SubViewport>("BaseCam/BGviewContainer/BGview");
        FgView = GetNode<SubViewport>("BaseCam/FGviewContainer/FGview");

        BgCam = GetNode<Camera3D>("BaseCam/BGviewContainer/BGview/BackgroundCam");
        FgCam = GetNode<Camera3D>("BaseCam/FGviewContainer/FGview/ForegroundCam");

        GameManager.SetBaseCam(BgCam);

        Resize();
    }

	void Resize()
	{
		BgView.Size = DisplayServer.WindowGetSize();
        FgView.Size = DisplayServer.WindowGetSize();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var camTransform = GameManager.Player.CameraPoint.GlobalTransform;

		BgCam.GlobalTransform = camTransform;
		FgCam.GlobalTransform = camTransform;
    }
}
