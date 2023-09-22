using Godot;
using System;

public partial class TestUI : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.SetLabel1(GetNode<Label>("VBoxContainer/Label"));
        GameManager.SetLabel2(GetNode<Label>("VBoxContainer/Label2"));
        GameManager.SetLabel3(GetNode<Label>("VBoxContainer/Label3"));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
