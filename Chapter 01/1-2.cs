//Sample code
public static async void WebResults(WebSearchClient client)
{
    try
    {
        var fetchedData = await client.Web.SearchAsync(query: "Tom Campbell's Hill Natural Park");
        Console.WriteLine("Looking for \"Tom Campbell's Hill Natural Park\"");

        // ...

    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception during search. " + ex.Message);
    }
}