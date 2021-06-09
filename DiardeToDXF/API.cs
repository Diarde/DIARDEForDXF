using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json;

namespace DiardeToDXF
{
    class API
    {
        static string loginURL = $"https://diarde.com/_api/login"; // POST
        static string modelURL = $"https://diarde.com/_api/loadmodel"; // POST
        static string projectsURL = $"https://diarde.com/_api/projects"; // GET
        static string email2 = "kevin@freelancer.com", password2 = "kevinForDiarde";
        public static WebClientEx client = new WebClientEx();

        public static string SelectedProject = null;
        public static string SelectedRoom = null;
        public static string SelectedProjectName = null;
        public static string SelectedRoomName = null;
        public static string SelectedGeometry = null;
        public static string AuthErrorMessage = "";
        //static WebClientEx client = new WebClientEx();
        public static bool Login(string email, string password)
        {
            AuthErrorMessage = "";
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            try
            {
                var data = new NameValueCollection{
                    { "email", email }, { "password", password }};
                string loginResponse = Encoding.Default.GetString(API.client.UploadValues(loginURL, data));
                Login login = JsonConvert.DeserializeObject<Login>(loginResponse);
                if (login.Authentication is "success")
                    return true;
                else
                {
                    AuthErrorMessage = login.Error;
                    return false;
                }
            }
            catch { return false; }
        }

        public static Project[] GetProjects()
        {
            try
            {
                // Projects:
                var projectsResponse = API.client.DownloadString(projectsURL);
                Project[] projects = JsonConvert.DeserializeObject<Project[]>(projectsResponse);
                return projects;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static Room[] GetRooms()
        {
            try
            {
                // Rooms:
                var roomsResponse = API.client.DownloadString($"{projectsURL}/{SelectedProject}/rooms");
                Room[] rooms = JsonConvert.DeserializeObject<Room[]>(roomsResponse);
                return rooms;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("error");
                return null;
            }
        }
        public static Geometry[] GetGeometries()
        {
            try
            {
                // Geometry:
                var geometriesResponse = API.client.DownloadString($"{projectsURL}/{SelectedProject}/" +
                    $"rooms/{SelectedRoom}/geometries");
                Geometry[] geometries = JsonConvert.DeserializeObject<Geometry[]>(geometriesResponse);
                return geometries;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("error");
                return null;
            }
        }
        public static Revision[] GetRevisions()
        {
            try
            {
                // Revisions:
                var revisionsResponse = API.client.DownloadString($"{projectsURL}/{SelectedProject}/rooms/" +
                    $"{SelectedRoom}/geometries/{SelectedGeometry}/revisions");
                Revision[] revisions = JsonConvert.DeserializeObject<Revision[]>(revisionsResponse);
                return revisions;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("error");
                return null;
            }
        }

        public static Model GetModel(string id)
        {
            try
            {
                var data = new NameValueCollection { { "id", id } };
                string response = Encoding.Default.GetString(API.client.UploadValues(modelURL, data));
                Model model = JsonConvert.DeserializeObject<Model>(response);
                return model;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("error");
                return null;
            }
        }


        public static string ConvertToPreviewURL(string url) =>
            $"https://sketchup.diarde.com/_uploads/{url}.thumb.jpg";



    }

    public class WebClientEx : WebClient
    {
        private CookieContainer _cookieContainer = new CookieContainer();

        public WebClientEx() {
            this.Encoding = Encoding.UTF8;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = _cookieContainer;
            }
            return request;
        }
    }

    // API
    // API
    public class Login
    {
        [JsonProperty("authentication")]
        public string Authentication { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Project
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Room
    {
        [JsonProperty("fotos")]
        public Foto[] Fotos { get; set; }

        [JsonProperty("supplements")]
        public Foto[] Supplements { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }

    public class Foto
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("owner")]
        public object Owner { get; set; }

        [JsonProperty("__v")]
        public long V { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Revision
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public string Model { get; set; }
    }

    public class Model
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("cameras")]
        public Object Cameras { get; set; }

        [JsonProperty("model")]
        public Object Model { get; set; }

        [JsonProperty("floorplan")]
        public Floorplan Floorplan { get; set; }

    }

    public class Floorplan
    {
        [JsonProperty("vertices")]
        public Vertex[] Vertices { get; set; }

        [JsonProperty("displacements")]
        public Displacement[] Displacements { get; set; }

        [JsonProperty("walls")]
        public Wall[] Walls { get; set; }
    }

    public class Vertex
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("point")]
        public Point2D Point { get; set; }
    }

    public class Displacement
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("vector")]
        public Point2D Vector { get; set; }
    }

    public class Wall
    {
        [JsonProperty("wall")]
        public string[] Stretch { get; set; }

        [JsonProperty("sections")]
        public string[][] Sections { get; set; }

        [JsonProperty("doors")]
        public string[][] Doors { get; set; }

        [JsonProperty("windows")]
        public string[][] Windows { get; set; }
    }

    public class Point2D
    {
        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        public Point2D add(Point2D point)
        {
            Point2D _point = new Point2D();
            _point.X = this.X + point.X;
            _point.Y = this.Y + point.Y;
            return _point;
        }

        public Point2D sub(Point2D point)
        {
            Point2D _point = new Point2D();
            _point.X = this.X - point.X;
            _point.Y = this.Y - point.Y;
            return _point;
        }

        public Point2D normalize() {
            float length = Convert.ToSingle(Math.Sqrt(this.X * this.X + this.Y * this.Y));
            Point2D returnPoint = new Point2D();
            returnPoint.X = (this.X / length);
            returnPoint.Y = (this.Y / length);
            return returnPoint;
        }

        public Point2D scale(float scale) {
            Point2D returnPoint = new Point2D();
            returnPoint.X = (this.X * scale);
            returnPoint.Y = (this.Y * scale);
            return returnPoint;
        }

        public float dot(Point2D point) {
            return this.X * point.X + this.Y * point.Y;
        }

    }

    public class ProjectComparer : IComparer<Project>
    {
        public int Compare(Project x, Project y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    public class RoomComparer : IComparer<Room>
    {
        public int Compare(Room x, Room y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    public class GeometryComparer : IComparer<Geometry>
    {
        public int Compare(Geometry x, Geometry y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
