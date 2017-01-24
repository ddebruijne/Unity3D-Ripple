using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GridCubeMod {
    private float height = 0;
    private Color color = Color.black;

    public float Height {
        get { return height; }
        set { height = value; }
    }

    public Color Color {
        get { return color; }
        set { color = value; }
    }

    public void Combine(GridCubeMod mod) {
        Height += mod.Height;
        Color += mod.Color;
    }
}
