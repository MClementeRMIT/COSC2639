using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;


namespace HIAC19.Pages;

public class IndexModel : PageModel
{

    public bool Submitted;
    public string SubmittedCoordinates;
    private class locData { 
        public long idx { get; set; }
        public string id { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }
    }

    private class regData { 
        public string id { get; set; }
        public string number { get; set; }
    }

    private class posRegData
    {
        public string id { get; set; }
    }

    public IndexModel()
    {

    }

    public void OnPostSubmit() {
   
        var data = new locData() {
            idx = DateTime.Now.Ticks,
            id = Request.Form["id"].ToString(),
            lng = Request.Form["LngInput"].ToString(),
            lat = Request.Form["LatInput"].ToString()
        };
        var lam = "https://mgvlu95dde.execute-api.ap-southeast-2.amazonaws.com/saveLocation";

        var wc = new WebClient();
        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
        wc.UploadString(lam, "POST", JsonSerializer.Serialize(data));

        Submitted = true;
        SubmittedCoordinates = data.lng + ", " + data.lat;
    }

    public void OnPostRegistration()  
    {
        var data = new regData()
        {
            id = Request.Form["id"].ToString(),
            number = Request.Form["PhoneNumber"].ToString() 
        };
        var lam = "https://dj7uevri7e.execute-api.ap-southeast-2.amazonaws.com/register";
        
        var wc = new WebClient();
        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
        wc.UploadString(lam, "POST", JsonSerializer.Serialize(data));
    }

    public void OnPostPositiveRegistration() 
    {
        var data = new posRegData()
        {
            id = Request.Form["id"].ToString()
        };
        var lam = "https://3cufwqfxw0.execute-api.ap-southeast-2.amazonaws.com/positiveRegistration";

        var wc = new WebClient();
        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
        wc.UploadString(lam, "POST", JsonSerializer.Serialize(data));

        //localStorage.setItem('registered', event.currentTarget [0].value);
        //this.setState({ hasRegistered: true });
    }

    public void OnGet()
    {
    }
}