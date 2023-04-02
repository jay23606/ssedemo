# Using server sent events in C# using minimal API

https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#event_stream_format

server-sent events are used by chat-gpt's completions endpoint for example:

https://platform.openai.com/docs/api-reference/completions

Uses an EventSource Javascript object that opens a persistent connection to a server, which sends events in text/event-stream format. The connection remains open until closed by calling eventSource.close()

https://developer.mozilla.org/en-US/docs/Web/API/EventSource#Examples
