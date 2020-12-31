using System;
using System.Collections.Generic;
using System.Text;

namespace MiDI.MidiDevices.Launchpad
{
    /// <summary>
    /// Struct for simple representation for a Point in 2D space
    /// </summary>
    public struct PadCoordinate : IEquatable<PadCoordinate>
    {
        #region Values

        public int x;
        public int y;

        #endregion



        #region Methods

        public PadCoordinate(int _x, int _y) { x = _x; y = _y; }
        public bool Equals(PadCoordinate other) { if (other.x == this.x && other.y == this.y) return true; return false;  }
        public override int GetHashCode() { return Tuple.Create(x, y).GetHashCode(); }
        public static PadCoordinate operator +(PadCoordinate lhs, PadCoordinate rhs) => new PadCoordinate(lhs.x + rhs.x, lhs.y + rhs.y);
        public static PadCoordinate operator -(PadCoordinate lhs, PadCoordinate rhs) => new PadCoordinate(lhs.x - rhs.x, lhs.y - rhs.y);
        public static PadCoordinate operator *(PadCoordinate lhs, float rhs) => new PadCoordinate((int)(lhs.x * rhs), (int)(lhs.y * rhs));
        public static PadCoordinate operator *(float lhs, PadCoordinate rhs) => new PadCoordinate((int)(rhs.x * lhs), (int)(rhs.y * lhs));
        public static bool operator ==(PadCoordinate lhs, PadCoordinate rhs) => lhs.x == rhs.x && lhs.y == rhs.y;
        public static bool operator !=(PadCoordinate lhs, PadCoordinate rhs) => lhs.x != rhs.x || lhs.y != rhs.y;

        #endregion
    }
}
