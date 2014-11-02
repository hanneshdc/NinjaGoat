using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NinjaGoat
{
    [StructLayout(LayoutKind.Sequential)]
    struct Vertex
    {
        public Vector3 Color;
        public Vector3 Position;
    }
}
