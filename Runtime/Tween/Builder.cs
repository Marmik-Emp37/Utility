using UnityEngine;
using UnityEngine.Events;

namespace Emp37.Utility.Tween
{
#pragma warning disable IDE1006 // Naming Styles
      internal abstract class Builder
      {
            private protected readonly Transform transform;

            private readonly float duration;
            private float delay, overshoot = 1F, elapsedTime;
            private readonly Ease.Type type;
            private TimeMode mode;
            private UnityAction onComplete = delegate { };
            private bool initialised;

            public bool IsComplete { get; private set; }

            private float deltaTime => mode switch
            {
                  TimeMode.Unscaled => Time.unscaledDeltaTime,
                  _ => Time.deltaTime
            };

            internal Builder(Transform transform, float duration, Ease.Type type)
            {
                  this.transform = transform;
                  this.duration = duration;
                  this.type = type;
            }


            internal void Update()
            {
                  if (delay > 0F)
                  {
                        delay -= deltaTime;
                        return;
                  }

                  if (!initialised)
                  {
                        Initialize();
                        initialised = true;
                  }

                  if (elapsedTime < 1F)
                  {
                        elapsedTime = Mathf.Clamp01(elapsedTime + Time.deltaTime / duration);

                        float T = Ease.EasedRatio(elapsedTime, type, overshoot);
                        ExecuteTween(T);
                  }
                  else
                  {
                        onComplete.Invoke();
                        IsComplete = true;
                  }
            }

            public abstract void Initialize();
            public abstract void ExecuteTween(float value);


            #region C H A I N E D   M E T H O D S
            /// <summary>
            /// Sets the delay before starting the tween animation.
            /// </summary>
            /// <param name="value">The duration of delay in seconds.</param>
            public Builder setDelay(float value)
            {
                  delay = value;
                  return this;
            }
            /// <summary>
            /// Sets the intensity of overshooting for the tween animation.
            /// <br><b>Note: </b>Overshoot is only effective when using 'Back' or 'Elastic' ease types.</br>
            /// </summary>
            /// <param name="value">Higher values result in more pronounced overshooting.</param>
            public Builder setOvershoot(float value)
            {
                  overshoot = value;
                  return this;
            }
            public Builder setMode(TimeMode value)
            {
                  mode = value;
                  return this;
            }
            /// <summary>
            /// Specifies an action to be executed once the tween animation is complete.
            /// </summary>
            /// <param name="call">The action to be performed once.</param>
            public Builder setOnComplete(UnityAction call)
            {
                  onComplete += call;
                  return this;
            }
            #endregion
      }
#pragma warning restore IDE1006
}