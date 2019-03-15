// BGR555.cs
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
    using System;
    using System.Drawing;
    using Yarhl.FileFormat;
    using Yarhl.IO;

    public class BGR555 : IColorEncoding
    {
        public Color Decode(DataStream source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int data = source.ReadByte() | (source.ReadByte() << 8);
            return Color.FromArgb(
                (data & 0x1F).Map(0x1F, 0xFF),
                ((data >> 5) & 0x1F).Map(0x1F, 0xFF),
                ((data >> 10) & 0x1F).Map(0x1F, 0xFF));
        }

        public void Encode(DataStream stream, Color source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            int data = source.R.Map(0xFF, 0x1F)
                | (source.G.Map(0xFF, 0x1F) << 5)
                | (source.B.Map(0xFF, 0x1F) << 5);

            stream.WriteByte((byte)(data & 0xFF));
            stream.WriteByte((byte)(data >> 8));
        }
    }
}
