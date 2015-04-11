﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace MapEditorCore.Input.Trigger
{
    public sealed class KeyTrigger : ITrigger
    {
        private readonly Keys key;

        public KeyTrigger(Keys key)
        {
            this.key = key;
        }

        public int TriggerHash()
        {
            return (int) key;
        }

        public override bool Equals(object obj)
        {
            var trigger = obj as KeyTrigger;
            if (trigger == null) return false;

            return trigger.key == key;
        }

        public override int GetHashCode()
        {
            return ("key" + key).GetHashCode();
        }
    }
}
