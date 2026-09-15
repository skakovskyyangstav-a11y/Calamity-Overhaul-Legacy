using CalamityOverhaulLegacy.Common;
using InnoVault.RenderHandles;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityOverhaulLegacy.Content.Renders
{
    /// <summary>EndEntityDraw 弹幕扩展绘制层</summary>
    internal sealed class ProjectileLayerRender : RenderHandle
    {
        private static readonly List<IPrimitiveDrawable> _primitiveBuffer = new(64);
        private static readonly List<IAdditiveDrawable> _additiveBuffer = new(64);
        private static readonly HashSet<string> _loggedPrimitiveFailures = new();
        private static readonly HashSet<string> _loggedAdditiveFailures = new();

        /// <summary>权重 1.2，与原 EffectLoader 挂载次序一致</summary>
        public override float Weight => 1.2f;

        public override void EndEntityDraw(SpriteBatch spriteBatch, Main main)
        {
            CollectDrawables();

            int primitiveCount = _primitiveBuffer.Count;

            for (int i = 0; i < primitiveCount; i++) {
                IPrimitiveDrawable drawable = _primitiveBuffer[i];

                try {
                    drawable.DrawPrimitives();
                }
                catch (Exception ex) {
                    LogPrimitiveFailure(drawable, ex);

                    // InfiniteArrow changes GraphicsDevice.BlendState before
                    // Trail.DrawTrail. Restore it if that call aborts.
                    Main.graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                }
            }

            int additiveCount = _additiveBuffer.Count;

            if (additiveCount <= 0) {
                return;
            }

            bool began = false;

            try {
                spriteBatch.Begin(
                    SpriteSortMode.Deferred,
                    BlendState.Additive,
                    SamplerState.PointWrap,
                    DepthStencilState.None,
                    RasterizerState.CullNone,
                    null,
                    Main.GameViewMatrix.TransformationMatrix
                );

                began = true;

                for (int i = 0; i < additiveCount; i++) {
                    IAdditiveDrawable drawable = _additiveBuffer[i];

                    try {
                        drawable.DrawAdditiveAfterNon(spriteBatch);
                    }
                    catch (Exception ex) {
                        LogAdditiveFailure(drawable, ex);
                    }
                }
            }
            finally {
                if (began) {
                    spriteBatch.End();
                }
            }
        }

        private static void LogPrimitiveFailure(IPrimitiveDrawable drawable, Exception ex)
        {
            string typeName = drawable?.GetType().FullName ?? "<null primitive>";

            if (!_loggedPrimitiveFailures.Add(typeName)) {
                return;
            }

            ModContent.GetInstance<global::CalamityOverhaulLegacy.CalamityOverhaulLegacy>()
                .Logger.Error($"[LegacyPrimitiveFailure] {typeName}\n{ex}");
        }

        private static void LogAdditiveFailure(IAdditiveDrawable drawable, Exception ex)
        {
            string typeName = drawable?.GetType().FullName ?? "<null additive>";

            if (!_loggedAdditiveFailures.Add(typeName)) {
                return;
            }

            ModContent.GetInstance<global::CalamityOverhaulLegacy.CalamityOverhaulLegacy>()
                .Logger.Error($"[LegacyAdditiveFailure] {typeName}\n{ex}");
        }

        private static void CollectDrawables()
        {
            _primitiveBuffer.Clear();
            _additiveBuffer.Clear();

            Projectile[] projectiles = Main.projectile;
            int count = projectiles.Length;

            for (int i = 0; i < count; i++) {
                Projectile p = projectiles[i];

                if (!p.active) {
                    continue;
                }

                ModProjectile mp = p.ModProjectile;

                if (mp is null) {
                    continue;
                }

                if (mp is IPrimitiveDrawable primitive) {
                    _primitiveBuffer.Add(primitive);
                }

                if (mp is IAdditiveDrawable additive) {
                    _additiveBuffer.Add(additive);
                }
            }
        }
    }
}
