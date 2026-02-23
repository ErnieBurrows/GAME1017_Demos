using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Drop this on an empty GameObject in a blank scene and press Play.
/// It builds a tiny "demo scene" at runtime showing:
/// - State Machine (Capsule: Idle/Move/Jump)
/// - Command Pattern (Cube: WASD moves with Undo/Redo)
/// - Observer Pattern (Health: H/J changes, UI + Console reacts)
/// - Flyweight Pattern (Bullets share shared data via factory)
public class PatternDemoScene : MonoBehaviour
{
    [Header("Optional: camera auto setup")]
    public bool autoSetupCamera = true;

    private Text instructionsText;

    private void Start()
    {
        if (autoSetupCamera) SetupCamera();

        CreateLighting();
        CreateGround();

        var ui = CreateUI();
        instructionsText = ui;

        // --- PATTERN OBJECTS ---
        CreateStateMachinePlayer(new Vector3(-6f, 1f, 0f));
        CreateCommandMover(new Vector3(-2f, 0.5f, 0f));
        CreateObserverHealthDemo(new Vector3(2f, 1f, 0f), ui);
        CreateFlyweightShooterDemo(new Vector3(6f, 1f, 0f));

        ui.text =
            "PATTERN DEMO SCENE\n\n" +
            "1) State Machine (Capsule on LEFT)\n" +
            "   - A/D to move, Space to jump\n\n" +
            "2) Command Pattern (Cube)\n" +
            "   - WASD to move in steps\n" +
            "   - Z = Undo, Y = Redo\n\n" +
            "3) Observer Pattern (Health)\n" +
            "   - H = take damage, J = heal\n" +
            "   - UI + Console updates via event\n\n" +
            "4) Flyweight Pattern (Shooter on RIGHT)\n" +
            "   - 1 = Pistol bullet, 2 = Shotgun bullet\n" +
            "   - Bullets share mesh/material/speed via Flyweight\n";
    }

    // -----------------------------
    // Scene Setup
    // -----------------------------
    private void SetupCamera()
    {
        var cam = Camera.main;
        if (!cam)
        {
            var go = new GameObject("Main Camera");
            cam = go.AddComponent<Camera>();
            go.tag = "MainCamera";
        }

        cam.transform.position = new Vector3(0f, 9f, -14f);
        cam.transform.rotation = Quaternion.Euler(25f, 0f, 0f);
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    private void CreateLighting()
    {
        if (FindFirstObjectByType<Light>() != null) return;

        var lightGO = new GameObject("Directional Light");
        var light = lightGO.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.0f;
        lightGO.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
    }

    private void CreateGround()
    {
        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.position = Vector3.zero;
        ground.transform.localScale = new Vector3(2.5f, 1f, 2.5f);
        ground.AddComponent<Rigidbody>().isKinematic = true;
    }

    private Text CreateUI()
    {
        var canvasGO = new GameObject("Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        var textGO = new GameObject("InstructionsText");
        textGO.transform.SetParent(canvasGO.transform, false);

        var text = textGO.AddComponent<Text>();
        text.fontSize = 18;
        text.alignment = TextAnchor.UpperLeft;
        text.color = Color.white;

        var rt = text.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0f, 1f);
        rt.anchorMax = new Vector2(0f, 1f);
        rt.pivot = new Vector2(0f, 1f);
        rt.anchoredPosition = new Vector2(12f, -12f);
        rt.sizeDelta = new Vector2(900f, 600f);

        // subtle background panel
        var panelGO = new GameObject("Panel");
        panelGO.transform.SetParent(canvasGO.transform, false);
        var panel = panelGO.AddComponent<Image>();
        panel.color = new Color(0f, 0f, 0f, 0.45f);
        var prt = panel.GetComponent<RectTransform>();
        prt.anchorMin = new Vector2(0f, 1f);
        prt.anchorMax = new Vector2(0f, 1f);
        prt.pivot = new Vector2(0f, 1f);
        prt.anchoredPosition = new Vector2(8f, -8f);
        prt.sizeDelta = new Vector2(920f, 620f);

        // Ensure panel behind text
        panelGO.transform.SetAsFirstSibling();
        textGO.transform.SetAsLastSibling();

        return text;
    }

    // -----------------------------
    // 1) State Machine Demo
    // -----------------------------
    private void CreateStateMachinePlayer(Vector3 pos)
    {
        var player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "StateMachine_Player";
        player.transform.position = pos;

        var rb = player.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        var sm = player.AddComponent<PlayerStateMachine>();
        sm.rb = rb;
        sm.moveSpeed = 5f;
        sm.jumpForce = 7f;

        // label
        CreateWorldLabel("State Machine\n(A/D + Space)", pos + new Vector3(0f, 2.2f, 0f));
    }

    // -----------------------------
    // 2) Command Pattern Demo
    // -----------------------------
    private void CreateCommandMover(Vector3 pos)
    {
        var mover = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mover.name = "CommandMover_Cube";
        mover.transform.position = pos;

        var cmd = mover.AddComponent<CommandMover>();
        cmd.step = 1f;

        CreateWorldLabel("Command\n(WASD, Z/Y)", pos + new Vector3(0f, 1.8f, 0f));
    }

    // -----------------------------
    // 3) Observer Pattern Demo
    // -----------------------------
    private void CreateObserverHealthDemo(Vector3 pos, Text uiText)
    {
        var subjectGO = new GameObject("Observer_HealthSubject");
        subjectGO.transform.position = pos;

        var subject = subjectGO.AddComponent<HealthSubject>();
        subject.maxHealth = 10;

        var obsGO = new GameObject("Observer_HealthObserver");
        var obs = obsGO.AddComponent<HealthTextObserver>();
        obs.health = subject;
        obs.uiText = uiText; // re-use the big UI text (it also logs to Console)

        CreateWorldLabel("Observer\n(H/J)", pos + new Vector3(0f, 1.8f, 0f));
    }

    // -----------------------------
    // 4) Flyweight Demo
    // -----------------------------
    private void CreateFlyweightShooterDemo(Vector3 pos)
    {
        // Shooter "stand"
        var shooter = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        shooter.name = "Flyweight_Shooter";
        shooter.transform.position = pos;
        shooter.transform.localScale = new Vector3(0.8f, 1.2f, 0.8f);

        // Factory
        var factoryGO = new GameObject("FlyweightFactory");
        var factory = factoryGO.AddComponent<BulletFlyweightFactory>();

        // Build flyweights at runtime (no inspector setup needed)
        factory.pistol = new BulletFlyweight()
        {
            mesh = GetPrimitiveMesh(PrimitiveType.Sphere),
            material = MakeMaterial(new Color(0.2f, 0.8f, 1f)),
            speed = 12f,
            damage = 1
        };

        factory.shotgun = new BulletFlyweight()
        {
            mesh = GetPrimitiveMesh(PrimitiveType.Cube),
            material = MakeMaterial(new Color(1f, 0.5f, 0.2f)),
            speed = 7f,
            damage = 2
        };

        var shooterComp = shooter.AddComponent<BulletShooter>();
        shooterComp.factory = factory;

        CreateWorldLabel("Flyweight\n(1/2 shoot)", pos + new Vector3(0f, 2.4f, 0f));
    }

    private Mesh GetPrimitiveMesh(PrimitiveType type)
    {
        var temp = GameObject.CreatePrimitive(type);
        var mesh = temp.GetComponent<MeshFilter>().sharedMesh;
        Destroy(temp);
        return mesh;
    }

    private Material MakeMaterial(Color color)
    {
        // Works in Built-in (Standard) and often in URP (Lit). If shader not found, Unity will fallback.
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (!shader) shader = Shader.Find("Standard");
        var mat = new Material(shader);
        if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", color); // URP Lit
        if (mat.HasProperty("_Color")) mat.color = color;                    // Standard
        return mat;
    }

    private void CreateWorldLabel(string text, Vector3 pos)
    {
        var go = new GameObject("Label");
        go.transform.position = pos;
        var tm = go.AddComponent<TextMesh>();
        tm.text = text;
        tm.characterSize = 0.12f;
        tm.fontSize = 60;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.alignment = TextAlignment.Center;
        tm.color = Color.white;
    }
}