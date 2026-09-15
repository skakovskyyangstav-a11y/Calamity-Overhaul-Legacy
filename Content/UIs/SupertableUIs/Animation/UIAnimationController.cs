using System ;
using Microsoft.Xna.Framework ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Animation
{
    public class UIAnimationController
    {
        private float openProgress ;
        private int closeDelay ;
        private readonly float[] hover = new float[SupertableConstants.TOTAL_SLOTS] ;
        public float OpenProgress => openProgress ;
        public void UpdateOpenAnimation(bool open)
        {
            if (open && closeDelay <= 0) openProgress += SupertableConstants.ANIMATION_SPEED_OPEN ;
            else openProgress -= SupertableConstants.ANIMATION_SPEED_CLOSE ;
            openProgress = MathHelper.Clamp(openProgress, 0f, 1f) ;
            if (closeDelay > 0) closeDelay-- ;
        }
        public void UpdateSlotHoverAnimation(int index)
        {
            for (int i=0;i<hover.Length;i++) hover[i] = MathHelper.Clamp(hover[i] + (i==index ? SupertableConstants.HOVER_ANIMATION_SPEED : -SupertableConstants.HOVER_ANIMATION_SPEED),0f,1f) ;
        }
        public float GetSlotHoverProgress(int index) => index>=0 && index<hover.Length ? hover[index] : 0f ;
        public void RequestDelayedClose(int frames=30) => closeDelay=frames ;
        public void ForceClose() { closeDelay=0 ; openProgress=0f ; }
        public void Reset() { closeDelay=0 ; openProgress=0f ; Array.Fill(hover,0f) ; }
    }
}
