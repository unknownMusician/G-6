using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl
{
    protected CharacterBase character;

    protected float damage;
    protected float duration;
    protected float interval;

    protected float currentInterval;
    protected float currentDuration;
    protected CardEffect.EffectType type;

    public float Damage
    {
        get => damage;
        set
        {
            if (value > damage)
                damage = value;
        }
    }
    public float Duration
    {
        get => duration;
        set
        {
            if (value > duration)
            {
                duration = value;
            }
            currentDuration = 0;
        }
    }
    public float Interval
    {
        get => interval;
        set
        {
            if (value < interval)
                interval = value;
        }
    }

    public EffectControl(CardEffect.EffectType type, float damage, float duration, float interval, CharacterBase character)
    {
        this.type = type;
        this.damage = damage;
        this.duration = duration;
        this.interval = interval;
        this.character = character;

        this.currentInterval = 0;
        this.currentDuration = 0;
    }
    public EffectControl(CardEffect.NestedProps props, CharacterBase character)
    {
        this.type = props.Effect;
        this.damage = props.DMG;
        this.duration = props.Duration;
        this.interval = props.Interval;
        this.character = character;

        this.currentInterval = 0;
        this.currentDuration = 0;
    }

    public void Act(float deltaTime)
    {
        currentInterval += deltaTime;
        currentDuration += deltaTime;

        if (currentInterval >= interval)
        {
            currentInterval -= interval;
            character.TakeDamage(damage);
        }

        if (currentDuration >= duration)
        {
            character.CurrentEffects.Remove(type);
        }
    }

    public void ChangeParams(CardEffect.NestedProps props)
    {
        if (type == props.Effect)
        {
            Damage = props.DMG;
            Duration = props.Duration;
            Interval = props.Interval;
        }
    }
}
