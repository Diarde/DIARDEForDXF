using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutocadLibrary.logic
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

            public readonly List<Tetragon> sections;
            public readonly List<Tetragon> doors;
            public readonly List<Tetragon> windows;

            public Wall(List<Tetragon> sections, List<Tetragon> doors, List<Tetragon> windows)
            {
                this.sections = sections;
                this.doors = doors;
                this.windows = windows;
            }

        }

        private Dictionary<string, Point2D> vertices = new Dictionary<string, Point2D>();
        private Dictionary<string, Point2D> displacements = new Dictionary<string, Point2D>();

        public Floorplan(AutocadLibrary.Floorplan floorplan)
        {

            foreach (Vertex vertex in floorplan.Vertices)
            {
                this.vertices.Add(vertex.Id, vertex.Point);
            }

            foreach (Displacement displacement in floorplan.Displacements)
            {
                this.displacements.Add(displacement.Id, displacement.Vector);
            }

            this.Walls = floorplan.Walls.Select(wall => {
                return new Wall(makeTetragons(wall.Sections), makeTetragons(wall.Doors), makeTetragons(wall.Windows));
            }).ToList();

        }

        public List<Wall> Walls { get; }

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
                                return new Tetragon(p1, p1.add(v1), p2.add(v2), p2);
                            }
                        }
                    }
                }
                return null;
            }).Where(x => x != null).ToList();
        }

    }
}
