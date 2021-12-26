using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging;

namespace ApiGateWay.Controllers
{
    public class ServiceRegistrationController : Controller
    {
        // GET: ServiceRegistrationController
        public async Task<ActionResult> Index()
        {
            using StreamReader r = new StreamReader("appsettings.json");
            var json = r.ReadToEnd();
            var items = JsonConvert.DeserializeObject<Rootobject>(json);
            r.Close();
            items.Routes.Add(new Route()
            {
                UpstreamPathTemplate = "/prr",
                DownstreamScheme = "https",
                DownstreamPathTemplate = "/User",
                DownstreamHostAndPorts = new List<Downstreamhostandport>()
                    {new Downstreamhostandport() {Host = "localhost", Port = 7048}},
                UpstreamHttpMethod = new List<string>() {"Get"},
            });
            
            //using StreamWriter w = new StreamWriter("appsettings.json");
            //w.Write(items);
            var dsd = items;
            var text = JsonConvert.SerializeObject(items);
            await System.IO.File.WriteAllTextAsync("appsettings.json", text);
            return View();
        }

        // GET: ServiceRegistrationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiceRegistrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceRegistrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceRegistrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServiceRegistrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceRegistrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServiceRegistrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}


public class Rootobject
{
    public List<Route> Routes { get; set; }
    public Globalconfiguration GlobalConfiguration { get; set; }
}

public class Globalconfiguration
{
    public string BaseUrl { get; set; }
}

public class Route
{
    public string DownstreamPathTemplate { get; set; }
    public string DownstreamScheme { get; set; }
    public List<Downstreamhostandport> DownstreamHostAndPorts { get; set; }
    public string UpstreamPathTemplate { get; set; }
    public List<string> UpstreamHttpMethod { get; set; }
}

public class Downstreamhostandport
{
    public string Host { get; set; }
    public int Port { get; set; }
}
