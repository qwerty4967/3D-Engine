using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using _3D_Engine;
using System.Collections.Specialized;

namespace _3D_Engine
{
    class SceneReader : SceneReaderModule{
        internal override object run()
        {
            scene.addObject(Templates[0], new Vector3(0, 0, 0), 1.0)
            return (base.run());
        }
    }
}
