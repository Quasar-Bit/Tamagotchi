//
//  ByteExtensions.cs
//
//  Author:
//       Songurov Fiodor <songurov@gmail.com>
//
//  Copyright (c) 2021
//
//  This library is free software; you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 2.1 of the
//  License, or (at your option) any later version.
//
//  This library is distributed in the hope that it will be useful, but
//  WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Tamagotchi.Application.Extensions;

public static class ByteExtensions
{
    public static byte[] Compress(this byte[] bytes)
    {
        if (bytes is null) return Array.Empty<byte>();

        using var output = new MemoryStream();

        using var stream = new BrotliStream(output, CompressionMode.Compress);

        stream.Write(bytes, 0, bytes.Length);

        return output.ToArray();
    }

    public static byte[] Decompress(this byte[] bytes)
    {
        using var input = new MemoryStream(bytes);

        using var stream = new BrotliStream(input, CompressionMode.Decompress);

        using var output = new MemoryStream();

        stream.CopyTo(output);

        return output.ToArray();
    }

    public static T Object<T>(this byte[] bytes)
    {
        return JsonSerializer.Deserialize<T>(Encoding.Default.GetString(bytes));
    }

    public static string ToStringFromBase64(this Guid guid)
    {
        var encoded = Convert.ToBase64String(guid.ToByteArray());

        encoded = encoded.Replace("/", "_").Replace("+", "-").Insert(0,"!");

        return encoded[..22];
    }
}
