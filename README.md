# Using server sent events in C# using minimal API

https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#event_stream_format

Uses an EventSource Javascript object that opens a persistent connection to a server, which sends events in text/event-stream format. The connection remains open until closed by calling eventSource.close()

https://developer.mozilla.org/en-US/docs/Web/API/EventSource#Examples

server-sent events are used by chat-gpt's completions endpoint for example:

https://platform.openai.com/docs/api-reference/completions

However chat-gpt does things a little bit differently by using a POST request using fetch and not using EventSource and also using Content-Type application/json instead of text/event-stream 

I've included an example that hits the openai endpoint to send gpt 3.5 a prompt
