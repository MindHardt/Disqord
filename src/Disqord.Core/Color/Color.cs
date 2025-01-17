using System;
using System.ComponentModel;

namespace Disqord
{
    /// <summary>
    ///     Represents an RGB color used by Discord.
    /// </summary>
    public readonly partial struct Color : IEquatable<int>, IEquatable<Color>, IComparable<int>, IComparable<Color>
    {
        /// <summary>
        ///     Gets the raw value of this <see cref="Color"/>.
        /// </summary>
        public int RawValue { get; }

        /// <summary>
        ///     Gets the red component of this <see cref="Color"/>.
        /// </summary>
        public byte R => (byte) (RawValue >> 16);

        /// <summary>
        ///     Gets the green component of this <see cref="Color"/>.
        /// </summary>
        public byte G => (byte) (RawValue >> 8);

        /// <summary>
        ///     Gets the blue component of this <see cref="Color"/>.
        /// </summary>
        public byte B => (byte) RawValue;

        /// <summary>
        ///     Instantiates a new <see cref="Color"/> using the given raw value.
        /// </summary>
        /// <param name="rawValue"> The raw value. </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     "Raw value must be a non-negative value less than or equal to <c>16777215</c>."
        /// </exception>
        public Color(int rawValue)
        {
            if (rawValue < 0 || rawValue > 0xFFFFFF)
                throw new ArgumentOutOfRangeException(nameof(rawValue), "Raw value must be a non-negative value less than or equal to 16777215.");

            RawValue = rawValue;
        }

        /// <summary>
        ///     Instantiates a new <see cref="Color"/> using the given RGB values.
        /// </summary>
        /// <param name="r"> The red (0-255) component value. </param>
        /// <param name="g"> The green (0-255) component value. </param>
        /// <param name="b"> The blue (0-255) component value. </param>
        public Color(byte r, byte g, byte b)
        {
            RawValue = r << 16 | g << 8 | b;
        }

        /// <summary>
        ///     Instantiates a new <see cref="Color"/> using the given RGB values.
        /// </summary>
        /// <param name="r"> The red (0-1) component value. </param>
        /// <param name="g"> The green (0-1) component value. </param>
        /// <param name="b"> The blue (0-1) component value. </param>
        public Color(float r, float g, float b)
        {
            if (r < 0 || r > 1)
                throw new ArgumentOutOfRangeException(nameof(r));

            if (g < 0 || g > 1)
                throw new ArgumentOutOfRangeException(nameof(g));

            if (b < 0 || b > 1)
                throw new ArgumentOutOfRangeException(nameof(b));

            RawValue = (byte) (r * 255) << 16 | (byte) (g * 255) << 8 | (byte) (b * 255);
        }

        public bool Equals(Color other)
            => RawValue == other.RawValue;

        public int CompareTo(Color other)
            => RawValue.CompareTo(other.RawValue);

        public bool Equals(int other)
            => RawValue == other;

        public int CompareTo(int other)
            => RawValue.CompareTo(other);

        public override bool Equals(object obj)
            => obj is Color color && Equals(color);

        public override int GetHashCode()
            => RawValue;

        /// <summary>
        ///     Returns the hexadecimal representation of this <see cref="Color"/>.
        /// </summary>
        /// <returns>
        ///     The hexadecimal representation of this <see cref="Color"/>.
        /// </returns>
        public override string ToString()
            => $"#{RawValue:X6}";

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out byte r, out byte g, out byte b)
        {
            r = R;
            g = G;
            b = B;
        }

        public static bool operator <=(Color left, Color right)
            => left.RawValue <= right.RawValue;

        public static bool operator >=(Color left, Color right)
            => left.RawValue >= right.RawValue;

        public static bool operator >(Color left, Color right)
            => left.RawValue > right.RawValue;

        public static bool operator <(Color left, Color right)
            => left.RawValue < right.RawValue;

        public static bool operator ==(Color left, Color right)
            => left.RawValue == right.RawValue;

        public static bool operator !=(Color left, Color right)
            => left.RawValue != right.RawValue;

        /// <summary>
        ///     Implicitly instantiates a new <see cref="Color"/> from this raw value.
        /// </summary>
        /// <param name="value"> The raw value. </param>
        public static implicit operator Color(int value)
            => new(value);

        /// <summary>
        ///     Implicitly gets <see cref="RawValue"/> from the given <see cref="Color"/>.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(Color value)
            => value.RawValue;

        public static implicit operator Color((byte R, byte G, byte B) value)
            => new(value.R, value.G, value.B);

        public static implicit operator (byte R, byte G, byte B)(Color value)
            => (value.R, value.G, value.B);

        public static implicit operator Color((float R, float G, float B) value)
            => new(value.R, value.G, value.B);

        public static implicit operator System.Drawing.Color(Color value)
            => System.Drawing.Color.FromArgb(value.R, value.G, value.B);

        public static implicit operator Color(System.Drawing.Color value)
            => new(value.R, value.G, value.B);

        public static Color FromHsv(float h, float s, float v)
        {
            if (s < 0 || s > 1)
                throw new ArgumentOutOfRangeException(nameof(s));

            if (v < 0 || v > 1)
                throw new ArgumentOutOfRangeException(nameof(s));

            if (s == 0)
                return (v, v, v);

            var i = MathF.Floor(h / 60) % 6;
            var f = h / 60 - MathF.Floor(h / 60);
            var p = v * (1f - s);
            var q = v * (1f - s * f);
            var t = v * (1f - s * (1f - f));
            return i switch
            {
                0 => (v, t, p),
                1 => (q, v, p),
                2 => (p, v, t),
                3 => (p, q, v),
                4 => (t, p, v),
                _ => (v, p, q)
            };
        }
    }
}
