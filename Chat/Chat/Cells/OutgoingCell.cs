﻿using System;

using UIKit;
using Foundation;
using CoreGraphics;

namespace Chat
{
	[Register ("OutgoingCell")]
	public class OutgoingCell : BubbleCell
	{
		static readonly UIImage normalBubbleImage;
		static readonly UIImage highlightedBubbleImage;

		public static readonly NSString CellId = new NSString ("Outgoing");

		static OutgoingCell ()
		{
			UIImage mask = UIImage.FromBundle ("BubbleOutgoing");

			var cap = new UIEdgeInsets {
				Top = 17,
				Left = 21,
				Bottom = 17,
				Right = 26
			};

			var normalColor = UIColor.FromRGB (43, 119, 250);
			var highlightedColor = UIColor.FromRGB (32, 96, 200);

			normalBubbleImage = CreateColoredImage (normalColor, mask).CreateResizableImage (cap);
			highlightedBubbleImage = CreateColoredImage (highlightedColor, mask).CreateResizableImage (cap);
		}

		public OutgoingCell (IntPtr handle)
			: base (handle)
		{
			Initialize ();
		}

		public OutgoingCell ()
		{
			Initialize ();
		}

		void Initialize ()
		{
			BubbleHighlightedImage = highlightedBubbleImage;
			BubbleImage = normalBubbleImage;

			ContentView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:[bubble]|",
				(NSLayoutFormatOptions)0, 
				"bubble", BubbleImageView));
			ContentView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:|-2-[bubble]-2-|",
				(NSLayoutFormatOptions)0,
				"bubble", BubbleImageView
			));
			BubbleImageView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:[bubble(>=48)]",
				(NSLayoutFormatOptions)0,
				"bubble", BubbleImageView
			));

			var vSpaceTop = NSLayoutConstraint.Create (MessageLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.Top, 1, 10);
			ContentView.AddConstraint (vSpaceTop);

			var vSpaceBottom = NSLayoutConstraint.Create (MessageLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.Bottom, 1, -10);
			ContentView.AddConstraint (vSpaceBottom);

			var msgTrailing = NSLayoutConstraint.Create (MessageLabel, NSLayoutAttribute.Trailing, NSLayoutRelation.LessThanOrEqual, BubbleImageView, NSLayoutAttribute.Trailing, 1, -16);
			ContentView.AddConstraint (msgTrailing);

			var msgCenter = NSLayoutConstraint.Create (MessageLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.CenterX, 1, -3);
			ContentView.AddConstraint (msgCenter);

			MessageLabel.TextColor = UIColor.White;
		}
	}
}