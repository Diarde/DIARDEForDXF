using netDxf;
using netDxf.Entities;
using netDxf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace DiardeToDXF
{
    public partial class MainWindow : Form
    {


        private Point MouseDownLocation;
        private List<Project> projectsList = new List<Project>();
        public static CustomPanel focusedPanel = null;
        private string username = "";
        public MainWindow(string username)
        {
            InitializeComponent();
            this.username = username;
            //this.Deactivate += (o, e) => this.Hide();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            CueProvider.SetCue(txtSearch, "Search");

            Project[] projects = API.GetProjects();
            Array.Sort(projects, new ProjectComparer());

            foreach (Project project in projects)
            {
                Button button = projectsPanel.CreateItem(project.Name, project);
                button.Click += Button_OnClick;
                projectsList.Add(project);
            }
            cmText.Text = username;
            profileMenu.Width = btnLogout.Width = cmText.Width;
        }

        private void Button_OnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Cursor = Cursors.WaitCursor;
            if (button.Tag is Project) // Project clicked
            {
                Project project = button.Tag as Project;
                API.SelectedProject = project.Id;
                API.SelectedProjectName = project.Name;
                projectsPanel.ResetBGColor();
                button.BackColor = Color.LightGray;
                roomsPanel.Clear();
                geometriesPanel.Clear();
                revisionsPanel.Clear();
                previewsPanel.Clear();
                Room[] rooms = API.GetRooms();
                Array.Sort(rooms, new RoomComparer());
                foreach (Room room in rooms)
                {
                    if (room.Name is "") continue;
                    Button newButton = roomsPanel.CreateItem(room.Name, room);
                    newButton.Click += Button_OnClick;
                }
            }
            else if (button.Tag is Room)
            {
                Room room = button.Tag as Room;
                API.SelectedRoom = room.Id;
                API.SelectedRoomName = room.Name;
                roomsPanel.ResetBGColor();
                button.BackColor = Color.LightGray;
                geometriesPanel.Clear();
                revisionsPanel.Clear();
                previewsPanel.Clear();
                Geometry[] geometries = API.GetGeometries();
                Array.Sort(geometries, new GeometryComparer());

                foreach (Geometry geometry in geometries)
                {
                    if (geometry.Name is "") continue;
                    geometriesPanel.CreateItem(geometry.Name, geometry).Click += Button_OnClick;
                }
                // Load previews:
                if (room.Fotos != null)
                    foreach (Foto foto in room.Fotos)
                    {
                        PictureBox newPicture = previewsPanel.CreatePreview(API.ConvertToPreviewURL(foto.Filename));
                    }
            }
            else if (button.Tag is Geometry)
            {
                Geometry geometry = button.Tag as Geometry;
                API.SelectedGeometry = geometry.Id;
                geometriesPanel.ResetBGColor();
                button.BackColor = Color.LightGray;
                revisionsPanel.Clear();
                foreach (Revision revision in API.GetRevisions())
                {
                    bool grayOut = revision.Model is null;
                    Button newButton = revisionsPanel.CreateItem(revision.Date.ToString(), revision, grayOut);
                    if (grayOut is false) newButton.Click += Button_OnClick;
                }

            }
            else if (button.Tag is Revision)
            {
                Revision revision = button.Tag as Revision;
                Model model = API.GetModel(revision.Model);
                DiardeToDXF.logic.Floorplan floorplan = new DiardeToDXF.logic.Floorplan(model.Data.Floorplan);
                if (floorplan.Walls.Count > 0)
                    saveFloorplan(API.SelectedProjectName, API.SelectedRoomName, floorplan);
            }
            button.Cursor = Cursors.Arrow;
        }

        private void saveFloorplan(string project, string room, logic.Floorplan floorplan)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            float scale = 100.0f;

            saveFileDialog1.Filter = "dxf files (*.dxf)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {

                    DxfDocument doc = DxfDocument.Load(new MemoryStream(DiardeToDXF.Properties.Resources.Musterdatei));
                    DimensionStyle dimensionStyle = doc.DimensionStyles.First(x => x.Name == "BEM M50");
                    TextStyle textStyle = doc.TextStyles.First(x => x.Name == "ARIAL");

                    Point2D p1 = new Point2D();
                    Point2D p2 = new Point2D();
                    p1.X = floorplan.minX; p1.Y = floorplan.minY;
                    p2.X = floorplan.maxX; p2.Y = floorplan.minY;

                    AlignedDimension dimension = new AlignedDimension(ConvertPoint2D(p1, scale), ConvertPoint2D(p2, scale), Math.Abs(floorplan.minY) + 2.0 * scale);
                    dimension.Style = dimensionStyle;
                    doc.AddEntity(dimension);

                    p1.X = floorplan.minX; p1.Y = floorplan.maxY;
                    p2.X = floorplan.minX; p2.Y = floorplan.minY;
                    dimension = new AlignedDimension(ConvertPoint2D(p1, scale), ConvertPoint2D(p2, scale), Math.Abs(floorplan.minX) + 2.0 * scale);
                    dimension.Style = dimensionStyle;
                    doc.AddEntity(dimension);

                    foreach (logic.Floorplan.Wall wall in floorplan.Walls)
                    {
                        float offset = 0;
                        switch (wall.direction) {
                            case logic.Floorplan.Direction.NORTH:
                                offset = scale * Math.Abs(floorplan.minY - wall.end.Y);
                                break;
                            case logic.Floorplan.Direction.SOUTH:
                                offset = scale * Math.Abs(floorplan.maxY - wall.end.Y);
                                break;
                            case logic.Floorplan.Direction.EAST:
                                offset = scale * Math.Abs(floorplan.minX - wall.end.X);
                                break;
                            case logic.Floorplan.Direction.WEST:
                                offset = scale * Math.Abs(floorplan.maxX - wall.end.X);
                                break;
                        }

                        dimension = new AlignedDimension(ConvertPoint2D(wall.end, scale), ConvertPoint2D(wall.start, scale), offset + 1.5 * scale);
                        dimension.Style = dimensionStyle;
                        doc.AddEntity(dimension);

                        foreach (logic.Floorplan.Tetragon section in wall.sections)
                        {
                            Vector2[] autoCADWall = GetSection(section,scale);

                            Solid solid = new Solid(autoCADWall[0] ,autoCADWall[1], autoCADWall[3], autoCADWall[2]);
                            doc.AddEntity(solid);

                            dimension = new AlignedDimension(autoCADWall[3], autoCADWall[0], offset + 1.0 * scale);
                            dimension.Style = dimensionStyle;
                            doc.AddEntity(dimension);
                        }

                        foreach (logic.Floorplan.Tetragon door in wall.doors)
                        {
                            Vector2[] autoCADWall = GetSection(door, scale);

                            Line line1 = new Line(autoCADWall[0], autoCADWall[1]);
                            doc.AddEntity(line1);
                            Line line2 = new Line(autoCADWall[1], autoCADWall[2]);
                            doc.AddEntity(line2);
                            Line line3 = new Line(autoCADWall[2], autoCADWall[3]);
                            doc.AddEntity(line3);
                            Line line4 = new Line(autoCADWall[3], autoCADWall[0]);
                            doc.AddEntity(line4);

                            dimension = new AlignedDimension(line4, offset + 1.0 *scale);
                            dimension.Style = dimensionStyle;
                            doc.AddEntity(dimension);

                        }
                        foreach (logic.Floorplan.Tetragon window in wall.windows)
                        {
                            Vector2[] autoCADWall = GetSection(window, scale);

                            Line line1 = new Line(autoCADWall[0], autoCADWall[1]);
                            doc.AddEntity(line1);
                            Line line2 = new Line(autoCADWall[1], autoCADWall[2]);
                            doc.AddEntity(line2);
                            Line line3 = new Line(autoCADWall[2], autoCADWall[3]);
                            doc.AddEntity(line3);
                            Line line4 = new Line(autoCADWall[3], autoCADWall[0]);
                            doc.AddEntity(line4);

                            dimension = new AlignedDimension(line4, offset + 1.0 *scale);
                            dimension.Style = dimensionStyle;
                            doc.AddEntity(dimension);
                        }

                    }

                    Vector2 vector = new Vector2(scale * (floorplan.maxX + floorplan.minX) / 2.0, -scale * (floorplan.maxY + floorplan.minY) / 2.0);
                    netDxf.Entities.Text text = new netDxf.Entities.Text($"{project} - {room}", vector, 10, textStyle);
                    text.Alignment = TextAlignment.BaselineCenter;
                    doc.AddEntity(text);

                    vector = new Vector2(scale * (floorplan.maxX + floorplan.minX) / 2.0, -(scale * (floorplan.maxY + floorplan.minY) / 2.0 + 15));
                    text = new netDxf.Entities.Text($"Boden - OKFF = +/-{0}", vector, 10, textStyle);
                    text.Alignment = TextAlignment.BaselineCenter;
                    doc.AddEntity(text);

                    vector = new Vector2(scale * (floorplan.maxX + floorplan.minX) / 2.0, -(scale * (floorplan.maxY + floorplan.minY) / 2.0 + 2 * 15));
                    text = new netDxf.Entities.Text($"Deckenhöhe - UKD = +/-{120}", vector, 10, textStyle);
                    text.Alignment = TextAlignment.BaselineCenter;
                    doc.AddEntity(text);
                    // save to file
                    doc.Save(myStream);
                    myStream.Close();
                    this.Show();
                }
            }
  
        }
        private static Vector2[] GetSection(logic.Floorplan.Tetragon tetragon, float scale)
        {
            Vector2 point1 = new Vector2(tetragon.p1.X * scale, -tetragon.p1.Y * scale);
            Vector2 point2 = new Vector2(tetragon.p2.X * scale, -tetragon.p2.Y * scale);
            Vector2 point3 = new Vector2(tetragon.p3.X * scale, -tetragon.p3.Y * scale);
            Vector2 point4 = new Vector2(tetragon.p4.X * scale, -tetragon.p4.Y * scale);
            return new Vector2[] { point1, point2, point3, point4 };
        }

        private static Vector2 ConvertPoint2D(Point2D point, float scale) {
            return new Vector2(point.X * scale, -point.Y * scale);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            projectsPanel.HideScrollbar();
            roomsPanel.HideScrollbar();
            geometriesPanel.HideScrollbar();
            revisionsPanel.HideScrollbar();
            previewsPanel.HideScrollbar();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Program.loginScreen.Show();
            Program.LoggedIn = false;
            if (File.Exists("user.json")) try { File.Delete("user.json"); } catch { }
            Dispose();
        }

        private void titlebar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                MouseDownLocation = e.Location;
        }

        private void titlebar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Left = e.X + Left - MouseDownLocation.X;
                Top = e.Y + Top - MouseDownLocation.Y;
            }
        }

        private void profilePicture_Click(object sender, EventArgs e)
        {
            PictureBox btnSender = (PictureBox)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            profileMenu.Show(ptLowerLeft);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            projectsPanel.Clear();
            roomsPanel.Clear();
            geometriesPanel.Clear();
            revisionsPanel.Clear();
            previewsPanel.Clear();
            if (txtSearch.Text is "")
            {
                foreach (var project in projectsList)
                {
                    Button button = projectsPanel.CreateItem(project.Name, project);
                    button.Click += Button_OnClick;
                }
                return;
            }
            List<Project> newList = projectsList.Where(project => project.Name.ToLower().Contains(
                txtSearch.Text.ToLower())).ToList();

            foreach (var project in newList)
            {
                Button button = projectsPanel.CreateItem(project.Name, project);
                button.Click += Button_OnClick;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void MainWindow_Click(object sender, EventArgs e)
        {
            txtSearch.Enabled = false;
            txtSearch.Enabled = true;
        }

}
}
