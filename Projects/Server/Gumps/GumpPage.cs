/***************************************************************************
 *                                GumpPage.cs
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

using System.Buffers;
using Server.Network;

namespace Server.Gumps
{
  public class GumpPage : GumpEntry
  {
    private static byte[] m_LayoutName = Gump.StringToBuffer("page");
    private int m_Page;

    public GumpPage(int page)
    {
      m_Page = page;
    }

    public int Page
    {
      get => m_Page;
      set => Delta(ref m_Page, value);
    }

    public override string Compile(ArraySet<string> strings) => $"{{ page {m_Page} }}";

    public override void AppendTo(ArrayBufferWriter<byte> buffer, ArraySet<string> strings, ref int entries, ref int switches)
    {
      disp.AppendLayout(m_LayoutName);
      disp.AppendLayout(m_Page);
    }
  }
}