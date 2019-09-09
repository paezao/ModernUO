/***************************************************************************
 *                            GumpHtmlLocalized.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id$
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using Server.Buffers;

namespace Server.Gumps
{
  public enum GumpHtmlLocalizedType
  {
    Plain,
    Color,
    Args
  }

  public class GumpHtmlLocalized : GumpEntry
  {
    private string m_Args;
    private bool m_Background, m_Scrollbar;
    private int m_Color;
    private int m_Number;
    private int m_Width, m_Height;
    private int m_X, m_Y;

    public GumpHtmlLocalized(int x, int y, int width, int height, int number,
      bool background = false, bool scrollbar = false)
    {
      m_X = x;
      m_Y = y;
      m_Width = width;
      m_Height = height;
      m_Number = number;
      m_Background = background;
      m_Scrollbar = scrollbar;

      Type = GumpHtmlLocalizedType.Plain;
    }

    public GumpHtmlLocalized(int x, int y, int width, int height, int number, int color,
      bool background = false, bool scrollbar = false)
    {
      m_X = x;
      m_Y = y;
      m_Width = width;
      m_Height = height;
      m_Number = number;
      m_Color = color;
      m_Background = background;
      m_Scrollbar = scrollbar;

      Type = GumpHtmlLocalizedType.Color;
    }

    public GumpHtmlLocalized(int x, int y, int width, int height, int number, string args, int color,
      bool background = false, bool scrollbar = false)
    {
      // Are multiple arguments unsupported? And what about non ASCII arguments?

      m_X = x;
      m_Y = y;
      m_Width = width;
      m_Height = height;
      m_Number = number;
      m_Args = args;
      m_Color = color;
      m_Background = background;
      m_Scrollbar = scrollbar;

      Type = GumpHtmlLocalizedType.Args;
    }

    public int X
    {
      get => m_X;
      set => Delta(ref m_X, value);
    }

    public int Y
    {
      get => m_Y;
      set => Delta(ref m_Y, value);
    }

    public int Width
    {
      get => m_Width;
      set => Delta(ref m_Width, value);
    }

    public int Height
    {
      get => m_Height;
      set => Delta(ref m_Height, value);
    }

    public int Number
    {
      get => m_Number;
      set => Delta(ref m_Number, value);
    }

    public string Args
    {
      get => m_Args;
      set => Delta(ref m_Args, value);
    }

    public int Color
    {
      get => m_Color;
      set => Delta(ref m_Color, value);
    }

    public bool Background
    {
      get => m_Background;
      set => Delta(ref m_Background, value);
    }

    public bool Scrollbar
    {
      get => m_Scrollbar;
      set => Delta(ref m_Scrollbar, value);
    }

    public GumpHtmlLocalizedType Type { get; set; }

    public override string Compile()
    {
      switch (Type)
      {
        case GumpHtmlLocalizedType.Plain:
          return
            $"{{ xmfhtmlgump {m_X} {m_Y} {m_Width} {m_Height} {m_Number} {(m_Background ? 1 : 0)} {(m_Scrollbar ? 1 : 0)} }}";

        case GumpHtmlLocalizedType.Color:
          return
            $"{{ xmfhtmlgumpcolor {m_X} {m_Y} {m_Width} {m_Height} {m_Number} {(m_Background ? 1 : 0)} {(m_Scrollbar ? 1 : 0)} {m_Color} }}";

        default: // GumpHtmlLocalizedType.Args
          return
            $"{{ xmfhtmltok {m_X} {m_Y} {m_Width} {m_Height} {(m_Background ? 1 : 0)} {(m_Scrollbar ? 1 : 0)} {m_Color} {m_Number} @{m_Args}@ }}";
      }
    }

    private static byte[] m_LayoutNamePlain = Gump.StringToBuffer("{ xmfhtmlgump ");
    private static byte[] m_LayoutNameColor = Gump.StringToBuffer("{ xmfhtmlgumpcolor ");
    private static byte[] m_LayoutNameArgs = Gump.StringToBuffer("{ xmfhtmltok ");

    public override void AppendTo(SpanWriter writer, ref int entries, ref int switches)
    {
      switch (Type)
      {
        case GumpHtmlLocalizedType.Plain:
          {
            writer.Write(m_LayoutNamePlain);
            writer.WriteAscii(m_X.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Y.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Width.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Height.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Number.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Background ? "1" : "0");
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Scrollbar ? "1" : "0");
            writer.Write((byte)0x20); // ' '
            writer.Write((byte)0x7D); // '}'

            break;
          }

        case GumpHtmlLocalizedType.Color:
          {
            writer.Write(m_LayoutNameColor);
            writer.WriteAscii(m_X.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Y.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Width.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Height.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Number.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Background ? "1" : "0");
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Scrollbar ? "1" : "0");
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Color.ToString());
            writer.Write((byte)0x20); // ' '
            writer.Write((byte)0x7D); // '}'

            break;
          }

        case GumpHtmlLocalizedType.Args:
          {
            writer.Write(m_LayoutNameArgs);
            writer.WriteAscii(m_X.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Y.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Width.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Height.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Background ? "1" : "0");
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Scrollbar ? "1" : "0");
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Color.ToString());
            writer.Write((byte)0x20); // ' '
            writer.WriteAscii(m_Number.ToString());
            writer.Write((byte)0x20); // ' '
            writer.Write((byte)0x40); // '@'
            writer.WriteAscii(m_Args);
            writer.Write((byte)0x40); // '@'
            writer.Write((byte)0x20); // ' '
            writer.Write((byte)0x7D); // '}'

            break;
          }
      }
    }
  }
}
