using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALGA
{
    public class Sierpinski
    {
        public static void draw_sierpinski_carpet(int levels, double x, double y, double width, double height, SierpinskiCanvas canvas)
        {
            if (levels == 0) return;

            var newWidth = width / 3;
            var newHeight = height / 3;

            if (x == 0.0 && y == 0.0)
            {
                canvas.drawSquare(newWidth, newHeight, newWidth, newHeight);
                draw_sierpinski_carpet(levels - 1, newWidth, newHeight, newWidth, newHeight, canvas);

                return;
            }

            x = x + newWidth;
            y = y + newHeight;

            // Left
            canvas.drawSquare(x - width, y, newWidth, newHeight);
            draw_sierpinski_carpet(levels - 1, x - width, y, newWidth, newHeight, canvas);

            // Top Left
            canvas.drawSquare(x - width, y - height, newWidth, newHeight);
            draw_sierpinski_carpet(levels - 1, x - width, y - height, newWidth, newHeight, canvas);

            // Top
            canvas.drawSquare(x, y - height, newWidth, newHeight);
            draw_sierpinski_carpet(levels - 1, x, y - height, newWidth, newHeight, canvas);

            // Top Right
            canvas.drawSquare(x + width, y - height, newWidth, newHeight);
            draw_sierpinski_carpet(levels - 1, x + width, y - height, newWidth, newHeight, canvas);

            // Right
            canvas.drawSquare(x + width, y, newWidth, newHeight);
            draw_sierpinski_carpet(levels - 1, x + width, y, newWidth, newHeight, canvas);

            // Bottom Right
            canvas.drawSquare(x + width, y + height, newWidth, newHeight);
            draw_sierpinski_carpet(levels - 1, x + width, y + height, newWidth, newHeight, canvas);

            // Bottom
            canvas.drawSquare(x, y + height, newWidth, newHeight);
            draw_sierpinski_carpet(levels - 1, x, y + height, newWidth, newHeight, canvas);

            // Bottom Left
            canvas.drawSquare(x - width, y + height, newWidth, newHeight);
            draw_sierpinski_carpet(levels - 1, x - width, y + height, newWidth, newHeight, canvas);
        }

        public interface SierpinskiCanvas
        {
            void drawSquare(double x, double y, double width, double height);
        }
    }
}
