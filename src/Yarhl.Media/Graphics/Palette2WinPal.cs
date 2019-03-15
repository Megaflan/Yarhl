// Palette2WinPal.cs
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
    using Yarhl.FileFormat;
    using Yarhl.IO;

    public class Palette2WinPal : IConverter<Palette, BinaryFormat>, IInitializer<bool>
    {
        public static int Version => 03_00;

        public bool GimpCompatibility {
            get;
            set;
        }

        public void Initialize(bool gimpCompatibility)
        {
            GimpCompatibility = gimpCompatibility;
        }

        public BinaryFormat Convert(Palette palette)
        {
            if (palette == null)
                throw new ArgumentNullException(nameof(palette));

            var binary = new BinaryFormat();
            var writer = new DataWriter(binary.Stream);

            int paletteSize = palette.Colors.Count * 4;

            writer.Write("RIFF", nullTerminator: false);
            writer.Write((uint)(0x10 + paletteSize));

            writer.Write("PAL ", nullTerminator: false);
            writer.Write("data", nullTerminator: false);
            writer.Write((uint)(0x04 + paletteSize));
            writer.Write((ushort)Version);
            writer.Write((ushort)(palette.Colors.Count));

            // Bug in Gimp
            if (GimpCompatibility)
                writer.Write((uint)0x00);

            foreach (var color in palette.Colors) {
                writer.Write<RGB32>(color);
            }

            return binary;
        }
    }
}
