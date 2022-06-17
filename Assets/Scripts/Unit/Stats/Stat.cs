using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Stat
    {
        private int _min = 0;
        public int Min
        {
            get => _min;
            set
            {
                if (_min > Max) throw new Exception("Min can not be larger than max");
                _min = value;
                Value = _value;
            }
        }

        private int _max = 10;
        public int Max
        {
            get => _max;
            set {
                if (_max < Min) throw new Exception("Max can not be smaller than min");
                _max = value;
                Value = _value;
            }
        }

        private int _value = 10;

        public int Value
        {
            get => _value;
            set
            {
                int newValue = Math.Clamp(value, Min, Max);
                if (newValue == _value) return;

                _value = newValue;
                ValueChangedEvent?.Invoke(this);
            }
        }

        public float Norm
        {
            get => Mathf.Clamp01((float)(Value - Min) / (float)(Max - Min));
        }

        public UnityAction<Stat> ValueChangedEvent = null;
    }
}