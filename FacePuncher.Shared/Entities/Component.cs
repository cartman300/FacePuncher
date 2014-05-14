﻿using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using FacePuncher.Geometry;

namespace FacePuncher.Entities
{
    public class ScriptDefinableAttribute : Attribute { }

    public abstract class Component
    {
        public static T Create<T>(Entity ent)
            where T : Component
        {
            return (T) Create(typeof(T), ent);
        }

        public static Component Create(Type t, Entity ent)
        {
            var c = t.GetConstructor(new Type[0]);

            if (c == null) {
                throw new MissingMethodException(
                    String.Format("Type {0} is missing a valid constructor.", t.FullName));
            }

            var comp = (Component) c.Invoke(new object[0]);
            comp.Entity = ent;

            return comp;
        }

        protected uint ID
        {
            get { return Entity.ID; }
        }

        protected Tile Tile
        {
            get { return Entity.Tile; }
        }

        protected Room Room
        {
            get { return Entity.Room; }
        }

        protected Level Level
        {
            get { return Entity.Level; }
        }

        protected Position RelativePosition
        {
            get { return Entity.RelativePosition; }
        }

        protected Position Position
        {
            get { return Entity.Position; }
        }

        public Entity Entity { get; private set; }

        public virtual void LoadFromDefinition(XElement elem)
        {
            foreach (var sub in elem.Elements()) {
                var ident = sub.Name.LocalName;
                var prop = GetType().GetProperty(ident);
                
                if (prop == null) continue;
                if (prop.GetCustomAttributes<ScriptDefinableAttribute>().Count() == 0) return;

                prop.SetValue(this, elem.Element(sub.Name, prop.PropertyType));
            }
        }

        public virtual void OnInitialize() { }

        public virtual void OnUpdateComponents() { }

        public virtual void OnThink(ulong time) { }

        public virtual void OnRemove() { }
    }
}