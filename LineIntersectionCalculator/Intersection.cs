using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LineIntersectionCalculator
{
    class Intersection
    {
        public bool DoesIntersect { get; set; }
        public bool SegmentsDoIntersect { get; set; }
        public Point IntersectionPoint { get; set; }
        public Point CloseP1 { get; set; }
        public Point CloseP2 { get; set; }

        public Intersection(Point p1, Point p2, Point p3, Point p4)
        {
            FindIntersection(p1, p2, p3, p4);
        }
        private void FindIntersection(Point p1, Point p2, Point p3, Point p4)
        {
            // Get the segments' parameters.
            double dx12 = p2.X - p1.X;
            double dy12 = p2.Y - p1.Y;
            double dx34 = p4.X - p3.X;
            double dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            double denominator = (dy12 * dx34 - dx12 * dy34);

            double t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                    / denominator;

            if (double.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                this.DoesIntersect = false;
                this.SegmentsDoIntersect = false;
                this.IntersectionPoint = new Point(float.NaN, float.NaN);
                this.CloseP1 = new Point(float.NaN, float.NaN);
                this.CloseP2 = new Point(float.NaN, float.NaN);
                return;
            }

            this.DoesIntersect = true;

            double t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                    / -denominator;

            // Find the point of intersection.
            this.IntersectionPoint = new Point(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            // The segments intersect if t1 and t2 are between 0 and 1.
            this.SegmentsDoIntersect =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            this.CloseP1 = new Point(p1.X + dx12 * t1, p1.Y + dy12 * t1);
            this.CloseP2 = new Point(p3.X + dx34 * t2, p3.Y + dy34 * t2);
        }
    }
}
