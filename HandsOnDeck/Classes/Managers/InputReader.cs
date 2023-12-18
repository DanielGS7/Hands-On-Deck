using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public class InputReader
{
    private static InputReader instance;
    private KeyboardState keyboardState;

    private Keys select = Keys.Enter;
    private Keys escape = Keys.Escape;
    private Keys left = Keys.Left;
    private Keys right = Keys.Right;
    public Keys up = Keys.Up;
    private Keys down = Keys.Down;
    private Keys water = Keys.Q;
    private Keys earth = Keys.S;
    private Keys fire = Keys.D;
    private Keys air = Keys.F;
    private Keys hold = Keys.LeftShift;

    private Dictionary<string, bool> currentInput = new Dictionary<string, bool>();

    public static InputReader GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputReader();
            }
            return instance;
        }
    }

    public Dictionary<string, bool> CurrentInput
    {
        get
        {
            return currentInput;
        }
    }

    private InputReader()
    {
        // Private constructor to prevent instantiation
    }

    public void Initialize()
    {
        UpdateInput();
    }

    public void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        keyboardState = Keyboard.GetState();
        CurrentInput.Clear();
        CurrentInput.Add("select", keyboardState.IsKeyDown(select));
        CurrentInput.Add("escape", keyboardState.IsKeyDown(escape));
        CurrentInput.Add("left", keyboardState.IsKeyDown(left));
        CurrentInput.Add("right", keyboardState.IsKeyDown(right));
        CurrentInput.Add("up", keyboardState.IsKeyDown(up));
        CurrentInput.Add("down", keyboardState.IsKeyDown(down));
        CurrentInput.Add("water", keyboardState.IsKeyDown(water));
        CurrentInput.Add("earth", keyboardState.IsKeyDown(earth));
        CurrentInput.Add("fire", keyboardState.IsKeyDown(fire));
        CurrentInput.Add("air", keyboardState.IsKeyDown(air));
        CurrentInput.Add("hold", keyboardState.IsKeyDown(hold));
    }
}