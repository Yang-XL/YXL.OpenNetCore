﻿using System;
using System.Threading;

namespace LoggerExtensions.Setting
{
    public class LogScope
    {
        private static readonly AsyncLocal<LogScope> _value = new AsyncLocal<LogScope>();
        private readonly string _name;
        private readonly object _state;

        internal LogScope(string name, object state)
        {
            _name = name;
            _state = state;
        }

        public LogScope Parent { get; private set; }

        public static LogScope Current
        {
            set => _value.Value = value;
            get => _value.Value;
        }

        public static IDisposable Push(string name, object state)
        {
            var temp = Current;
            Current = new LogScope(name, state) {Parent = temp};
            return new DisposableScope();
        }

        public override string ToString()
        {
            return _state?.ToString();
        }

        private class DisposableScope : IDisposable
        {
            public void Dispose()
            {
                Current = Current.Parent;
            }
        }
    }
}