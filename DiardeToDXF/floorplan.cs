using System.Collections.Generic;
using System.Linq;

namespace DiardeToDXF.logic
{
    class Floorplan
    {

        public class Tetragon
        {

            public readonly Point2D p1;
            public readonly Point2D p2;
            public readonly Point2D p3;
            public readonly Point2D p4;

            public Tetragon(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
            {
                this.p1 = p1;
                this.p2 = p2;
                this.p3 = p3;
                this.p4 = p4;
            }

        }
        public class Wall
        {

            public readonly Direction direction;
            public readonly Point2D start;
            public readonly Point2D end;
            public readonly List<Tetragon> sections;
            public readonly List<Tetragon> doors;
            public readonly List<Tetragon> windows;

            public Wall(Point2D start, Point2D end, Direction direction, List<Tetragon> sections, List<Tetragon> doors, List<Tetragon> windows)
            {
                this.start = start;
                this.end = end;
                this.direction = direction;
                this.sections = sections;
                this.doors = doors;
                this.windows = windows;
            }

        }

        public enum Direction { 
            NORTH, 
            SOUTH, 
            WEST, 
            EAST
        }

        private Dictionary<string, Point2D> vertices = new Dictionary<string, Point2D>();
        private Dictionary<string, Point2D> displacements = new Dictionary<string, Point2D>();

        public Floorplan(DiardeToDXF.Floorplan floorplan)
        {

            foreach (Vertex vertex in floorplan.Vertices)
            {
                this.vertices.Add(vertex.Id, vertex.Point);
            }

            this.maxX = floorplan.Vertices.Select(vertex => vertex.Point.X).Max();
            this.minX = floorplan.Vertices.Select(vertex => vertex.Point.X).Min();
            this.maxY = floorplan.Vertices.Select(vertex => vertex.Point.Y).Max();
            this.minY = floorplan.Vertices.Select(vertex => vertex.Point.Y).Min();

            foreach (Displacement displacement in floorplan.Displacements)
            {
                this.displacements.Add(displacement.Id, displacement.Vector);
            }

            this.Walls = floorplan.Walls.Select(wall => {
                Point2D v1, v2;
                Direction? direction = this.getDirection(wall.Stretch);
                if (direction != null)
                {
                    if (this.vertices.TryGetValue(wall.Stretch[0], out v1))
                    {
                        if (this.vertices.TryGetValue(wall.Stretch[1], out v2))
                        {
                            getStartAndEnd(wall.Stretch[0], v1, v2, out v1, out v2);
                            return new Wall(v1, v2, (Direction) direction, makeTetragons(wall.Sections), makeTetragons(wall.Doors), makeTetragons(wall.Windows));
                        }
                    }
                }
                return null;
            }).Where(x => x != null).ToList();

        }

        public List<Wall> Walls { get; }
        public float minX, minY, maxX, maxY;

        private List<Tetragon> makeTetragons(string[][] sections)
        {
            return sections.Select(section => {
                Point2D p1, p2, v1, v2;
                if (this.vertices.TryGetValue(section[0], out p1))
                {
                    if (this.displacements.TryGetValue(section[0], out v1))
                    {
                        if (this.vertices.TryGetValue(section[1], out p2))
                        {
                            if (this.displacements.TryGetValue(section[1], out v2))
                            {
                                Point2D p = new Point2D();
                                p.X = p1.Y - p2.Y;
                                p.Y = p2.X - p1.X;
                                return p.dot(v1) > 0 ? new Tetragon(p1, p1.add(v1), p2.add(v2), p2) : new Tetragon(p2, p2.add(v2), p1.add(v1), p1);
                            }
                        }
                    }
                }
                return null;
            }).Where(x => x != null).ToList();
        }

        private void getStartAndEnd(string point, Point2D v1_old, Point2D v2_old,  out Point2D v1, out Point2D v2) {
            Point2D disp;
            if (this.displacements.TryGetValue(point, out disp)) {
                Point2D p = new Point2D();
                p.X = v1_old.Y - v2_old.Y;
                p.Y = v2_old.X - v1_old.X;
                if (p.dot(disp) > 0) {
                    v1 = v1_old;
                    v2 = v2_old;
                } else {
                    v1 = v2_old;
                    v2 = v1_old;
                }
            } else {
                v1 = v1_old;
                v2 = v2_old;
            }
        }

        private Direction? getDirection(string[] wall)
        {
            Point2D disp, v1, v2;
            Point2D up = new Point2D();
            Point2D down = new Point2D();
            Point2D right = new Point2D();
            Point2D left = new Point2D();
            up.X = 0; up.Y = -1;
            down.X = 0; down.Y = 1;
            left.X = -1; left.Y = 0;
            right.X = 1; right.Y = 0;

            if (this.displacements.TryGetValue(wall[0], out disp)) {
                if (this.vertices.TryGetValue(wall[0], out v1)) {
                    if (this.vertices.TryGetValue(wall[1], out v2)) {
                        Point2D p = new Point2D();
                        p.X = v1.Y - v2.Y;
                        p.Y = v2.X - v1.X;
                        Point2D normal = p.scale(p.dot(disp)).normalize();
                        if (normal.dot(up) > 0.99)
                        {
                            return Direction.NORTH;
                        }
                        if (normal.dot(down) > 0.99)
                        {
                            return Direction.SOUTH;
                        }
                        if (normal.dot(left) > 0.99)
                        {
                            return Direction.EAST;
                        }
                        if (normal.dot(right) > 0.99)
                        {
                            return Direction.WEST;
                        }
                    }
                }
            }

            return null;
        }

    }

}
