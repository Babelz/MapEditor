using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Components.EditorComponents
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

        #region Event handlers
        private void DrawOrder_Changed()
        {
            SortComponents();
        }
        #endregion

        private void SortComponents()
        {
            components.Sort((a, b) =>
            {
                if (a.DrawOrder == b.DrawOrder) return 0;
                if (a.DrawOrder > b.DrawOrder) return -1;

                return 1;
            });
        }
        public void AddComponent(EditorComponent component)
        {
            components.Add(component);

            SortComponents();

            component.DrawOrder.Changed += DrawOrder_Changed;
        }

        public void RemoveComponent(EditorComponent component)
        {
            components.Remove(component);

            // Remove events so component gets collected by GC.
            component.DrawOrder.Changed -= DrawOrder_Changed;
        }

        public void Update(GameTime gameTime, Rectangle viewBounds)
        {
            for (int i = 0; i < components.Count; i++) components[i].Update(gameTime, viewBounds);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
            for (int i = 0; i < components.Count; i++) components[i].Draw(spriteBatch, viewBounds);
        }
    }
}
