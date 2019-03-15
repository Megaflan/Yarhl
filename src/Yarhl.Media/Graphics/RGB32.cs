// RGB32.cs
//
// Copyright (c) 2019 SceneGate Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
namespace Yarhl.Media.Graphics
{
    using System.Drawing;
    using Yarhl.IO;

    public class RGB32 : IColorEncoding
    {
        public Color Decode(DataStream stream)
        {
            var color = Color.FromArgb(
                stream.ReadByte(),
                stream.ReadByte(),
                stream.ReadByte());

            stream.ReadByte(); // padding to 4 bytes (32-bits)
            return color;
        }

        public void Encode(DataStream stream, Color color)
        {
            stream.WriteByte(color.R);
            stream.WriteByte(color.G);
            stream.WriteByte(color.B);
            stream.WriteByte(0x00);
        }
    }
}
