// This project is a version of this code in C#:
// https://github.com/mdn/dom-examples/tree/main/server-sent-events
// I'd like to also see how I can do this in a blazor page
// perhaps without JS interop

//Instructions
//Navigate to https://localhost to see DOM events interact with the server
//Navigate to https://localhost/events to see the events sent from server and how 
// we break infinite loop if context.RequestAborted.IsCancellationRequested

using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
//HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    //options.ListenAnyIP(80);
    options.ListenAnyIP(443, listenOptions => { listenOptions.UseHttps(); });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Stream the events
app.Map("/events", eventsApp =>
{
    eventsApp.Run(async context =>
    {
        // Set the timezone to America/New_York
        TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
        string timezoneFormatted = timezone.BaseUtcOffset.ToString(@"hh\:mm");

        // Set the headers
        context.Response.Headers.Add("Cache-Control", "no-store");
        context.Response.Headers.Add("Content-Type", "text/event-stream");

        // Set the counter
        int counter = new Random().Next(1, 10);

        // Stream the events
        while (true)
        {
            await context.Response.WriteAsync("event: ping\n");
            string curDate = DateTime.UtcNow.Add(timezone.BaseUtcOffset).ToString("s") + timezoneFormatted;
            await context.Response.WriteAsync($"data: {{\"time\": \"{curDate}\"}}\n\n");

            counter--;
            if (counter == 0)
            {
                await context.Response.WriteAsync($"data: This is a message at time {curDate}\n\n");
                counter = new Random().Next(1, 10);
            }

            await context.Response.Body.FlushAsync();
            if (context.RequestAborted.IsCancellationRequested) 
                break;
            await Task.Delay(1000);
        }
    });
});

await app.RunAsync();