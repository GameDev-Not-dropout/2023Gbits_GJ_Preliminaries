using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    public Vector2 velocity;
    public AnimationClip currentAnimation;
    internal float animationTime;

    public virtual void TimeUpdate()
    {
        if (currentAnimation != null)
        {
            animationTime += Time.deltaTime;
            if (animationTime > currentAnimation.length)
            {
                animationTime = animationTime - currentAnimation.length;
            }
        }
    }

    public void UpdateAnimation()
    {
        if (currentAnimation != null)
        {
            currentAnimation.SampleAnimation(gameObject, animationTime);
        }
    }
}

