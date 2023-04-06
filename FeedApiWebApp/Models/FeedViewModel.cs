namespace FeedApiWebApp.Models;

// Children. Only for media_type "CAROUSEL"
public class Children
{
    public List<Datum> data { get; set; }
}

// Cursors for use in pagination
public class Cursors
{
    public string before { get; set; }
    public string after { get; set; }
}

// A singular piece of data
public class Datum
{
    public string id { get; set; }
    public string caption { get; set; }
    public string media_type { get; set; }
    public string media_url { get; set; }
    public string permalink { get; set; }
    public string timestamp { get; set; }
    public string username { get; set; }
    public Children children { get; set; }
}

// Pagination. Refers to the cursors
public class Paging
{
    public Cursors cursors { get; set; }
}

// The root model of the JSON result
public class FeedViewModel
{
    public List<Datum> data { get; set; }
    public Paging paging { get; set; }
}
