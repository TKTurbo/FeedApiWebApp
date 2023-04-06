using System.Text.Json;

namespace FeedApiWebApp.Models;

// Methods to retrieve the feed from the API
public class Feed
{
	// The fields we need. These get put into the model.
	private readonly string[] fields =
		{ "id", "caption", "media_type", "media_url", "permalink", "timestamp", "username"};

	private readonly string[] child_fields = {"media_type" , "media_url", "thumbnail_url" };

	private String baseUrl;
    private String token;
    
    // The final URL which will be fetched
    private String fetchFeedURL;

    // Constructor to get environment variables
    public Feed()
    {
	    this.baseUrl = Environment.GetEnvironmentVariable("INSTAGRAM_API_BASE_URL");
	    this.token = Environment.GetEnvironmentVariable("INSTAGRAM_API_KEY");

	    if (this.token == null || this.baseUrl == null)
	    {
		    throw new Exception("Required environment variables are not defined!");
	    }
	    
	    // Build the URL
	    this.buildURL();
    }
    
    // Build the fields and add the children.
    public String getFields()
    {
	    List<String> fields = this.fields.ToList();
	    
	    fields.Add("children{" + String.Join(",", this.child_fields) + "}");

	    return String.Join(',', fields);
    }

    // Build the full URL for retrieving the feed
    public void buildURL()
    {
	    this.fetchFeedURL = baseUrl + "/me/media?fields=" + this.getFields() + "&access_token=" + token;
	    
	    Console.WriteLine(this.fetchFeedURL);
	}

    // Fetch and serialize the data
    public FeedViewModel get()
    {
	    // Fetch the data asynchonously
	    var task = fetch();
	    task.Wait();

	    // Serialize the JSON data as an instance and return it
	    return JsonSerializer.Deserialize<FeedViewModel>(task.Result);
    }

    // Fetch the data from the API, and return it as a task string
    public async Task<String> fetch()
    {
	    // Using to create the client, make a response and get the content
	    using (HttpClient client = new HttpClient())
	    using (HttpResponseMessage response = await client.GetAsync(this.fetchFeedURL))
	    using (HttpContent content = response.Content)
	    {
		    // Read the async string and return it
		    return await content.ReadAsStringAsync();
	    }
	}
}