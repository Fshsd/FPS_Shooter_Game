// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

using System;

namespace Animancer
{
    /// <summary>Determines how <see cref="AnimancerLayer.Play(AnimancerState, float, FadeMode)"/> works.</summary>
    /// 
    /// <remarks>
    /// <strong>Documentation:</strong>
    /// <see href="https://kybernetik.com.au/animancer/docs/manual/blending/fading/modes">
    /// Fade Modes</see>
    /// <para></para>
    /// <strong>Sample:</strong>
    /// <see href="https://kybernetik.com.au/animancer/docs/samples/basics/transitions">
    /// Transitions</see>
    /// </remarks>
    /// 
    /// https://kybernetik.com.au/animancer/api/Animancer/FadeMode
    /// 
    public enum FadeMode
    {
        /************************************************************************************************************************/

        /// <summary>
        /// Calculate the fade speed to bring the <see cref="AnimancerNode.Weight"/>
        /// from 0 to 1 over the specified fade duration (in seconds),
        /// regardless of the actual starting weight.
        /// </summary>
        ///
        /// <remarks>
        /// <strong>Example:</strong>
        /// A fade duration of 0.5 would make the fade last for 0.5 seconds, regardless of how long the animation is.
        /// <para></para>
        /// This is generally the same as <see cref="FixedDuration"/> but differs when starting the fade from a
        /// non-zero <see cref="AnimancerNode.Weight"/>, for example:
        /// <list type="bullet">
        /// <item>Fade Duration: 0.25</item>
        /// <item>To fade from 0 to 1 with either mode would get a speed of 4 and take 0.25 seconds</item>
        /// <item>To fade from 0.5 to 1 with <see cref="FixedDuration"/> would get a speed of 2 and take 0.25 seconds.
        /// It has half the distance to cover so it goes half as fast to maintain the expected duration.</item>
        /// <item>To fade from 0.5 to 1 with <see cref="FixedSpeed"/> would get a speed of 4 and take 0.125 seconds.
        /// It gets the same speed regardless of the distance to cover, so with less distance it completes faster.</item>
        /// </list>
        /// </remarks>
        ///
        /// <exception cref="InvalidOperationException">The <see cref="AnimancerState.Clip"/> is null.</exception>
        ///
        /// <exception cref="ArgumentOutOfRangeException">
        /// More states have been created for the <see cref="AnimancerState.Clip"/> than the
        /// <see cref="AnimancerLayer.MaxCloneCount"/> allows.
        /// </exception>
        FixedSpeed,

        /// <summary>
        /// Calculate the fade speed to bring the <see cref="AnimancerNode.Weight"/> to the target value over the
        /// specified fade duration (in seconds).
        /// </summary>
        ///
        /// <remarks>
        /// <strong>Example:</strong>
        /// A fade duration of 0.5 would make the fade last for 0.5 seconds, regardless of how long the animation is.
        /// <para></para>
        /// This is generally the same as <see cref="FixedSpeed"/>, but differs when starting the fade from a
        /// non-zero <see cref="AnimancerNode.Weight"/>:
        /// <list type="bullet">
        /// <item>Fade Duration: 0.25</item>
        /// <item>To fade from 0 to 1 with either mode would get a speed of 4 and take 0.25 seconds</item>
        /// <item>To fade from 0.5 to 1 with <see cref="FixedDuration"/> would get a speed of 2 and take 0.25 seconds.
        /// It has half the distance to cover so it goes half as fast to maintain the expected duration.</item>
        /// <item>To fade from 0.5 to 1 with <see cref="FixedSpeed"/> would get a speed of 4 and take 0.125 seconds.
        /// It gets the same speed regardless of the distance to cover, so with less distance it completes faster.</item>
        /// </list>
        /// </remarks>
        FixedDuration,

        /// <summary>
        /// If the <see cref="AnimancerNode.Weight"/> is above the <see cref="AnimancerLayer.WeightlessThreshold"/>,
        /// this mode will use <see cref="AnimancerLayer.GetOrCreateWeightlessState"/> to get a copy of it that is at 0
        /// weight so it can fade the copy in while the original fades out with all other states. This allows an
        /// animation to fade into itself.
        /// </summary>
        ///
        /// <remarks>
        /// This mode can be useful when you want to repeat an action while the previous animation is still fading out.
        /// For example, if you play an 'Attack' animation, it ends and starts fading back to 'Idle', and while it is
        /// doing so you want to start another 'Attack' with the same animation. The previous 'Attack' can't simply
        /// snap back to the start, so you can use this mode to create a second 'Attack' state to fade in while the old
        /// one fades out.
        /// <para></para>
        /// Using this mode repeatedly on subsequent frames will probably have undesirable effects because it will
        /// create a new state each time. In such a situation you most likely want <see cref="FixedSpeed"/> instead.
        /// <para></para>
        /// This mode only works for <see cref="ClipState"/>s.
        /// <para></para>
        /// The <see href="https://kybernetik.com.au/animancer/docs/manual/blending/fading/modes">Fade Modes</see> page
        /// explains this mode in more detail.
        /// </remarks>
        FromStart,

        /// <summary>
        /// Like <see cref="FixedSpeed"/>, except that the fade duration is multiplied by the animation length.
        /// </summary>
        NormalizedSpeed,

        /// <summary>
        /// Like <see cref="FixedDuration"/>, except that the fade duration is multiplied by the animation length.
        /// </summary>
        NormalizedDuration,

        /// <summary>
        /// Like <see cref="FromStart"/>, except that the fade duration is multiplied by the animation length.
        /// </summary>
        NormalizedFromStart,

        /************************************************************************************************************************/
    }
}

