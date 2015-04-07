﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Components
{
    public sealed class ComponentCollection
    {
        #region Fields
        private readonly List<EditorComponent> components;
        #endregion

        #region Properties
        public IEnumerable<EditorComponent> Components
        {
            get
            {
                return components;
            }
        }
        #endregion

        public ComponentCollection()
        {
            components = new List<EditorComponent>();
        }

        public void AddComponent(EditorComponent component)
        {
            components.Add(component);

            components.Sort((a, b) =>
            {
                if (a.DrawOrder == b.DrawOrder) return 0;
                if (a.DrawOrder > b.DrawOrder) return -1;

                return 1;
            });
        }
        public void RemoveComponent(EditorComponent component)
        {
            components.Remove(component);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++) components[i].Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++) components[i].Draw(spriteBatch);
        }
    }
}
