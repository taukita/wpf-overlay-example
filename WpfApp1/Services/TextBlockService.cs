﻿namespace WpfApp1.Services
{
	using System;
	using System.Threading;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Threading;

	//see https://stackoverflow.com/questions/1041820/how-can-i-determine-if-my-textblock-text-is-being-trimmed
	public static class TextBlockService
	{
		private static readonly DependencyPropertyKey IsTextTrimmedKey = DependencyProperty.RegisterAttachedReadOnly(
			"IsTextTrimmed",
			typeof(bool),
			typeof(TextBlockService),
			new PropertyMetadata(false));

		public static readonly DependencyProperty IsTextTrimmedProperty = IsTextTrimmedKey.DependencyProperty;


		public static readonly DependencyProperty AutomaticToolTipEnabledProperty = DependencyProperty.RegisterAttached(
			"AutomaticToolTipEnabled",
			typeof(bool),
			typeof(TextBlockService),
			new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

		static TextBlockService()
		{
			// Register for the SizeChanged event on all TextBlocks, even if the event was handled.
			EventManager.RegisterClassHandler(
				typeof(TextBlock),
				FrameworkElement.SizeChangedEvent,
				new SizeChangedEventHandler(OnTextBlockSizeChanged),
				true);
		}

		[AttachedPropertyBrowsableForType(typeof(DependencyObject))]
		public static bool GetAutomaticToolTipEnabled(DependencyObject element)
		{
			if (null == element)
			{
				throw new ArgumentNullException(nameof(element));
			}

			return (bool) element.GetValue(AutomaticToolTipEnabledProperty);
		}

		[AttachedPropertyBrowsableForType(typeof(TextBlock))]
		public static bool GetIsTextTrimmed(TextBlock target)
		{
			return (bool) target.GetValue(IsTextTrimmedProperty);
		}

		public static void SetAutomaticToolTipEnabled(DependencyObject element, bool value)
		{
			if (null == element)
			{
				throw new ArgumentNullException(nameof(element));
			}

			element.SetValue(AutomaticToolTipEnabledProperty, value);
		}

		private static bool CalculateIsTextTrimmed(TextBlock textBlock)
		{
			if (!textBlock.IsArrangeValid)
			{
				return GetIsTextTrimmed(textBlock);
			}

			var typeface = new Typeface(
				textBlock.FontFamily,
				textBlock.FontStyle,
				textBlock.FontWeight,
				textBlock.FontStretch);

			// FormattedText is used to measure the whole width of the text held up by TextBlock container
			var formattedText = new FormattedText(
				textBlock.Text,
				Thread.CurrentThread.CurrentCulture,
				textBlock.FlowDirection,
				typeface,
				textBlock.FontSize,
				textBlock.Foreground) {MaxTextWidth = textBlock.ActualWidth};


			// When the maximum text width of the FormattedText instance is set to the actual
			// width of the textBlock, if the textBlock is being trimmed to fit then the formatted
			// text will report a larger height than the textBlock. Should work whether the
			// textBlock is single or multi-line.
			// The "formattedText.MinWidth > formattedText.MaxTextWidth" check detects if any 
			// single line is too long to fit within the text area, this can only happen if there is a 
			// long span of text with no spaces.
			return formattedText.Height > textBlock.ActualHeight ||
			       formattedText.MinWidth > formattedText.MaxTextWidth;
		}

		private static void OnTextBlockSizeChanged(object sender, SizeChangedEventArgs e)
		{
			TriggerTextRecalculation(sender);
		}

		private static void TriggerTextRecalculation(object sender)
		{
			var textBlock = sender as TextBlock;
			if (null == textBlock)
			{
				return;
			}

			if (TextTrimming.None == textBlock.TextTrimming)
			{
				textBlock.SetValue(IsTextTrimmedKey, false);
			}
			else
			{
				//If this function is called before databinding has finished the tooltip will never show.
				//This invoke defers the calculation of the text trimming till after all current pending databinding
				//has completed.
				var isTextTrimmed = textBlock.Dispatcher.Invoke(() => CalculateIsTextTrimmed(textBlock),
					DispatcherPriority.DataBind);
				textBlock.SetValue(IsTextTrimmedKey, isTextTrimmed);
			}
		}
	}
}