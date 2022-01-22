﻿using System;
using System.Drawing;
using PDFPatcher.Common;

namespace PDFPatcher.Processor
{
	sealed class SetTextColorProcessor : IPdfInfoXmlProcessor
	{
		readonly string r, g, b;

		public SetTextColorProcessor(Color color) {
			if (color != Color.Transparent && color != Color.White) {
				r = ValueHelper.ToText(color.R / 255f);
				g = ValueHelper.ToText(color.G / 255f);
				b = ValueHelper.ToText(color.B / 255f);
			}
		}

		#region IInfoDocProcessor 成员

		public string Name => "设置书签文本颜色";

		public IUndoAction Process(System.Xml.XmlElement item) {
			var undo = new UndoActionGroup();
			if (String.IsNullOrEmpty(r)) {
				undo.RemoveAttribute(item, Constants.Colors.Red);
				undo.RemoveAttribute(item, Constants.Colors.Green);
				undo.RemoveAttribute(item, Constants.Colors.Blue);
			}
			else {
				undo.SetAttribute(item, Constants.Colors.Red, r);
				undo.SetAttribute(item, Constants.Colors.Green, g);
				undo.SetAttribute(item, Constants.Colors.Blue, b);
			}
			undo.RemoveAttribute(item, Constants.Color);
			return undo;
		}

		#endregion
	}
}
