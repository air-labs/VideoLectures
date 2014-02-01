using System;
using System.Drawing;
using System.IO;

namespace ImageGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 8)
			{
				Console.WriteLine("ImageGenerator <img> <title file> <title index> <subtitle file> <subtitle index> <x> <y> <output filename>");
				Console.WriteLine("Example: ImageGenerator C:\\image.png 1 3 1280 720 d:\\picture.png");
				return;
			}
			int x, y, titleIndex, subtitleIndex;
			if (!int.TryParse(args[2], out titleIndex) || !int.TryParse(args[4], out subtitleIndex) 
				|| !int.TryParse(args[5], out x) || !int.TryParse(args[6], out y))
			{
				Console.WriteLine("Invalid arguments: <title index>, <subtitle index>, <x> and <y> must be integers");
				return;
			}

			var titles = File.ReadAllLines(args[1]);
			var subtitles = File.ReadAllLines(args[3]);
			string title, subtitle;
			try
			{
				title = titles[titleIndex - 1];
				subtitle = subtitles[subtitleIndex];
			}
			catch
			{
				Console.WriteLine("Can't get title or subtitle ({0} / {1}) from files.", titleIndex, subtitleIndex);
				return;
			}

			var bmp = new Bitmap(x, y);
			var img = new Bitmap(args[0]);
			using (var g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.White);
				g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));  // Don't do anything with image, just place it here as 1:1
				var xOffset = img.Width;
				
				// draw upper title
				using (var font = new Font("Arial", 36, FontStyle.Bold, GraphicsUnit.Point))
				{
					var rect = new Rectangle(xOffset, 0, x-xOffset, y/2);

					var stringFormat = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};

					g.DrawString(title, font, Brushes.Black, rect, stringFormat);
				}
				// draw lower subtitle
				using (var font = new Font("Arial", 26, FontStyle.Regular, GraphicsUnit.Point))
				{
					var rect = new Rectangle(xOffset, y / 2, x - xOffset, y / 2);

					var stringFormat = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};

					g.DrawString(subtitle, font, Brushes.Black, rect, stringFormat);
				}

			}
	
			bmp.Save(args[7]);
		}
	}
}
