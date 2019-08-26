MessageBird's REST API for C#
===============================
This repository contains the open source C# client for MessageBird's REST API. Documentation can be found at: https://developers.messagebird.com

Requirements
-----

- [Sign up](https://www.messagebird.com/en/signup) for a free MessageBird account
- Create a new access_key in the developers sections
- MessageBird API client for C# requires **.NET Framework >= 4** or **.NET Standard >= 2.0**
- It has a dependency on **JSON.NET >= 4.5.6**

Installation
-----

#### JSON.NET

The *MessageBird* project depends on JSON.NET, which can be installed using the *NuGet* package manager.
For more information on how to achieve this, see ['Managing NuGet packages using the dialog'](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog).

Usage
-----

We have put some self-explanatory examples in the *Examples* project, but here is a quick breakdown on how it works. First, you need to set up a **MessageBird.Client**. Be sure to replace **YOUR_ACCESS_KEY** with something real.

```csharp
using System;
using MessageBird;
using MessageBird.Objects;

class Example
{
  static void Main(string[] args)
  {
    Client client = Client.CreateDefault("YOUR_ACCESS_KEY");
  }
}
```

That's easy enough. Now we can query the server for information. Lets use getting your balance overview as an example:

```csharp
// Get your balance
Balance balance = client.Balance();
```

#### Error handling
In case of an error the *client* throws an *ErrorException*.
The *ErrorException* objects contains either:
- an array of *Error* objects that encapsulates the errors in the response (see [errors](https://developers.messagebird.com/docs/errors) for more info),
- a *best effort* description when no error information from the endpoint is available.

See the provided examples on how to access error information from the *ErrorException*.

##### Conversations WhatsApp Sandbox
To use the whatsapp sandbox you need to add `Client.Features.EnableWhatsAppSandboxConversations` to the list of features you want enabled. Don't forget to replace `YOUR_ACCESS_KEY` with your actual access key.

```csharp
using System;
using MessageBird;
using MessageBird.Objects;

class Example
{
  static void Main(string[] args)
  {
    Client client = Client.CreateDefault("YOUR_ACCESS_KEY",features: new Client.Features[] { Client.Features.EnableWhatsAppSandboxConversations });
  }
}
```
Documentation
----
Complete documentation, instructions, and examples are available at:
[https://developers.messagebird.com](https://developers.messagebird.com)


License
----
The MessageBird REST Client for C# is licensed under [The ISC License](http://choosealicense.com/licenses/isc/). Copyright (c) 2014, Remco Vermeulen
