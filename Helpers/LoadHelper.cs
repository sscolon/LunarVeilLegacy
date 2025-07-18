﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;

namespace LunarVeilLegacy.Helpers
{
    public static class LoadHelper
    {
        public static MethodInfo startRain;
        public static MethodInfo stopRain;
        public static void Load(bool unload)
        {
            if (unload)
            {
                startRain = null;
                stopRain = null;
            }
            else
            {
                startRain = typeof(Main).GetMethod("StartRain", BindingFlags.Static | BindingFlags.NonPublic);
                stopRain = typeof(Main).GetMethod("StopRain", BindingFlags.Static | BindingFlags.NonPublic);
            }
        }

        public static void SandstormStuff()
        {
            Sandstorm.IntendedSeverity = 20; //0.4f;
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;
            NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }

       

        public static void StartRain()
        {
            startRain.Invoke(null, null);
        }
        public static void StopRain()
        {
            stopRain.Invoke(null, null);
        }

        public static Rectangle toRect(Vector2 pos, int w, int h)
        {
            return new Rectangle((int)(pos.X - Main.screenPosition.X), (int)(pos.Y - Main.screenPosition.Y), w, h);
        }

        public static Vector2 GetInventoryPosition(Vector2 position, Rectangle frame, Vector2 origin, float scale)
        {
            return position + (((frame.Size() / 2f) - origin) * scale * Main.inventoryScale) + new Vector2(1.5f, 1.5f);
        }

        public static Vector2 NearestPoint(this Vector2 vec, Rectangle rect)
        {
            float nearX = vec.X > rect.Right ? rect.Right : vec.X < rect.Left ? rect.Left : vec.X;
            float nearY = vec.Y > rect.Bottom ? rect.Bottom : vec.Y < rect.Top ? rect.Top : vec.Y;
            return new Vector2(nearX, nearY);
        }
        private static float Bezier(float x1, float x2, float x3, float x4, float t)
        {
            return (float)(
                x1 * Math.Pow(1 - t, 3) +
                x2 * 3 * t * Math.Pow(1 - t, 2) +
                x3 * 3 * Math.Pow(t, 2) * (1 - t) +
                x4 * Math.Pow(t, 3)
                );
        }
        public static Vector2 Bezier(Vector2 from, Vector2 to, Vector2 cp1, Vector2 cp2, float amount)
        {
            Vector2 output = new Vector2();
            output.X = Bezier(from.X, cp1.X, cp2.X, to.X, amount);
            output.Y = Bezier(from.Y, cp1.Y, cp2.Y, to.Y, amount);
            return output;
        }
        public static Vector3 ToVector3(this Vector2 vec)
        {
            return new Vector3(vec.X, vec.Y, 0);
        }
        public static Vector2 AdjustToGravity(this Vector2 velocity, float gravity, float time)
        {
            velocity.X = velocity.X / time;
            velocity.Y = velocity.Y / time - 0.5f * gravity * time;
            return velocity;
        }
        public static bool HasParameter(this Effect effect, string name)
        {
            foreach (EffectParameter param in effect.Parameters)
            {
                if (param.Name == name) return true;
            }
            return false;
        }
        public static MyPlayer LunarVeilLegacy(this Player player)
        {
            return player.GetModPlayer<MyPlayer>();
        }
        public static void SafeSetParameter(this Effect effect, string name, float value)
        {
            if (effect.HasParameter(name)) effect.Parameters[name].SetValue(value);
        }
        public static void SafeSetParameter(this Effect effect, string name, Vector2 value)
        {
            if (effect.HasParameter(name)) effect.Parameters[name].SetValue(value);
        }
        public static void SafeSetParameter(this Effect effect, string name, Color value)
        {
            if (effect.HasParameter(name)) effect.Parameters[name].SetValue(value.ToVector3());
        }
        public static void SafeSetParameter(this Effect effect, string name, Texture2D value)
        {
            if (effect.HasParameter(name)) effect.Parameters[name].SetValue(value);
        }
        public static void SafeSetParameter(this Effect effect, string name, Matrix value)
        {
            if (effect.HasParameter(name)) effect.Parameters[name].SetValue(value);
        }
        public static void Reload(this SpriteBatch spriteBatch, SpriteSortMode sortMode = SpriteSortMode.Deferred)
        {
            if (spriteBatch.HasBegun())
            {
                spriteBatch.End();
            }
            BlendState blendState = (BlendState)spriteBatch.GetType().GetField("blendState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            SamplerState samplerState = (SamplerState)spriteBatch.GetType().GetField("samplerState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            DepthStencilState depthStencilState = (DepthStencilState)spriteBatch.GetType().GetField("depthStencilState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            RasterizerState rasterizerState = (RasterizerState)spriteBatch.GetType().GetField("rasterizerState", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            Effect effect = (Effect)spriteBatch.GetType().GetField("customEffect", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            Matrix matrix = (Matrix)spriteBatch.GetType().GetField("transformMatrix", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, matrix);
        }
        public static void Reload(this SpriteBatch spriteBatch, BlendState blendState = null, SpriteSortMode sortMode = default)
        {
            if (spriteBatch.HasBegun())
            {
                spriteBatch.End();
            }
            if (blendState == null) blendState = (BlendState)spriteBatch.GetField("blendState");
            SamplerState state = (SamplerState)spriteBatch.GetField("samplerState");
            DepthStencilState state2 = (DepthStencilState)spriteBatch.GetField("depthStencilState");
            RasterizerState state3 = (RasterizerState)spriteBatch.GetField("rasterizerState");
            Effect effect = (Effect)spriteBatch.GetField("customEffect");
            Matrix matrix = (Matrix)spriteBatch.GetField("transformMatrix");
            spriteBatch.Begin(sortMode, blendState, state, state2, state3, effect, matrix);
        }
        public static bool HasBegun(this SpriteBatch spriteBatch)
        {
            return (bool)spriteBatch.GetType().GetField("beginCalled", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(spriteBatch);
        }
        public static object GetField(this object obj, string name)
        {
            return obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
        }

        public static float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        public static void Move(this NPC npc, Vector2 pos, float speed, float divider)
        {
            Vector2 vel = npc.DirectionTo(pos) * speed;
            npc.velocity = (npc.velocity * divider + vel) / (divider + 1);
        }

        public static void Move(this NPC npc, Vector2 vector, float speed, float turnResistance = 10f,
            bool toPlayer = false)
        {
            Player player = Main.player[npc.target];
            Vector2 moveTo = toPlayer ? player.Center + vector : vector;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            npc.velocity = move;
        }

        public static void Move(this Projectile projectile, Vector2 vector, float speed, float turnResistance = 10f, bool toPlayer = false)
        {
            Player player = Main.player[projectile.owner];
            Vector2 moveTo = toPlayer ? player.Center + vector : vector;
            Vector2 move = moveTo - projectile.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            move = (projectile.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            projectile.velocity = move;
        }

        /// <summary>
        /// Gets the top-left tile of a multitile
        /// </summary>
        /// <param name="i">The tile X-coordinate</param>
        /// <param name="j">The tile Y-coordinate</param>
        public static Point16 GetTileOrigin(int i, int j)
        {
            //Framing.GetTileSafely ensures that the returned Tile instance is not null
            //Do note that neither this method nor Framing.GetTileSafely check if the wanted coordiates are in the world!
            Tile tile = Framing.GetTileSafely(i, j);

            Point16 coord = new Point16(i, j);
            Point16 frame = new Point16(tile.TileFrameX / 18, tile.TileFrameY / 18);

            return coord - frame;
        }

        /// <summary>
        /// Uses <seealso cref="GetTileOrigin(int, int)"/> to try to get the entity bound to the multitile at (<paramref name="i"/>, <paramref name="j"/>).
        /// </summary>
        /// <typeparam name="T">The type to get the entity as</typeparam>
        /// <param name="i">The tile X-coordinate</param>
        /// <param name="j">The tile Y-coordinate</param>
        /// <param name="entity">The found <typeparamref name="T"/> instance, if there was one.</param>
        /// <returns><see langword="true"/> if there was a <typeparamref name="T"/> instance, or <see langword="false"/> if there was no entity present OR the entity was not a <typeparamref name="T"/> instance.</returns>
        public static bool TryGetTileEntityAs<T>(int i, int j, out T entity) where T : TileEntity
        {
            Point16 origin = GetTileOrigin(i, j);

            //TileEntity.ByPosition is a Dictionary<Point16, TileEntity> which contains all placed TileEntity instances in the world
            //TryGetValue is used to both check if the dictionary has the key, origin, and get the value from that key if it's there
            if (TileEntity.ByPosition.TryGetValue(origin, out TileEntity existing) && existing is T existingAsT)
            {
                entity = existingAsT;
                return true;
            }

            entity = null;
            return false;
        }

        public static void Kill(this NPC npc)
        {
            npc.life = 0;
            npc.NPCLoot();
            npc.active = false;

            //npc.netUpdate = true;
        }

        public static float isLeft(Vector2 P0, Vector2 P1, Vector2 P2)
        {
            return ((P1.X - P0.X) * (P2.X - P0.X) - (P2.X - P0.X) * (P1.X - P0.X));
        }
        public static bool PointInShape(Vector2 P, params Vector2[] args)
        {
            bool result = true;
            for (int i = 0; i < args.Length; i++)
            {
                int i1 = i;
                int i2 = (i + 1) % args.Length;
                Vector2 a = args[i1];
                Vector2 b = args[i2];
                Vector2 c = P;
                if (isLeft(a, b, c) < 0) result = false;
            }
            return result;
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            int c = list.Count;
            List<T> current = new List<T>();
            for (int i = 0; i < c; i++)
            {
                int index = Main.rand.Next(list.Count);
                current.Add(list[index]);
                list.RemoveAt(index);
            }

            return current;
        }

        public static T[] Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = Main.rand.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }

            return array;
            //return Shuffle<T>(new List<T>(array)).ToArray();
        }
    }
}