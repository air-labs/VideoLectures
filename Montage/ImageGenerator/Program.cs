using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 6)
			{
				Console.WriteLine("ImageGenerator <img> <title> <subtitle> <x> <y> <output dir>");
				Console.WriteLine("Example: ImageGenerator C:\\image.png \"Text\" \"More text\" 1280 720 ??? .");
				return;
			}
			int x, y = 0;
			if (!int.TryParse(args[3], out x) || !int.TryParse(args[4], out y))
			{
				Console.WriteLine("Invalid arguments: <x> and <y> must be integers");
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
				using (var font = new Font("Arial", 66, FontStyle.Bold, GraphicsUnit.Point))
				{
					var rect = new Rectangle(xOffset, 0, x-xOffset, y/2);

					var stringFormat = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};

					g.DrawString(args[1], font, Brushes.Black, rect, stringFormat);
				}
				// draw lower subtitle
				using (var font = new Font("Arial", 36, FontStyle.Regular, GraphicsUnit.Point))
				{
					var rect = new Rectangle(xOffset, y / 2, x - xOffset, y / 2);

					var stringFormat = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};

					g.DrawString(args[2], font, Brushes.Black, rect, stringFormat);
				}

			}
	
			bmp.Save(args[5]);
		}
	}
}
