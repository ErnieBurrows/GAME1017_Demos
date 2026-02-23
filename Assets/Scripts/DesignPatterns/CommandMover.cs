using System.Collections.Generic;
using UnityEngine;

public class CommandMover : MonoBehaviour
{
    public float step = 1f;

    private readonly Stack<ICommand> undo = new();
    private readonly Stack<ICommand> redo = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) Execute(new MoveCommand(transform, Vector3.forward * step));
        if (Input.GetKeyDown(KeyCode.S)) Execute(new MoveCommand(transform, Vector3.back * step));
        if (Input.GetKeyDown(KeyCode.A)) Execute(new MoveCommand(transform, Vector3.left * step));
        if (Input.GetKeyDown(KeyCode.D)) Execute(new MoveCommand(transform, Vector3.right * step));

        if (Input.GetKeyDown(KeyCode.Z)) Undo();
        if (Input.GetKeyDown(KeyCode.Y)) Redo();
    }

    private void Execute(ICommand cmd)
    {
        cmd.Execute();
        undo.Push(cmd);
        redo.Clear();
    }

    private void Undo()
    {
        if (undo.Count == 0) return;
        var cmd = undo.Pop();
        cmd.Undo();
        redo.Push(cmd);
    }

    private void Redo()
    {
        if (redo.Count == 0) return;
        var cmd = redo.Pop();
        cmd.Execute();
        undo.Push(cmd);
    }
}

public interface ICommand
{
    void Execute();
    void Undo();
}

public class MoveCommand : ICommand
{
    private readonly Transform target;
    private readonly Vector3 delta;

    public MoveCommand(Transform target, Vector3 delta)
    {
        this.target = target;
        this.delta = delta;
    }

    public void Execute() => target.position += delta;
    public void Undo() => target.position -= delta;
}