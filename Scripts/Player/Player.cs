using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody3D
{
	public const float Acceleration = 0.1f;
	public float Speed = 0.0f;
    public const float WalkSpeed = 1.5f;
    public const float RunSpeed = 5.0f;
    public const float MaxSpeed = 5.0f;
    public const float JumpVelocity = 4.5f;
	public bool IsWalking, IsRunning, IsJumping, IsJumpAnimation;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public Node3D Visuals;

	public Node3D CameraPoint;

	public AnimationTree AnimationTree;

    public override void _Ready()
    {
        base._Ready();

		Visuals = GetNode<Node3D>("Visuals");
		CameraPoint = GetNode<Node3D>("CameraPoint");
		AnimationTree = GetNode<AnimationTree>("Visuals/AnimationTree");

		GameManager.SetPlayer(this);
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		IsWalking = false;
		IsRunning = false;
		IsJumping = false;
		
		if (Input.IsActionPressed("Run"))
        {
            IsRunning = true;
			IsWalking = false;
        }
        else
        {
            IsRunning = false;
            IsWalking = true;
        }

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y -= gravity * (float)delta;
            IsJumping = true;
            IsWalking = false;
            IsRunning = false;
        }
        else
        {
            IsJumpAnimation = false;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("Jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
            IsJumping = true;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("Left", "Right", "Forward", "Backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero && !IsJumping)
		{
			if(IsWalking)
            {
                velocity.X = direction.X * WalkSpeed;
                velocity.Z = direction.Z * WalkSpeed;
            }
			else if(IsRunning)
            {
                velocity.X = direction.X * RunSpeed;
                velocity.Z = direction.Z * RunSpeed;
            }

            Visuals.LookAt(direction + Position);
		}
		else if(IsJumping)
		{
			//do nothing
		}
		else
		{
			IsWalking = false;
			IsRunning = false;

			velocity.X = 0;
			velocity.Z = 0;
		}

		Velocity = velocity;
		var normV = Velocity.Normalized();

        GameManager.Label1.Text = $"Velocity = X:{Velocity.X}, Z:{Velocity.Z}";

        GameManager.Label2.Text = $"Velocity(normalized) = X:{normV.X}, Z:{normV.Z}";

		var v = Mathf.Abs(normV.X) + Mathf.Abs(normV.Z);

        GameManager.Label3.Text = $"IsWalking: {IsWalking}, IsRunning: {IsRunning}, IsJumping: {IsJumping}";

        if(IsJumpAnimation)
        {
            //do nothing
        }
        else if(IsJumping)
        {
            AnimationTree.Set("parameters/Locomotion/blend_position", 0);
            AnimationTree.Set("parameters/Jump/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
            IsJumpAnimation = true;
        }
        else
        {
            AnimationTree.Set("parameters/Jump/active", false);
            AnimationTree.Set("parameters/Locomotion/blend_position", IsWalking ? 0.5 : IsRunning ? 1 : 0);
        }

        MoveAndSlide();
	}
}
